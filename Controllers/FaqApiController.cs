using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using mabuddy.Data;
using mabuddy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecuritySwitch.Abstractions;

namespace mabuddy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaqApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FaqApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/VideoApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HomeModel>>> GetFaq()
        {
            return await _context.Home.ToListAsync();
        }

        // GET: api/QuestionApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HomeModel>> GetFaq(int id)
        {
            var home = await _context.Home.FindAsync(id);

            if (home == null)
            {
                return NotFound();
            }

            return home;
        }

        // PUT: api/QuestionApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestion(int id, HomeModel homeModel)
        {
            if (id != homeModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(homeModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FaqExist(id))
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
        public async Task<ActionResult<HomeModel>> PostQuestion(HomeModel homeModel)
        {
            _context.Home.Add(homeModel);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFaq", new {id = homeModel.Id}, homeModel);
        }

        // DELETE: api/QuestionApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HomeModel>> DeleteQuestion(int id)
        {
            var faq = await _context.Home.FindAsync(id);
            if (faq == null)
            {
                return NotFound();
            }

            _context.Home.Remove(faq);
            await _context.SaveChangesAsync();

            return faq;
        }

        private bool FaqExist(int id)
        {
            return _context.Home.Any(e => e.Id == id);
        }

    }
}