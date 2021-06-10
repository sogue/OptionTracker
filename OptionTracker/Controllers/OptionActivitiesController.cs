using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FlowService.Models.Anal;
using OptionTracker.Data;

namespace OptionTracker.Controllers
{
    public class OptionActivitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OptionActivitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OptionActivities
        public async Task<IActionResult> Index([FromQuery
    ]string date)
        {
            DateTime dateTime = date != null ? DateTime.ParseExact(date, "ddMMyy", null) : DateTime.Today.AddDays(-1);
            var a = await _context.OptionActivities
                .Where(x => x.ActivityDate == dateTime)
                .OrderByDescending(x => x.TotalVolume).Take(50).ToListAsync();
            return View(a.OrderByDescending(x=>x.TotalVolume));
        }

        // GET: OptionActivities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var optionActivity = await _context.OptionActivities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (optionActivity == null)
            {
                return NotFound();
            }

            return View(optionActivity);
        }

        // GET: OptionActivities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OptionActivities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ticker,ActivityDate,CallVolume,PutVolume,TotalVolume,CallPutRatio")] OptionActivity optionActivity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(optionActivity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(optionActivity);
        }

        // GET: OptionActivities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var optionActivity = await _context.OptionActivities.FindAsync(id);
            if (optionActivity == null)
            {
                return NotFound();
            }
            return View(optionActivity);
        }

        // POST: OptionActivities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ticker,ActivityDate,CallVolume,PutVolume,TotalVolume,CallPutRatio")] OptionActivity optionActivity)
        {
            if (id != optionActivity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(optionActivity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OptionActivityExists(optionActivity.Id))
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
            return View(optionActivity);
        }

        // GET: OptionActivities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var optionActivity = await _context.OptionActivities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (optionActivity == null)
            {
                return NotFound();
            }

            return View(optionActivity);
        }

        // POST: OptionActivities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var optionActivity = await _context.OptionActivities.FindAsync(id);
            _context.OptionActivities.Remove(optionActivity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OptionActivityExists(int id)
        {
            return _context.OptionActivities.Any(e => e.Id == id);
        }
    }
}
