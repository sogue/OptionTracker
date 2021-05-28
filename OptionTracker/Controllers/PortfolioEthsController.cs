using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OptionTracker.Data;
using Org.OpenAPITools.Models;

namespace OptionTracker.Controllers
{
    [Authorize]
    public class PortfolioEthsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PortfolioEthsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PortfolioEths
        public async Task<IActionResult> Index()
        {
            return View(await _context.PortfoliosEth.ToListAsync());
        }

        // GET: PortfolioEths/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioEth = await _context.PortfoliosEth
                .FirstOrDefaultAsync(m => m.Id == id);
            if (portfolioEth == null)
            {
                return NotFound();
            }

            return View(portfolioEth);
        }

        // GET: PortfolioEths/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PortfolioEths/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MaintenanceMargin,AvailableWithdrawalFunds,InitialMargin,AvailableFunds,Currency,MarginBalance,Equity,Balance")] PortfolioEth portfolioEth)
        {
            if (ModelState.IsValid)
            {
                _context.Add(portfolioEth);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(portfolioEth);
        }

        // GET: PortfolioEths/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioEth = await _context.PortfoliosEth.FindAsync(id);
            if (portfolioEth == null)
            {
                return NotFound();
            }
            return View(portfolioEth);
        }

        // POST: PortfolioEths/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MaintenanceMargin,AvailableWithdrawalFunds,InitialMargin,AvailableFunds,Currency,MarginBalance,Equity,Balance")] PortfolioEth portfolioEth)
        {
            if (id != portfolioEth.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(portfolioEth);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PortfolioEthExists(portfolioEth.Id))
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
            return View(portfolioEth);
        }

        // GET: PortfolioEths/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioEth = await _context.PortfoliosEth
                .FirstOrDefaultAsync(m => m.Id == id);
            if (portfolioEth == null)
            {
                return NotFound();
            }

            return View(portfolioEth);
        }

        // POST: PortfolioEths/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var portfolioEth = await _context.PortfoliosEth.FindAsync(id);
            _context.PortfoliosEth.Remove(portfolioEth);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PortfolioEthExists(int id)
        {
            return _context.PortfoliosEth.Any(e => e.Id == id);
        }
    }
}
