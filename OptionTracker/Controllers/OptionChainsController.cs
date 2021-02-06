using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OptionTracker.Data;
using OptionTracker.Models;

namespace OptionTracker.Controllers
{
    public class OptionChainsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OptionChainsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OptionChains
        public async Task<IActionResult> Index()
        {
            return View(await _context.OptionChain.ToListAsync());
        }

        // GET: OptionChains/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var optionChain = await _context.OptionChain
                .FirstOrDefaultAsync(m => m.Id == id);
            if (optionChain == null)
            {
                return NotFound();
            }

            return View(optionChain);
        }

        // GET: OptionChains/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OptionChains/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Symbol,UnderlyingPrice,Created,OptionContracts")] OptionChain optionChain)
        {
            if (ModelState.IsValid)
            {
                _context.Add(optionChain);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(optionChain);
        }

        // GET: OptionChains/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var optionChain = await _context.OptionChain.FindAsync(id);
            if (optionChain == null)
            {
                return NotFound();
            }
            return View(optionChain);
        }

        // POST: OptionChains/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Symbol,UnderlyingPrice,Created,OptionContracts")] OptionChain optionChain)
        {
            if (id != optionChain.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(optionChain);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OptionChainExists(optionChain.Id))
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
            return View(optionChain);
        }

        // GET: OptionChains/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var optionChain = await _context.OptionChain
                .FirstOrDefaultAsync(m => m.Id == id);
            if (optionChain == null)
            {
                return NotFound();
            }

            return View(optionChain);
        }

        // POST: OptionChains/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var optionChain = await _context.OptionChain.FindAsync(id);
            _context.OptionChain.Remove(optionChain);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OptionChainExists(int id)
        {
            return _context.OptionChain.Any(e => e.Id == id);
        }
    }
}
