using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mabuddy.Data;
using mabuddy.Models;

namespace mabuddy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public QuestionsApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/QuestionApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Questions>>> GetQuestion()
        {
            return await _context.Questions.OrderBy(l =>  l.Level)
                .ThenBy(s => s.Subject)
                .ToListAsync();
        }

        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<Questions>>> GetQuestion(string search)

        {
           var questions = _context.Questions.AsQueryable();

            if (search == null)
            {
                return NotFound();
            }

            questions = questions
                .Where(s => s.Subject == search);

            return await questions.OrderBy(l => l.Level)
                .ThenBy(i => i.Id)
                .ToListAsync();
        }
        
        public async Task<ActionResult<Questions>> QuizIndex(int id)
        {
            var question = await _context.Questions.FindAsync(id);

            if (question == null)
            {
                return NotFound();
            }

            return question;
        }

        // PUT: api/QuestionApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestion(int id, Questions questions)
        {
            if (id != questions.Id)
            {
                return BadRequest();
            }

            _context.Entry(questions).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionExists(id))
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
        public async Task<ActionResult<Questions>> PostQuestion(Questions questions)
        {
            _context.Questions.Add(questions);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuestion", new { id = questions.Id }, questions);
        }

        // DELETE: api/QuestionApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Questions>> DeleteQuestion(int id)
        {
            var question = await _context.Questions.FindAsync(id);
            if (question == null)
            {
                return NotFound();
            }

            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();

            return question;
        }

        private bool QuestionExists(int id)
        {
            return _context.Questions.Any(e => e.Id == id);
        }
    }
}
