using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BikeStoreAPI2_WIP_.Models;

namespace BikeStoreAPI2_WIP_.Controllers
{
    [Route("api/Bikes")]
    [ApiController]
    public class BikesController : ControllerBase
    {
        private readonly BikeStoreDBContext _context;

        public BikesController(BikeStoreDBContext context)
        {
            _context = context;
        }

        // GET: api/Bikes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bikes>>> GetBikes()
        {
            return await _context.Bikes.ToListAsync();
        }

        // GET: api/Bikes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bikes>> GetBikes(int id)
        {
            var bikes = await _context.Bikes.FindAsync(id);

            if (bikes == null)
            {
                return NotFound();
            }

            return bikes;
        }

        // PUT: api/Bikes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBikes(int id, Bikes bikes)
        {
            if (id != bikes.BikeID)
            {
                return BadRequest();
            }

            _context.Entry(bikes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BikesExists(id))
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

        // POST: api/Bikes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Bikes>> PostBikes(Bikes bikes)
        {
            _context.Bikes.Add(bikes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBikes", new { id = bikes.BikeID }, bikes);
        }

        // DELETE: api/Bikes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Bikes>> DeleteBikes(int id)
        {
            var bikes = await _context.Bikes.FindAsync(id);
            if (bikes == null)
            {
                return NotFound();
            }

            _context.Bikes.Remove(bikes);
            await _context.SaveChangesAsync();

            return bikes;
        }

        private bool BikesExists(int id)
        {
            return _context.Bikes.Any(e => e.BikeID == id);
        }
    }
}
