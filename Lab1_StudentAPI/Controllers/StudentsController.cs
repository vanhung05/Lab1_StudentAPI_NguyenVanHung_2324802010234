using Lab1_StudentAPI.Data;
using Lab1_StudentAPI.DTOs;
using Lab1_StudentAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab1_StudentAPI.Data;
using Lab1_StudentAPI.DTOs;
using Lab1_StudentAPI.Models;

namespace StudentAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public StudentsController(ApplicationDbContext context)
        {
            _context = context;
        }
        // This method returns a list of students.
        // GET: api/students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetStudents()
        {
            var students = await _context.Students.ToListAsync();
            return Ok(students.Select(s => new StudentDTO { Name = s.Name, Email = s.Email }));
        }
        // This method returns a single student by Id.
        // GET: api/students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDTO>> GetStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(new StudentDTO { Name = student.Name, Email = student.Email });
        }
        // This method handles creating a new Student in the database.
        // The request body should contain the student's Name and Email.
        // POST: api/students
        [HttpPost]
        public async Task<ActionResult<StudentDTO>> CreateStudent(StudentDTO studentDTO)
        {
            var student = new Student
            {
                Name = studentDTO.Name,
                Email = studentDTO.Email
            };
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            // The CreatedAtAction method returns a status code of 201 and
            // the URL of the newly created resource.
            return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, studentDTO);
        }
        // This method is used to update an existing Student.
        // You must pass the student's ID and new data in the request body.
        // PUT: api/students/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, StudentDTO studentDTO)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            student.Name = studentDTO.Name;
            student.Email = studentDTO.Email;
            _context.Entry(student).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            // NoContent() returns a 204 status code, indicating that the update was successful,
            // but there is no content to return.
            return NoContent();
        }
        // This method deletes a student from the database based on ID.
        // DELETE: api/students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            // NoContent() is returned after the student is deleted, indicating success.
            return NoContent();
        }
    }
}
