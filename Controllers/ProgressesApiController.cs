using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mabuddy.Data;
using mabuddy.Models;
using mabuddy.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SQLitePCL;

namespace mabuddy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgressesApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProgressesApiController(ApplicationDbContext context,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        // GET: api/ProgressesApi/5
        [HttpGet("{subject}")]
        public async Task<ActionResult<IEnumerable<Questions>>> GetProgress(string subject)
        {
            if (subject == null)
            {
                return NotFound();
            }

            var progress = new Progress();
            var quiz = new Quiz();


            ApplicationUser currentUser = null;
            currentUser = await _userManager.GetUserAsync(User);

            var questions = _context.Questions.AsQueryable();

            questions = questions
                .Where(q => q.Subject == subject && q.Level == currentUser.Level);

            var user = _userManager.GetUserId(User);
            quiz.ApplicationUserId = user;

            quiz.Questions = questions.ToList();

            return await questions.ToListAsync();
        }

        // PUT: api/ProgressesApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProgress(int id, Progress progress)
        {
            _context.Entry(progress).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProgressExists(id))
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

        // POST: api/ProgressesApi
        [HttpPost("{subject}")]
        public async Task<ActionResult<IEnumerable<Questions>>> PostProgress(string subject, Progress progress)
        {
            if (subject == null)
            {
                return NotFound();
            }

            var question = new Questions();
            var quiz = new Quiz();


            ApplicationUser currentUser = null;
            currentUser = await _userManager.GetUserAsync(User);

            var questions = _context.Questions.AsQueryable();

            questions = questions
                .Where(q => q.Subject == subject && q.Level == currentUser.Level);

            if (QuizExistsProgress(currentUser.Level, subject, currentUser.Email))
            {
                var idProgress = _context.Progresses.Where(p => p.level == currentUser.Level &&
                                                                p.subject == subject &&
                                                                p.userEmail == currentUser.Email)
                    .Select(i => i.Id)
                    .FirstOrDefault();


                progress.Id = idProgress;
                progress.subject = subject;
                progress.level = currentUser.Level;
                progress.userEmail = currentUser.Email;

                await PutProgress(idProgress, progress);

                return await questions.ToListAsync();
            }


            var user = _userManager.GetUserId(User);
            quiz.ApplicationUserId = user;
            quiz.userEmail = currentUser.Email;

            quiz.Questions = questions.ToList();

            progress.Quiz = quiz;
            progress.subject = subject;
            progress.level = currentUser.Level;
            progress.userEmail = currentUser.Email;

            _context.Progresses.Add(progress);
            _context.Quiz.Add(quiz);


            _context.Progresses.Add(progress);
            await _context.SaveChangesAsync();


            return CreatedAtAction("GetProgress", new {id = progress.Id}, progress);
        }

        // DELETE: api/ProgressesApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Progress>> DeleteProgress(int id)
        {
            var progress = await _context.Progresses.FindAsync(id);
            if (progress == null)
            {
                return NotFound();
            }

            _context.Progresses.Remove(progress);
            await _context.SaveChangesAsync();

            return progress;
        }

        private bool ProgressExists(int id)
        {
            return _context.Progresses.Any(e => e.Id == id);
        }

        private bool QuizExistsProgress(int level, string subject, string email)
        {
            return _context.Progresses.Any(p => p.level == level && p.subject == subject
                                                                 && p.userEmail == email);
        }
    }
}