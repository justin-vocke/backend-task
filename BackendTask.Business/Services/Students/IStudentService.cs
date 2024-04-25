using BackendTask.Business.DTOs;
using BackendTask.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTask.Business.Services.Students
{
    public interface IStudentService
    {
        Task<StudentDto?> GetStudentAsync(int studentId);
        Task<IEnumerable<StudentDto>> GetStudentsAsync();
        Task<StudentDto> AddStudentAsync(StudentForCreationDto student);
        Task DeleteStudentAsync(int studentId);
        Task UpdateStudentAsync(int studentId, Student student);

    }
}
