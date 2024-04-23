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
        Task<Student?> GetStudentAsync(int studentId);
        Task<IEnumerable<Student>> GetStudentsAsync();
        Task<Student> AddStudentAsync(Student student);
        Task DeleteStudentAsync(int studentId);
        Task UpdateStudentAsync(int studentId, Student student);

    }
}
