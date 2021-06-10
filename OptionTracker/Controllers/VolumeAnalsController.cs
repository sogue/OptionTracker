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
using Org.OpenAPITools.Models;

namespace OptionTracker.Controllers
{
    public class VolumeAnalsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VolumeAnalsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: VolumeAnals
        public async Task<IActionResult> Index()
        {
            return View(await _context.VolumeAnals.ToListAsync());
        }

        // GET: VolumeAnals/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var volumeAnal = await _context.VolumeAnals.Include(x=>x.VolumeDatas)
                .FirstOrDefaultAsync(m => m.Symbol == id);
            if (volumeAnal == null)
            {
                volumeAnal = new VolumeAnal();
                volumeAnal.Symbol = id;
                volumeAnal.VolumeDatas = new List<VolumeData>();

                _context.VolumeAnals.Add(volumeAnal);
                await _context.SaveChangesAsync();

                return View(volumeAnal);
            }

            return View(volumeAnal);
        }

        [Microsoft.AspNetCore.Mvc.HttpPost("VolumeAnals/GetChartDataCall/{ticker}")]
        public JsonResult GetChartDataCall(string ticker)
        {
            try
            {
                var volumeAnal = _context.VolumeAnals.Include(x => x.VolumeDatas)
                    .First(m => m.Symbol == ticker);

           
                var chainRaw = volumeAnal.VolumeDatas
                    .OrderByDescending(x => x.Time)
                    .Where(x=>x.OptionType == Instrument.OptionTypeEnum.CallEnum).Take(30).ToList();

                int[] SeriesVal = chainRaw.Select(x => x.Volume).ToArray();
                string[] LabelsVal = chainRaw.Select(x => x.Time.Date.ToShortDateString()).ToArray();
                return Json(new { success = true, series = SeriesVal, labels = LabelsVal, message = "success.!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Some thing went Wrong.! unsuccessfull!" });
            }
        }

        [Microsoft.AspNetCore.Mvc.HttpPost("VolumeAnals/GetChartDataPut/{ticker}")]
        public JsonResult GetChartDataPut(string ticker)
        {
            try
            {
                var volumeAnal = _context.VolumeAnals.Include(x => x.VolumeDatas)
                    .First(m => m.Symbol == ticker);


                var chainRaw = volumeAnal.VolumeDatas
                    .OrderByDescending(x => x.Time)
                    .Where(x => x.OptionType == Instrument.OptionTypeEnum.PutEnum).Take(30).ToList();

                int[] SeriesVal = chainRaw.Select(x => x.Volume).ToArray();
                string[] LabelsVal = chainRaw.Select(x => x.Time.Date.ToShortDateString()).ToArray();
                return Json(new { success = true, series = SeriesVal, labels = LabelsVal, message = "success.!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Some thing went Wrong.! unsuccessfull!" });
            }
        }
        // GET: VolumeAnals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VolumeAnals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Symbol")] VolumeAnal volumeAnal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(volumeAnal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(volumeAnal);
        }

        // GET: VolumeAnals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var volumeAnal = await _context.VolumeAnals.FindAsync(id);
            if (volumeAnal == null)
            {
                return NotFound();
            }
            return View(volumeAnal);
        }

        // POST: VolumeAnals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Symbol")] VolumeAnal volumeAnal)
        {
            if (id != volumeAnal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(volumeAnal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VolumeAnalExists(volumeAnal.Id))
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
            return View(volumeAnal);
        }

        // GET: VolumeAnals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var volumeAnal = await _context.VolumeAnals
                .FirstOrDefaultAsync(m => m.Id == id);
            if (volumeAnal == null)
            {
                return NotFound();
            }

            return View(volumeAnal);
        }

        // POST: VolumeAnals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var volumeAnal = await _context.VolumeAnals.FindAsync(id);
            _context.VolumeAnals.Remove(volumeAnal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VolumeAnalExists(int id)
        {
            return _context.VolumeAnals.Any(e => e.Id == id);
        }
    }
}
