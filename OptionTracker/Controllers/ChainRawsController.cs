using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptionTracker.Data;
using OptionTracker.Models;
using System.Linq;
using System.Threading.Tasks;

namespace OptionTracker.Controllers
{
    public class ChainRawsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChainRawsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ChainRaws
        public async Task<IActionResult> Index()
        {
            return View(await _context.ChainRaw.ToListAsync());
        }

        // GET: ChainRaws/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chainRaw = await _context.ChainRaw
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chainRaw == null)
            {
                return NotFound();
            }

            return View(chainRaw.Chain.OptionContracts.ToList());
        }

        // GET: ChainRaws/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ChainRaws/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Chain")] ChainRaw chainRaw)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chainRaw);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(chainRaw);
        }

        // GET: ChainRaws/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chainRaw = await _context.ChainRaw.FindAsync(id);
            if (chainRaw == null)
            {
                return NotFound();
            }
            return View(chainRaw);
        }

        // POST: ChainRaws/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Chain")] ChainRaw chainRaw)
        {
            if (id != chainRaw.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chainRaw);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChainRawExists(chainRaw.Id))
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
            return View(chainRaw);
        }

        // GET: ChainRaws/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chainRaw = await _context.ChainRaw
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chainRaw == null)
            {
                return NotFound();
            }

            return View(chainRaw);
        }

        // POST: ChainRaws/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chainRaw = await _context.ChainRaw.FindAsync(id);
            _context.ChainRaw.Remove(chainRaw);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChainRawExists(int id)
        {
            return _context.ChainRaw.Any(e => e.Id == id);
        }
    }
}
