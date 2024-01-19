using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mabuddy.Data;
using mabuddy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace mabuddy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddLessonApi: ControllerBase
    {
         private readonly ApplicationDbContext _context;

        public AddLessonApi(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/VideoApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddLesson>>> GetLesson()
        {
            return await _context.AddLesson.ToListAsync();
        }

        // GET: api/QuestionApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AddLesson>> GetLesson(int id)
        {
            var home = await _context.AddLesson.FindAsync(id);

            if (home == null)
            {
                return NotFound();
            }

            return home;
        }

        // PUT: api/QuestionApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLesson(int id, AddLesson addLesson)
        {
            if (id != addLesson.Id)
            {
                return BadRequest();
            }

            _context.Entry(addLesson).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LessonExist(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/QuestionApi
        [HttpPost]
        public async Task<ActionResult<AddLesson>> PostLesson(AddLesson addLesson)
        {
            _context.AddLesson.Add(addLesson);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLesson", new {id = addLesson.Id}, addLesson);
        }

        // DELETE: api/QuestionApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AddLesson>> DeleteLesson(int id)
        {
            var lesson = await _context.AddLesson.FindAsync(id);
            if (lesson == null)
            {
                return NotFound();
            }

            _context.AddLesson.Remove(lesson);
            await _context.SaveChangesAsync();

            return lesson;
        }

        private bool LessonExist(int id)
        {
            return _context.AddLesson.Any(e => e.Id == id);
        }

    }
    
}