using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SwordAPI.Models;

namespace SwordAPI.Controllers       //defining Http methods
{
    [Route("api/[controller]")]
    [ApiController]
    public class SwordController : ControllerBase
    {
        private readonly SwordContext _dbContext;

        public SwordController(SwordContext dbContext)
        {
            _dbContext = dbContext;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Swords>>> GetSwords()
        {
            if (_dbContext.Swords == null)
            {
                return NotFound();
            }
            return await _dbContext.Swords.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Swords>> GetSword(int id)
        {
            if (_dbContext.Swords == null)

            {
                return NotFound();
            }
            var sword = await _dbContext.Swords.FindAsync(id);
            if(sword == null)
            {
                return NotFound();
            }

            return sword;
        }
        [HttpPost]
        public async Task<ActionResult<Swords>> PostSword(Swords sword)
        {
            _dbContext.Swords.Add(sword);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSword), new { id = sword.ID }, sword);
        }
        [HttpPut]
        public async Task<IActionResult> PutSword(int id, Swords sword)
        {
            if(id != sword.ID)
            {
                return BadRequest();
            }
            _dbContext.Entry(sword).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();

            }
            catch(DbUpdateConcurrencyException)
            {
                if (!SwordAvailable(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok();
        }

        private bool SwordAvailable(int id)
        {
            return (_dbContext.Swords?.Any(x => x.ID == id)).GetValueOrDefault();    
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSword(int id)
        {
            if(_dbContext.Swords == null)
            {
                return NotFound();
            }
            var sword = await _dbContext.Swords.FindAsync(id);
            if(sword == null)
            {
                return NotFound();
            }
            _dbContext.Swords.Remove(sword);
            await _dbContext.SaveChangesAsync();

            return Ok();

        }


    }
}
