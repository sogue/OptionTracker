using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OptionTracker.Data;
using OptionTracker.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptionTracker.Controllers
{
    public class DateChainsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DateChainsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DateChains
        [HttpGet("DateChains/")]
        [Route("DateChains/{ticker}")]
        public async Task<IActionResult> Index(string? ticker)
        {
            var chainRaw = await _context.OptionChainRaw.Where(x =>
                    x.Data.RootElement.GetProperty("symbol").GetString() == ticker)
                .OrderByDescending(x => x.Id).FirstOrDefaultAsync();


            var dates = JsonConvert
                .DeserializeObject<Dictionary<string, Dictionary<string, OptionContract[]>>>(
                    chainRaw.Data.RootElement.GetProperty("callExpDateMap").ToString() ?? "");


            var list = dates.Select(dat => new DateChainViewModel
            {
                Symbol = ticker,
                ExpDate = dat.Key.Split(":").First(),
                Strikes = dat.Value.SelectMany(x => x.Value).Select(x => x.StrikePrice).ToArray()
            }).ToList();

            return View(list);
        }

        // GET: DateChains/Details/5
        [HttpGet("DateChains/{ticker}/{date}")]
        public async Task<IActionResult> Details(string ticker, string? date)
        {
            if (date == null)
            {
                return NotFound();
            }

            var result = await _context.OptionChainRaw.FirstOrDefaultAsync(x => x.Data.RootElement.GetProperty("symbol").GetString().Equals(ticker));

            var dates = JsonConvert
                .DeserializeObject<Dictionary<string, Dictionary<string, OptionContract[]>>>(
                    result.Data.RootElement.GetProperty("callExpDateMap").ToString() ?? "");


            var list = new List<DateChain>();
            foreach (var dat in dates)
            {
                list.Add(
                    new DateChain()
                    {
                        Symbol = ticker,
                        ExpDate = dat.Key.Split(":").First(),
                        OptionContracts = dat.Value.SelectMany(x => x.Value).ToList()
                    });
            }

            var rView = list.First(x => x.ExpDate.Equals(date)).OptionContracts.OrderByDescending(x => x.OpenInterest).ToList() ?? new List<OptionContract>();


            return View(rView);
        }

        // GET: DateChains/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DateChains/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Symbol,ExpDate,OptionContracts")] DateChain dateChain)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dateChain);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dateChain);
        }

        // GET: DateChains/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dateChain = await _context.DateChain.FindAsync(id);
            if (dateChain == null)
            {
                return NotFound();
            }
            return View(dateChain);
        }

        // POST: DateChains/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Symbol,ExpDate,OptionContracts")] DateChain dateChain)
        {
            if (id != dateChain.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dateChain);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DateChainExists(dateChain.Id))
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
            return View(dateChain);
        }

        // GET: DateChains/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dateChain = await _context.DateChain
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dateChain == null)
            {
                return NotFound();
            }

            return View(dateChain);
        }

        // POST: DateChains/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dateChain = await _context.DateChain.FindAsync(id);
            _context.DateChain.Remove(dateChain);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DateChainExists(int id)
        {
            return _context.DateChain.Any(e => e.Id == id);
        }
    }
}
