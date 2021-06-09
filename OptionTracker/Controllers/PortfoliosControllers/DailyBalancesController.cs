using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptionTracker.Data;
using Org.OpenAPITools.Models;

namespace OptionTracker.Controllers.PortfoliosControllers
{
    public class DailyBalancesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DailyBalancesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DailyBalances
        public async Task<IActionResult> Index()
        {
            return View(await _context.DailyBalances.ToListAsync());
        }

        // GET: DailyBalances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dailyBalance = await _context.DailyBalances
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dailyBalance == null)
            {
                return NotFound();
            }

            return View(dailyBalance);
        }

        // GET: DailyBalances/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DailyBalances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BalanceDate")] DailyBalance dailyBalance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dailyBalance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dailyBalance);
        }

        // GET: DailyBalances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dailyBalance = await _context.DailyBalances.FindAsync(id);
            if (dailyBalance == null)
            {
                return NotFound();
            }
            return View(dailyBalance);
        }

        // POST: DailyBalances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BalanceDate")] DailyBalance dailyBalance)
        {
            if (id != dailyBalance.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dailyBalance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DailyBalanceExists(dailyBalance.Id))
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
            return View(dailyBalance);
        }

        // GET: DailyBalances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dailyBalance = await _context.DailyBalances
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dailyBalance == null)
            {
                return NotFound();
            }

            return View(dailyBalance);
        }

        // POST: DailyBalances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dailyBalance = await _context.DailyBalances.FindAsync(id);
            _context.DailyBalances.Remove(dailyBalance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DailyBalanceExists(int id)
        {
            return _context.DailyBalances.Any(e => e.Id == id);
        }
    }
}
