using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using OptionTracker.Data;
using OptionTracker.Models;
using OptionTracker.Services;

namespace OptionTracker.Controllers
{
    [Authorize]
    public class ChainResultsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IApiService _apiService;

        public ChainResultsController(ApplicationDbContext context, IApiService apiService)
        {
            _context = context;
            _apiService = apiService;
        }

        // GET: ChainResults
        public async Task<IActionResult> Index()
        {
            return View(await _context.ChainResults.ToListAsync());
        }

        // GET: ChainResults/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var chainResult = await _context.ChainResults.Include(x => x.OptionsResults.OrderByDescending(y => y.OpenInterest))
                .FirstOrDefaultAsync(m => m.Id == id);

            var oldChainResult = await _context.ChainResults.Include(x => x.OptionsResults.OrderByDescending(y => y.OpenInterest))
                .Where(m => m.Ticker == chainResult.Ticker && m.Id != id && m.Created < chainResult.Created).OrderByDescending(y => y.Created).FirstOrDefaultAsync();

            var viewModel = new ChainResultViewModel();

            if (oldChainResult == null)
            {
                viewModel = new ChainResultViewModel
                {
                    Ticker = chainResult.Ticker,
                    Created = chainResult.Created,

                    OptionsResults = chainResult.OptionsResults
                     .Select(both => new OptionResultViewModel
                     {
                         Id = both.Id,
                         Description = both.Description,
                         OpenInterest = both.OpenInterest,
                         ClosePrice = both.ClosePrice,
                         OpenInterestChange = both.OpenInterest - both.OpenInterest,
                         ClosePriceChange = both.ClosePrice - both.ClosePrice
                     })
                     .ToList()
                };
            }
            else
            {
                viewModel = new ChainResultViewModel
                {
                    Ticker = chainResult.Ticker,
                    Created = chainResult.Created,
                    TimeChange = chainResult.Created - oldChainResult.Created,

                    OptionsResults = chainResult.OptionsResults.Zip(oldChainResult.OptionsResults, (a, b) => new { a, b })
                   .Select(both => new OptionResultViewModel
                   {
                       Id = both.a.Id,
                       Description = both.a.Description,
                       OpenInterest = both.a.OpenInterest,
                       ClosePrice = both.a.ClosePrice,
                       OpenInterestChange = both.a.OpenInterest - both.b.OpenInterest,
                       ClosePriceChange = both.a.ClosePrice - both.b.ClosePrice
                   })
                   .ToList()
                };
            }

            if (chainResult == null)
            {
                return NotFound();
            }




            return View(viewModel);
        }

        // GET: ChainResults/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ChainResults/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ticker,Created")] ChainResult chainResult)
        {
            if (ModelState.IsValid)
            {
                var optionContracts = _apiService.GetContractsByTickerName(chainResult.Ticker).ToList();

                await _context.OptionContracts.AddRangeAsync(optionContracts);
                var optionResults = _apiService.CreateResults(optionContracts);
                chainResult.OptionsResults = optionResults;

                await _context.ChainResults.AddAsync(chainResult);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(chainResult);
        }

        // GET: ChainResults/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chainResult = await _context.ChainResults.FindAsync(id);
            if (chainResult == null)
            {
                return NotFound();
            }
            return View(chainResult);
        }

        // POST: ChainResults/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ticker,Created")] ChainResult chainResult)
        {
            if (id != chainResult.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chainResult);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChainResultExists(chainResult.Id))
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
            return View(chainResult);
        }

        // GET: ChainResults/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chainResult = await _context.ChainResults
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chainResult == null)
            {
                return NotFound();
            }

            return View(chainResult);
        }

        // POST: ChainResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chainResult = await _context.ChainResults.FindAsync(id);
            _context.ChainResults.Remove(chainResult);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChainResultExists(int id)
        {
            return _context.ChainResults.Any(e => e.Id == id);
        }
    }
}
