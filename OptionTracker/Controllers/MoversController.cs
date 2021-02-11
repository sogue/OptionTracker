using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptionTracker.Data;
using OptionTracker.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OptionTracker.Controllers
{
    public class MoversController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MoversController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("Movers/")]
        [Route("Movers/{id}")]
        public async Task<IActionResult> IndexAsync(string? id)
        {
            var chainRaws = await _context.CompareRaw.Where(x => x.CompareDate.Date > DateTime.Today.Date.AddDays(-1)).ToListAsync();

            var result = chainRaws.OrderByDescending(x => x.OpenInterest);

            if (id != null && id.Equals("vChange"))
                result = chainRaws.OrderByDescending(x => x.VolatilityChange);

            if (id != null && id.Equals("oChange"))
                result = chainRaws.OrderByDescending(x => x.OpenInterestChange);

            if (id != null && id.Equals("pChange"))
                result = chainRaws.OrderByDescending(x => x.ClosePriceChange);

            if (id != null && id.Equals("oInterest"))
                result = chainRaws.OrderByDescending(x => x.OpenInterest);

            if (id != null && id.Equals("tValue"))
                result = chainRaws.OrderByDescending(x => x.TotalValue);

            return View(result.Take(50).ToList());
        }
        // GET: Movers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var optionResultViewModel = await _context.CompareRaw
                .FirstOrDefaultAsync(m => m.Id == id);
            if (optionResultViewModel == null)
            {
                return NotFound();
            }

            return View(optionResultViewModel);
        }

        // GET: Movers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,OpenInterest,OpenInterestChange,ClosePriceChange,ClosePrice,Volatility,VolatilityChange")] OptionResultViewModel optionResultViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(optionResultViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(optionResultViewModel);
        }

        // GET: Movers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var optionResultViewModel = await _context.CompareRaw.FindAsync(id);
            if (optionResultViewModel == null)
            {
                return NotFound();
            }
            return View(optionResultViewModel);
        }

        // POST: Movers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,OpenInterest,OpenInterestChange,ClosePriceChange,ClosePrice,Volatility,VolatilityChange")] OptionResultViewModel optionResultViewModel)
        {
            if (id != optionResultViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(optionResultViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OptionResultViewModelExists(optionResultViewModel.Id))
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
            return View(optionResultViewModel);
        }

        // GET: Movers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var optionResultViewModel = await _context.CompareRaw
                .FirstOrDefaultAsync(m => m.Id == id);
            if (optionResultViewModel == null)
            {
                return NotFound();
            }

            return View(optionResultViewModel);
        }

        // POST: Movers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var optionResultViewModel = await _context.CompareRaw.FindAsync(id);
            _context.CompareRaw.Remove(optionResultViewModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OptionResultViewModelExists(int id)
        {
            return _context.CompareRaw.Any(e => e.Id == id);
        }
    }
}
