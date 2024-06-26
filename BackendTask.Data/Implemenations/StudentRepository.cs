﻿using BackendTask.Data.Contracts;
using BackendTask.Data.DbContexts;
using BackendTask.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BackendTask.Data.Implemenations
{
    public class StudentRepository : IStudentRepository
    {
        private readonly SchoolContext _schoolContext;
        public StudentRepository(SchoolContext schoolContext)
        {
            _schoolContext = schoolContext;
        }
        public async Task<Student> AddStudent(Student student)
        {
            await _schoolContext.Students.AddAsync(student);
            await _schoolContext.SaveChangesAsync();
            return student;
        }

        public async Task DeleteStudent(int studentId)
        {
            var student = await GetStudent(studentId);
            if (student != null)
            {
                _schoolContext.Students.Remove(student);
            }
            await _schoolContext.SaveChangesAsync();
        }

        public async Task<Student?> GetStudent(int studentId)
        {
            try
            {
                var student = await _schoolContext.Students.FirstOrDefaultAsync(x => x.Id == studentId);
                return student;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
            
        }

        public async Task<IEnumerable<Student>> GetStudents()
        {
            return await _schoolContext.Students.ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _schoolContext.SaveChangesAsync();
        }

        public async Task UpdateStudent(Student currentStudent, Student student)
        {
            if (currentStudent != null)
            {
                currentStudent.FirstName = student.FirstName;
                currentStudent.LastName = student.LastName;
                currentStudent.Age = student.Age;
            }
            
        }
    }
}
