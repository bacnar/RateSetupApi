﻿using Microsoft.AspNetCore.Mvc;
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
    public class SetupsController : ControllerBase
    {
        private readonly EFContext _context;

        public SetupsController(EFContext context)
        {
            _context = context;
        }

        // GET: api/Setups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Setup>>> GetSetup()
        {
            return await _context.Setup.ToListAsync();
        }

        // GET: api/Setups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Setup>> GetSetup(long id)
        {
            var setup = await _context.Setup.FindAsync(id);

            if (setup == null)
            {
                return NotFound();
            }

            return setup;
        }

        // PUT: api/Setups/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSetup(long id, Setup setup)
        {
            if (id != setup.Id)
            {
                return BadRequest();
            }

            _context.Entry(setup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SetupExists(id))
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

        // POST: api/Setups
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Setup>> PostSetup(Setup setup)
        {
            _context.Setup.Add(setup);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSetup", new { id = setup.Id }, setup);
        }

        // DELETE: api/Setups/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSetup(long id)
        {
            var setup = await _context.Setup.FindAsync(id);
            if (setup == null)
            {
                return NotFound();
            }

            _context.Setup.Remove(setup);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SetupExists(long id)
        {
            return _context.Setup.Any(e => e.Id == id);
        }
    }
}
