using AutoMapper;
using BackendTask.Business.DTOs;
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
        private readonly IMapper _mapper;
        public StudentService(IStudentRepository studentRepository,
            IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }
        public async Task<StudentDto> AddStudentAsync(StudentForCreationDto student)
        {
            try
            {
                var newStudentEntity = _mapper.Map<Student>(student);
                var newStudent = await _studentRepository.AddStudent(newStudentEntity);

                var studentToReturn = _mapper.Map<StudentDto>(newStudentEntity);
                return studentToReturn;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong in the {nameof(AddStudentAsync)} service method {ex}");
                throw;
            }
            
        }

        public async Task DeleteStudentAsync(int studentId)
        {
            var student = await GetStudentAsync(studentId);
            if (student is not null)
            {
                await _studentRepository.DeleteStudent(studentId);
            }
        }

        public async Task<StudentDto?> GetStudentAsync(int studentId)
        {
            try
            {
                var student = await _studentRepository.GetStudent(studentId);

                var studentDto = _mapper.Map<StudentDto>(student);

                return studentDto;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Something went wrong in the {nameof(GetStudentAsync)} service method {ex}");
                throw;
            }
            

        }

        public async Task<IEnumerable<StudentDto>> GetStudentsAsync()
        {
            try
            {
                var students = await _studentRepository.GetStudents();
                var studentsDto = _mapper.Map<IEnumerable<StudentDto>>(students);
                return studentsDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong in the {nameof(GetStudentsAsync)} service method {ex}");
                throw;
            }
            
        }

        public async Task UpdateStudentAsync(int studentId, Student student)
        {
            var currentStudent = await _studentRepository.GetStudent(studentId);
            if (currentStudent is not null)
            {
                currentStudent.FirstName = student.FirstName;
                currentStudent.LastName = student.LastName;
                currentStudent.Age = student.Age;
            }
            await _studentRepository.SaveChangesAsync();
            
        }
    }
}
