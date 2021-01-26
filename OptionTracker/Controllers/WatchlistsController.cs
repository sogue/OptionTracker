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
using OptionTracker.Services;

namespace OptionTracker.Controllers
{
    [Authorize]
    public class WatchlistsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IApiService _apiService;

        public WatchlistsController(ApplicationDbContext context, IApiService apiService)
        {
            _context = context;
            _apiService = apiService;
        }

        // GET: Watchlists
        public async Task<IActionResult> Index()
        {
            return View(await _context.Watchlist.ToListAsync());
        }

        // GET: Watchlists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var watchlist = await _context.Watchlist
                .FirstOrDefaultAsync(m => m.Id == id);
            if (watchlist == null)
            {
                return NotFound();
            }

            return View(watchlist);
        }

        // GET: Watchlists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Watchlists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] Watchlist watchlist)
        {
            if (ModelState.IsValid)
            {
                var allTickers =  _context.Ticker.AsQueryable().ToList();
                watchlist.TickerList = allTickers;
                _context.Add(watchlist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(watchlist);
        }

        // GET: Watchlists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var watchlist = await _context.Watchlist.FindAsync(id);
            if (watchlist == null)
            {
                return NotFound();
            }
            return View(watchlist);
        }

        // POST: Watchlists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] Watchlist watchlist)
        {
            if (id != watchlist.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    foreach (var ticker in watchlist.TickerList)
                    {
                        var optionContracts = _apiService.GetContractsByTickerName(ticker.Symbol).ToList();

                        await _context.OptionContracts.AddRangeAsync(optionContracts);
                        var optionResults = _apiService.CreateResults(optionContracts);

                        var chainResult = new ChainResult();

                        chainResult.OptionsResults = optionResults;

                        await _context.ChainResults.AddAsync(chainResult);

                        await _context.SaveChangesAsync();
                    }
                    _context.Update(watchlist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WatchlistExists(watchlist.Id))
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
            return View(watchlist);
        }

        // GET: Watchlists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var watchlist = await _context.Watchlist
                .FirstOrDefaultAsync(m => m.Id == id);
            if (watchlist == null)
            {
                return NotFound();
            }

            return View(watchlist);
        }

        // POST: Watchlists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var watchlist = await _context.Watchlist.FindAsync(id);
            _context.Watchlist.Remove(watchlist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WatchlistExists(int id)
        {
            return _context.Watchlist.Any(e => e.Id == id);
        }
    }
}
