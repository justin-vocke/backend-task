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
                .ForMember(s => s.FullName, 
                    opt => opt.MapFrom(x => string.Join(' ', x.FirstName, x.LastName)));
        }
    }
}
