using BackendTask.Data.Contracts;
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
        private readonly IStudentRepository _studentRepository;
        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public async Task<Student> AddStudentAsync(Student student)
        {
            
            return await _studentRepository.AddStudent(student);
            
        }

        public Task DeleteStudentAsync(int studentId)
        {
            throw new NotImplementedException();
        }

        public Task<Student> GetStudentAsync(int studentId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Student>> GetStudentsAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateStudentAsync(int studentId, Student student)
        {
            throw new NotImplementedException();
        }
    }
}
