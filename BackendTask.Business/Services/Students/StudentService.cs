using BackendTask.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTask.Business.Services.Students
{
    public class StudentService : IStudentService
    {
        public Task AddStudent(Student student)
        {
            throw new NotImplementedException();
        }

        public Task DeleteStudent(int studentId)
        {
            throw new NotImplementedException();
        }

        public Task<Student> GetStudent(int studentId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Student>> GetStudents()
        {
            throw new NotImplementedException();
        }

        public Task UpdateStudent(int studentId, Student student)
        {
            throw new NotImplementedException();
        }
    }
}
