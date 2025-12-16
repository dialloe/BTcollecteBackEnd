using BonDeCollecte.Data;
using BonDeCollecte.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        //[Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<IEnumerable<BTCollecte>>> GetBTCollectes()
        {
           var BtCollectes = await _context.BTCollectes.Include(bt => bt.Client).ToListAsync();

            return BtCollectes;
        }

        // GET: api/BTCollectes/5
        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<BTCollecte>> GetBTCollecte(int id)
        {
            var bTCollecte = await _context.BTCollectes.Include(cl => cl.Client).FirstOrDefaultAsync(b => b.Id == id);

            if (bTCollecte == null)
            {
                return NotFound();
            }

            return bTCollecte;
        }

        // PUT: api/BTCollectes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin,Manager")]
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
        //[Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<BTCollecte>> PostBTCollecte(BTCollecte bTCollecte)
        {

            _context.BTCollectes.Add(bTCollecte);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBTCollecte", new { id = bTCollecte.Id }, bTCollecte);
        }




        // DELETE: api/BTCollectes/5
        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
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
