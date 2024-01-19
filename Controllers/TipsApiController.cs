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
    public class TipsApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TipsApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/VideoApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tips>>> GetTips()
        {
            return await _context.Tips.ToListAsync();
        }

        // GET: api/QuestionApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tips>> GetTips(int id)
        {
            var tips = await _context.Tips.FindAsync(id);

            if (tips == null)
            {
                return NotFound();
            }

            return tips;
        }

        // PUT: api/QuestionApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTips(int id, Tips tips)
        {
            if (id != tips.Id)
            {
                return BadRequest();
            }

            _context.Entry(tips).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipsExist(id))
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
        public async Task<ActionResult<Tips>> PostTips(Tips tips)
        {
            _context.Tips.Add(tips);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTips", new {id = tips.Id}, tips);
        }

        // DELETE: api/QuestionApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Tips>> DeleteTips(int id)
        {
            var tips = await _context.Tips.FindAsync(id);
            if (tips == null)
            {
                return NotFound();
            }

            _context.Tips.Remove(tips);
            await _context.SaveChangesAsync();

            return tips;
        }

        private bool TipsExist(int id)
        {
            return _context.Tips.Any(e => e.Id == id);
        }

    }
}