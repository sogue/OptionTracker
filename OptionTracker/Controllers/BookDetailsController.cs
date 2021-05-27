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
    public class BookDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BookDetails
        public async Task<IActionResult> Index()
        {
            return View(await _context.BookDetails.ToListAsync());
        }

        // GET: BookDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookDetail = await _context.BookDetails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookDetail == null)
            {
                return NotFound();
            }

            return View(bookDetail);
        }

        // GET: BookDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BookDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RequestTime,underlying_price,underlying_index,timestamp,state,settlement_price,open_interest,min_price,max_price,mark_price,mark_iv,last_price,interest_rate,instrument_name,index_price,estimated_delivery_price,bid_iv,best_bid_price,best_bid_amount,best_ask_price,best_ask_amount,ask_iv")] BookDetail bookDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bookDetail);
        }

        // GET: BookDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookDetail = await _context.BookDetails.FindAsync(id);
            if (bookDetail == null)
            {
                return NotFound();
            }
            return View(bookDetail);
        }

        // POST: BookDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RequestTime,underlying_price,underlying_index,timestamp,state,settlement_price,open_interest,min_price,max_price,mark_price,mark_iv,last_price,interest_rate,instrument_name,index_price,estimated_delivery_price,bid_iv,best_bid_price,best_bid_amount,best_ask_price,best_ask_amount,ask_iv")] BookDetail bookDetail)
        {
            if (id != bookDetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookDetailExists(bookDetail.Id))
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
            return View(bookDetail);
        }

        // GET: BookDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookDetail = await _context.BookDetails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookDetail == null)
            {
                return NotFound();
            }

            return View(bookDetail);
        }

        // POST: BookDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookDetail = await _context.BookDetails.FindAsync(id);
            _context.BookDetails.Remove(bookDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookDetailExists(int id)
        {
            return _context.BookDetails.Any(e => e.Id == id);
        }
    }
}
