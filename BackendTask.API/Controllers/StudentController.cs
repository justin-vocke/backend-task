using BackendTask.Business.Services.Students;
using BackendTask.Data.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }
        // GET: api/<StudentController>
        [HttpGet]
        public IEnumerable<string> GetStudents()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<StudentController>/5
        [HttpGet("{id}")]
        public string GetStudent(int id)
        {
            return "value";
        }

        // POST api/<StudentController>
        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudent(Student student)
        {
            try
            {
                if (student is null)
                    return BadRequest();

                var createdStudent = await _studentService.AddStudentAsync(student);

                return CreatedAtAction(nameof(CreateStudent), new { id = createdStudent.Id}, student);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new employee record");
            }
        }

        // PUT api/<StudentController>/5
        [HttpPut("{id}")]
        public void UpdateStudent(int id, [FromBody] string value)
        {
        }

        // DELETE api/<StudentController>/5
        [HttpDelete("{id}")]
        public void DeleteStudent(int id)
        {
        }
    }
}
