using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OptionTracker.Data;
using OptionTracker.Models.Crypto;

namespace OptionTracker.Controllers
{
    public class InstrumentHistoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InstrumentHistoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: InstrumentHistories
        public async Task<IActionResult> Index()
        {
            return View(await _context.InstrumentHistories.ToListAsync());
        }

        // GET: InstrumentHistories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instrumentHistory = await _context.InstrumentHistories.Include(x=>x.BookSummaries)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (instrumentHistory == null)
            {
                return NotFound();
            }

            return View(instrumentHistory);
        }

        // GET: InstrumentHistories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: InstrumentHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,InstrumentName")] InstrumentHistory instrumentHistory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(instrumentHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(instrumentHistory);
        }

        // GET: InstrumentHistories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instrumentHistory = await _context.InstrumentHistories.FindAsync(id);
            if (instrumentHistory == null)
            {
                return NotFound();
            }
            return View(instrumentHistory);
        }

        // POST: InstrumentHistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,InstrumentName")] InstrumentHistory instrumentHistory)
        {
            if (id != instrumentHistory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(instrumentHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstrumentHistoryExists(instrumentHistory.Id))
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
            return View(instrumentHistory);
        }

        // GET: InstrumentHistories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instrumentHistory = await _context.InstrumentHistories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (instrumentHistory == null)
            {
                return NotFound();
            }

            return View(instrumentHistory);
        }

        // POST: InstrumentHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var instrumentHistory = await _context.InstrumentHistories.FindAsync(id);
            _context.InstrumentHistories.Remove(instrumentHistory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InstrumentHistoryExists(int id)
        {
            return _context.InstrumentHistories.Any(e => e.Id == id);
        }
    }
}
