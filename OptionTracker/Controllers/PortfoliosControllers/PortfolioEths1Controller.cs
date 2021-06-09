using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptionTracker.Data;
using Org.OpenAPITools.Models;

namespace OptionTracker.Controllers.PortfoliosControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioEths1Controller : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PortfolioEths1Controller(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PortfolioEths1
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PortfolioEth>>> GetPortfoliosEth()
        {
            return await _context.PortfoliosEth.ToListAsync();
        }

        // GET: api/PortfolioEths1/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PortfolioEth>> GetPortfolioEth(int id)
        {
            var portfolioEth = await _context.PortfoliosEth.FindAsync(id);

            if (portfolioEth == null)
            {
                return NotFound();
            }

            return portfolioEth;
        }

        // PUT: api/PortfolioEths1/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPortfolioEth(int id, PortfolioEth portfolioEth)
        {
            if (id != portfolioEth.Id)
            {
                return BadRequest();
            }

            _context.Entry(portfolioEth).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PortfolioEthExists(id))
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

        // POST: api/PortfolioEths1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PortfolioEth>> PostPortfolioEth(PortfolioEth portfolioEth)
        {
            _context.PortfoliosEth.Add(portfolioEth);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPortfolioEth", new { id = portfolioEth.Id }, portfolioEth);
        }

        // DELETE: api/PortfolioEths1/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePortfolioEth(int id)
        {
            var portfolioEth = await _context.PortfoliosEth.FindAsync(id);
            if (portfolioEth == null)
            {
                return NotFound();
            }

            _context.PortfoliosEth.Remove(portfolioEth);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PortfolioEthExists(int id)
        {
            return _context.PortfoliosEth.Any(e => e.Id == id);
        }
    }
}
