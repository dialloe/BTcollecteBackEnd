using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BonDeCollecte.Models;
using BonDeCollecte.Data;

namespace BonDeCollecte.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BTCollectesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BTCollectesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/BTCollectes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BTCollecte>>> GetBTCollectes()
        {
            return await _context.BTCollectes.Include(bt => bt.Client).ToListAsync();
        }

        // GET: api/BTCollectes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BTCollecte>> GetBTCollecte(int id)
        {
            var bTCollecte = await _context.BTCollectes.FindAsync(id);

            if (bTCollecte == null)
            {
                return NotFound();
            }

            return bTCollecte;
        }

        // PUT: api/BTCollectes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBTCollecte(int id, BTCollecte bTCollecte)
        {
            if (id != bTCollecte.Id)
            {
                return BadRequest();
            }

            _context.Entry(bTCollecte).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BTCollecteExists(id))
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

        // POST: api/BTCollectes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BTCollecte>> PostBTCollecte(BTCollecte bTCollecte)
        {
            _context.BTCollectes.Add(bTCollecte);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBTCollecte", new { id = bTCollecte.Id }, bTCollecte);
        }

        // DELETE: api/BTCollectes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBTCollecte(int id)
        {
            var bTCollecte = await _context.BTCollectes.FindAsync(id);
            if (bTCollecte == null)
            {
                return NotFound();
            }

            _context.BTCollectes.Remove(bTCollecte);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BTCollecteExists(int id)
        {
            return _context.BTCollectes.Any(e => e.Id == id);
        }
    }
}
