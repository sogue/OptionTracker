using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OptionTracker.Data;
using OptionTracker.Models;
using OptionTracker.Models.Anal;

namespace OptionTracker.Controllers
{
    public class VolumeDatasController : Controller
    {
        private readonly ApplicationDbContext _context;

        [Microsoft.AspNetCore.Mvc.HttpPost("VolumeDatas/GetChartData/")]
        public JsonResult GetChartData(string ticker)
        {
            try
            {
                var chainRaw = _context.VolumeDatas
                    .OrderByDescending(x=>x.Time).ToList();

                int[] SeriesVal = chainRaw.Select(x => x.Volume).ToArray();
                string[] LabelsVal = chainRaw.Select(x => x.Time.Date.ToShortDateString()).ToArray();
            return Json(new { success = true, series = SeriesVal, labels = LabelsVal, message = "success.!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Some thing went Wrong.! unsuccessfull!" });
            }
        }
        public VolumeDatasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: VolumeDatas
        public async Task<IActionResult> Index()
        {
            return View(await _context.VolumeDatas.ToListAsync());
        }

        // GET: VolumeDatas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var volumeData = await _context.VolumeDatas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (volumeData == null)
            {
                return NotFound();
            }

            return View(volumeData);
        }

        // GET: VolumeDatas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VolumeDatas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Time,Volume")] VolumeData volumeData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(volumeData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(volumeData);
        }

        // GET: VolumeDatas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var volumeData = await _context.VolumeDatas.FindAsync(id);
            if (volumeData == null)
            {
                return NotFound();
            }
            return View(volumeData);
        }

        // POST: VolumeDatas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Time,Volume")] VolumeData volumeData)
        {
            if (id != volumeData.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(volumeData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VolumeDataExists(volumeData.Id))
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
            return View(volumeData);
        }

        // GET: VolumeDatas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var volumeData = await _context.VolumeDatas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (volumeData == null)
            {
                return NotFound();
            }

            return View(volumeData);
        }

        // POST: VolumeDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var volumeData = await _context.VolumeDatas.FindAsync(id);
            _context.VolumeDatas.Remove(volumeData);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VolumeDataExists(int id)
        {
            return _context.VolumeDatas.Any(e => e.Id == id);
        }
    }
}
