using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RateSetup.Helpers;
using RateSetup.Models.Database;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RateSetup.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HashtagsController : ControllerBase
    {
        private readonly EFContext _context;

        public HashtagsController(EFContext context)
        {
            _context = context;
        }

        // GET: api/Hashtags
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hashtag>>> GetHashtag()
        {
            return await _context.Hashtag.ToListAsync();
        }

        // GET: api/Hashtags/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hashtag>> GetHashtag(long id)
        {
            var hashtag = await _context.Hashtag.FindAsync(id);

            if (hashtag == null)
            {
                return NotFound();
            }

            return hashtag;
        }

        // PUT: api/Hashtags/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHashtag(long id, Hashtag hashtag)
        {
            if (id != hashtag.Id)
            {
                return BadRequest();
            }

            _context.Entry(hashtag).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HashtagExists(id))
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

        // POST: api/Hashtags
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Hashtag>> PostHashtag(Hashtag hashtag)
        {
            _context.Hashtag.Add(hashtag);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHashtag", new { id = hashtag.Id }, hashtag);
        }

        // DELETE: api/Hashtags/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHashtag(long id)
        {
            var hashtag = await _context.Hashtag.FindAsync(id);
            if (hashtag == null)
            {
                return NotFound();
            }

            _context.Hashtag.Remove(hashtag);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HashtagExists(long id)
        {
            return _context.Hashtag.Any(e => e.Id == id);
        }
    }
}
