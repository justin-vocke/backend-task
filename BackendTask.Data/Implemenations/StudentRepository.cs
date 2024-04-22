using BackendTask.Data.Contracts;
using BackendTask.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BackendTask.Data.Implemenations
{
    public class StudentRepository : IStudentRepository
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

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateStudent(int studentId, Student student)
        {
            throw new NotImplementedException();
        }
    }
}
