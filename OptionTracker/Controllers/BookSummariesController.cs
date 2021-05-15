using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OptionTracker.Data;
using Org.OpenAPITools.Models;

namespace OptionTracker.Controllers
{
    public class BookSummariesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookSummariesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BookSummaries
        public async Task<IActionResult> Index()
        {
            return View(await _context.BookSummaries.OrderByDescending(x=>x.OpenInterest).ToListAsync());
        }

        // GET: BookSummaries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookSummary = await _context.BookSummaries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookSummary == null)
            {
                return NotFound();
            }

            return View(bookSummary);
        }

        // GET: BookSummaries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BookSummaries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,InstrumentId,UnderlyingIndex,Volume,VolumeUsd,UnderlyingPrice,BidPrice,OpenInterest,QuoteCurrency,High,EstimatedDeliveryPrice,Last,MidPrice,InterestRate,Funding8h,MarkPrice,AskPrice,InstrumentName,Low,BaseCurrency,CreationTimestamp,CurrentFunding")] BookSummary bookSummary)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookSummary);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bookSummary);
        }

        // GET: BookSummaries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookSummary = await _context.BookSummaries.FindAsync(id);
            if (bookSummary == null)
            {
                return NotFound();
            }
            return View(bookSummary);
        }

        // POST: BookSummaries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,InstrumentId,UnderlyingIndex,Volume,VolumeUsd,UnderlyingPrice,BidPrice,OpenInterest,QuoteCurrency,High,EstimatedDeliveryPrice,Last,MidPrice,InterestRate,Funding8h,MarkPrice,AskPrice,InstrumentName,Low,BaseCurrency,CreationTimestamp,CurrentFunding")] BookSummary bookSummary)
        {
            if (id != bookSummary.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookSummary);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookSummaryExists(bookSummary.Id))
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
            return View(bookSummary);
        }

        // GET: BookSummaries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookSummary = await _context.BookSummaries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookSummary == null)
            {
                return NotFound();
            }

            return View(bookSummary);
        }

        // POST: BookSummaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookSummary = await _context.BookSummaries.FindAsync(id);
            _context.BookSummaries.Remove(bookSummary);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookSummaryExists(int id)
        {
            return _context.BookSummaries.Any(e => e.Id == id);
        }
    }
}
