using BackendTask.Business.DTOs;
using BackendTask.Business.Services.Students;
using BackendTask.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Identity.Web;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendTask.API.Controllers
{
    
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    [EnableRateLimiting("fixed")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        /// <summary>
        /// The 'oid' (object id) is the only claim that should be used to uniquely identify
        /// a user in an Azure AD tenant. The token might have one or more of the following claim,
        /// that might seem like a unique identifier, but is not and should not be used as such:
        ///
        /// - upn (user principal name): might be unique amongst the active set of users in a tenant
        /// but tend to get reassigned to new employees as employees leave the organization and others
        /// take their place or might change to reflect a personal change like marriage.
        ///
        /// - email: might be unique amongst the active set of users in a tenant but tend to get reassigned
        /// to new employees as employees leave the organization and others take their place.
        /// </summary>
        private Guid GetUserId()
        {
            
            if (!Guid.TryParse(HttpContext.User.GetObjectId(), out Guid userId))
            {
                throw new Exception("User ID is not valid.");
            }

            return userId;
        }

        /// <summary>
        /// Access tokens that have neither the 'scp' (for delegated permissions) nor
        /// 'roles' (for application permissions) claim are not to be honored.
        ///
        /// An access token issued by Azure AD will have at least one of the two claims. Access tokens
        /// issued to a user will have the 'scp' claim. Access tokens issued to an application will have
        /// the roles claim. Access tokens that contain both claims are issued only to users, where the scp
        /// claim designates the delegated permissions, while the roles claim designates the user's role.
        ///
        /// To determine whether an access token was issued to a user (i.e delegated) or an application
        /// more easily, we recommend enabling the optional claim 'idtyp'. For more information, see:
        /// https://docs.microsoft.com/azure/active-directory/develop/access-tokens#user-and-application-tokens
        /// </summary>
        private bool IsAppMakingRequest()
        {
            // Add in the optional 'idtyp' claim to check if the access token is coming from an application or user.
            // See: https://docs.microsoft.com/en-us/azure/active-directory/develop/active-directory-optional-claims
            if (HttpContext.User.Claims.Any(c => c.Type == "idtyp"))
            {
                return HttpContext.User.Claims.Any(c => c.Type == "idtyp" && c.Value == "app");
            }
            else
            {
                // alternatively, if an AT contains the roles claim but no scp claim, that indicates it's an app token
                return HttpContext.User.Claims.Any(c => c.Type == "roles") && !HttpContext.User.Claims.Any(c => c.Type == "scp");
            }
        }
        // GET: api/<StudentController>

        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetStudents()
        {
            try
            {
                var students = await _studentService.GetStudentsAsync();
                return Ok(students);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new employee record");
            }
        }

        // GET api/<StudentController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            try
            {
                var student = await _studentService.GetStudentAsync(id);
                return Ok(student);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new employee record");
            }
        }

        // POST api/<StudentController>
        [HttpPost]
        public async Task<IActionResult> CreateStudent(StudentForCreationDto student)
        {
            try
            {
                if (student is null)
                    return BadRequest();

                var createdStudent = await _studentService.AddStudentAsync(student);

                return CreatedAtRoute(nameof(GetStudent), new { id = createdStudent.Id}, student);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new employee record");
            }
        }

        // PUT api/<StudentController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, Student student)
        {
            try
            {
                if (student is null)
                    return BadRequest();

                await _studentService.UpdateStudentAsync(id, student);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new employee record");
            }
        }

        // DELETE api/<StudentController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            try
            {
                await _studentService.DeleteStudentAsync(id);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting employee record");
            }
        }
    }
}
