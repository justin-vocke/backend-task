using BackendTask.API.ActionFilters;
using BackendTask.API.Extensions;
using BackendTask.Business.Services.Authentication;
using BackendTask.Business.Services.Students;
using BackendTask.Data.Contracts;
using BackendTask.Data.DbContexts;
using BackendTask.Data.Implemenations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApi(options =>
            {
                builder.Configuration.Bind("AzureAd", options);
                options.Events = new JwtBearerEvents();

                /// <summary>
                /// Below you can do extended token validation and check for additional claims, such as:
                ///
                /// - check if the caller's tenant is in the allowed tenants list via the 'tid' claim (for multi-tenant applications)
                /// - check if the caller's account is homed or guest via the 'acct' optional claim
                /// - check if the caller belongs to right roles or groups via the 'roles' or 'groups' claim, respectively
                ///
                /// Bear in mind that you can do any of the above checks within the individual routes and/or controllers as well.
                /// For more information, visit: https://docs.microsoft.com/azure/active-directory/develop/access-tokens#validate-the-user-has-permission-to-access-this-data
                /// </summary>

                //options.Events.OnTokenValidated = async context =>
                //{
                //    string[] allowedClientApps = { /* list of client ids to allow */ };

                //    string clientappId = context?.Principal?.Claims
                //        .FirstOrDefault(x => x.Type == "azp" || x.Type == "appid")?.Value;

                //    if (!allowedClientApps.Contains(clientappId))
                //    {
                //        throw new System.Exception("This client is not authorized");
                //    }
                //};
            }, options => { builder.Configuration.Bind("AzureAd", options); });
// Add services to the container.
builder.Services.AddScoped<ValidationFilterAttribute>();

//builder.Services.AddIdentityApiEndpoints<IdentityUser>()
//    .AddEntityFrameworkStores<SchoolContext>();

builder.Services.AddControllers();
// Allowing CORS for all domains and HTTP methods for the purpose of the sample
// In production, modify this with the actual domains and HTTP methods you want to allow
builder.Services.AddCors(o => o.AddPolicy("default", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//.Services.AddAuthentication();
//builder.Services.ConfigureIdentity();
//builder.Services.ConfigureJWT(builder.Configuration);
builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = 429;
    options.AddFixedWindowLimiter(policyName: "fixed", options =>
    {
        options.PermitLimit = 3;
        options.Window = TimeSpan.FromSeconds(10);
        options.AutoReplenishment = true;
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.QueueLimit = 2;
    });
});


builder.Services.AddScoped<IStudentService, StudentService>();
//builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddDbContext<SchoolContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING"))
);
builder.Services.AddAutoMapper(typeof(Program));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.MapIdentityApi<IdentityUser>();
app.UseCors("default");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
