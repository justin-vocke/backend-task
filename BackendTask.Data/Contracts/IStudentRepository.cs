﻿using BackendTask.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTask.Data.Contracts
{
    public interface IStudentRepository
    {
        Task<Student?> GetStudent(int studentId);
        Task<IEnumerable<Student>> GetStudents();
        Task<Student> AddStudent(Student student);
        Task DeleteStudent(int studentId);
        Task UpdateStudent(Student currentStudent, Student student);
        Task SaveChangesAsync();
    }
}
