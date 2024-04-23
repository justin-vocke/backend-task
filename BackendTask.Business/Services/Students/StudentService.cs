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

        public async Task DeleteStudentAsync(int studentId)
        {
            var student = await GetStudentAsync(studentId);
            if (student is not null)
            {
                await _studentRepository.DeleteStudent(studentId);
            }
        }

        public async Task<Student?> GetStudentAsync(int studentId)
        {
            return await _studentRepository.GetStudent(studentId);
        }

        public async Task<IEnumerable<Student>> GetStudentsAsync()
        {
            return await _studentRepository.GetStudents();
        }

        public async Task UpdateStudentAsync(int studentId, Student student)
        {
            var currentStudent = await GetStudentAsync(studentId);
            if(currentStudent is not null)
            {
                currentStudent.FirstName = student.FirstName;
                currentStudent.LastName = student.LastName;
                currentStudent.Age = student.Age;
            }
            await _studentRepository.SaveChangesAsync();
            
        }
    }
}
