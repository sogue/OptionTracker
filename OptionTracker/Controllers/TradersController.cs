using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptionTracker.Data;
using OptionTracker.Models;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Dtos;
using Microsoft.EntityFrameworkCore.Query;

namespace OptionTracker.Controllers
{
    public class TradersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TradersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Traders
        public async Task<IActionResult> Index(int id)
        {
            Trader trader;

            if (id == null)
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                trader = await _context.Traders.Include(x => x.Tickers).FirstOrDefaultAsync(x => x.IdentityUserId == userId);

            }
            else
            {
                trader = await _context.Traders.Include(x => x.Tickers)
                    .FirstOrDefaultAsync(m => m.Id == id);
            }

            if (trader == null)
            {
                return View(new List<TickerToReturnDto>());
            }

            return View(trader.Tickers);
        }

        // GET: Traders/Details/5
        [HttpGet("Traders/Details/{id}")]
        [Route("Traders/Details")]
        public async Task<IActionResult> Details(int? id)
        {
            Trader trader;

            if (id == null)
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                trader = await _context.Traders.Include(x => x.Tickers).FirstOrDefaultAsync(x => x.IdentityUserId == userId);

            }
            else
            {
                trader = await _context.Traders.Include(x => x.Tickers)
                    .FirstOrDefaultAsync(m => m.Id == id);
            }

            if (trader == null)
            {
                return View(new List<Ticker>());
            }

            return View(trader.Tickers);
        }

        // GET: Traders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Traders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdentityUserId")] Trader trader)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trader);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trader);
        }

        // GET: Traders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trader = await _context.Traders.FindAsync(id);
            if (trader == null)
            {
                return NotFound();
            }
            return View(trader);
        }

        // POST: Traders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdentityUserId")] Trader trader)
        {
            if (id != trader.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trader);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TraderExists(trader.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(trader);
        }

        // GET: Traders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var trader = await _context.Traders.Include(x => x.Tickers).FirstOrDefaultAsync(x => x.IdentityUserId == userId);

            if (trader == null)
            {
                return NotFound();
            }


            return View(trader.Tickers.FirstOrDefault(x=>x.Id == id));
        }

        // POST: Traders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var trader = await _context.Traders
                .Include(x => x.Tickers)
                .FirstOrDefaultAsync(x => x.IdentityUserId == userId);

            var ticker = trader.Tickers.FirstOrDefault(x => x.Id == id);

            trader.Tickers.Remove(ticker);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TraderExists(int id)
        {
            return _context.Traders.Any(e => e.Id == id);
        }
    }
}
