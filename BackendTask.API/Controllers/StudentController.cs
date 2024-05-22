using BackendTask.Business.DTOs;
using BackendTask.Business.Services.Students;
using BackendTask.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendTask.API.Controllers
{
    
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    [EnableRateLimiting("fixed")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }
        // GET: api/<StudentController>
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetStudents()
        {
            try
            {
                var students = await _studentService.GetStudentsAsync();
                return Ok(students);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new employee record");
            }
        }

        // GET api/<StudentController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            try
            {
                var student = await _studentService.GetStudentAsync(id);
                return Ok(student);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new employee record");
            }
        }

        // POST api/<StudentController>
        [HttpPost]
        public async Task<IActionResult> CreateStudent(StudentForCreationDto student)
        {
            try
            {
                if (student is null)
                    return BadRequest();

                var createdStudent = await _studentService.AddStudentAsync(student);

                return CreatedAtRoute(nameof(GetStudent), new { id = createdStudent.Id}, student);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new employee record");
            }
        }

        // PUT api/<StudentController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, Student student)
        {
            try
            {
                if (student is null)
                    return BadRequest();

                await _studentService.UpdateStudentAsync(id, student);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new employee record");
            }
        }

        // DELETE api/<StudentController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            try
            {
                await _studentService.DeleteStudentAsync(id);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting employee record");
            }
        }
    }
}
