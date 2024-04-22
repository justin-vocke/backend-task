﻿using BackendTask.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTask.Business.Services.Students
{
    public interface IStudentService
    {
        Task<Student> GetStudent(int studentId);
        Task<IEnumerable<Student>> GetStudents();
        Task AddStudent(Student student);
        Task DeleteStudent(int studentId);
        Task UpdateStudent(int studentId, Student student);

    }
}
