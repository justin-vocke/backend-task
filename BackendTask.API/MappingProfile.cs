using AutoMapper;
using BackendTask.Business.DTOs;
using BackendTask.Data.Models;

namespace BackendTask.API
{
    public class MappingProfile: Profile
    {
        public MappingProfile() 
        {
            CreateMap<Student, StudentDto>()
                .ForCtorParam("FullName", 
                    opt => opt.MapFrom(x => string.Join(' ', x.FirstName, x.LastName)));

            CreateMap<UserForRegistrationDto, User>();

            CreateMap<StudentForCreationDto, Student>();    
        }
    }
}
