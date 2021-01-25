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
    public class OptionResultsController : Controller
    {
        private readonly ApplicationDbContext _context;
        

        public OptionResultsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OptionResults
        public async Task<IActionResult> Index()
        {
            return View(await _context.OptionResults.ToListAsync());
        }

        // GET: OptionResults/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var optionResult = await _context.OptionResults
                .FirstOrDefaultAsync(m => m.Id == id);
            if (optionResult == null)
            {
                return NotFound();
            }

            return View(optionResult);
        }

        // GET: OptionResults/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OptionResults/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,OpenInterest,ClosePrice")] OptionResult optionResult)
        {
            if (ModelState.IsValid)
            {
                _context.Add(optionResult);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(optionResult);
        }

        // GET: OptionResults/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var optionResult = await _context.OptionResults.FindAsync(id);
            if (optionResult == null)
            {
                return NotFound();
            }
            return View(optionResult);
        }

        // POST: OptionResults/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,OpenInterest,ClosePrice")] OptionResult optionResult)
        {
            if (id != optionResult.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(optionResult);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OptionResultExists(optionResult.Id))
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
            return View(optionResult);
        }

        // GET: OptionResults/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var optionResult = await _context.OptionResults
                .FirstOrDefaultAsync(m => m.Id == id);
            if (optionResult == null)
            {
                return NotFound();
            }

            return View(optionResult);
        }

        // POST: OptionResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var optionResult = await _context.OptionResults.FindAsync(id);
            _context.OptionResults.Remove(optionResult);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OptionResultExists(int id)
        {
            return _context.OptionResults.Any(e => e.Id == id);
        }
    }
}
