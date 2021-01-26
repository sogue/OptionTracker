using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OptionTracker.Data;
using OptionTracker.Models;

namespace OptionTracker.Controllers
{
    [Authorize]
    public class TickersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TickersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tickers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Ticker.ToListAsync());
        }

        // GET: Tickers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticker = await _context.Ticker
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticker == null)
            {
                return NotFound();
            }

            return View(ticker);
        }

        // GET: Tickers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tickers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Symbol")] Ticker ticker)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ticker);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ticker);
        }

        // GET: Tickers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticker = await _context.Ticker.FindAsync(id);
            if (ticker == null)
            {
                return NotFound();
            }
            return View(ticker);
        }

        // POST: Tickers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Symbol")] Ticker ticker)
        {
            if (id != ticker.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   var defaultWatchlist = await _context.Watchlist.FirstOrDefaultAsync();
                    if( defaultWatchlist == null)
                    {
                        var watchlist = new Watchlist {TickerList = new List<Ticker>()};
                        await _context.Watchlist.AddAsync(watchlist);
                        await _context.SaveChangesAsync();
                    }

                    var updatedList = await _context.Watchlist.FirstOrDefaultAsync();

                    updatedList.TickerList.Add(ticker);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TickerExists(ticker.Id))
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
            return View(ticker);
        }

        // GET: Tickers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticker = await _context.Ticker
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticker == null)
            {
                return NotFound();
            }

            return View(ticker);
        }

        // POST: Tickers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticker = await _context.Ticker.FindAsync(id);
            _context.Ticker.Remove(ticker);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TickerExists(int id)
        {
            return _context.Ticker.Any(e => e.Id == id);
        }
    }
}
