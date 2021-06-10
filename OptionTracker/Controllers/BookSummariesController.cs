using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OptionTracker.Data;
using OptionTracker.Models.Crypto;
using OptionTracker.Models.Deribit;
using Org.OpenAPITools.Models;
using Websocket.Client;

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
        public async Task<IActionResult> Index
            ([FromQuery]string currency, [FromQuery]string underlyingIndex, [FromQuery]int? multiplier, [FromQuery]string strike, [FromQuery]int? searchStartTimestamp)

        {
            var multi = multiplier ?? 1;
            var ua = underlyingIndex ?? "dsfsdfdfdsf";
            var listA = new List<InstrumentHistory>();
            var btcUrl =
           "https://www.deribit.com/api/v2/public/get_instruments?currency=BTC&kind=option&expired=false";

            HttpClient clientBtc = new HttpClient();
            var responseBtc = await clientBtc.GetFromJsonAsync<JsonDocument>(btcUrl);

            var instrumentsBtc = JsonConvert.DeserializeObject<Instrument[]>(
                        responseBtc.RootElement.GetProperty("result").ToString() ?? "");

            var ethUrl =
                "https://www.deribit.com/api/v2/public/get_instruments?currency=ETH&kind=option&expired=false";

            HttpClient clientEth = new HttpClient();

            var responseEth = await clientEth.GetFromJsonAsync<JsonDocument>(ethUrl);
            var instrumentsEth = JsonConvert.DeserializeObject<Instrument[]>(
                        responseEth.RootElement.GetProperty("result").ToString() ?? "");

   
            var newInstruments = instrumentsBtc.Concat(instrumentsEth).ToList();


            var newHistories = newInstruments.Select(x => new InstrumentHistory
            {
                ActualInstrument = x,
                InstrumentName = x.InstrumentName,
                BookSummaries = new List<BookSummary>()
            }).Where(x=>x.ActualInstrument.InstrumentName.Contains(ua));

            if(newHistories != null && newHistories.Any())
            {
                listA.AddRange(newHistories);
            }
            

            var newBookSummaries = new List<BookSummary>();

            var wsUrl = new Uri("wss://www.deribit.com/ws/api/v2/");
            var exitEvent = new ManualResetEvent(false);
            var messages = new List<string>();
            if(newHistories.Select(x => x.ActualInstrument).Any())
            { 
            for (int i = 0; i < newHistories.Select(x => x.ActualInstrument).Count(); i = i + 40)
            {
                var instrumentsBatch = newHistories.Select(x=>x.ActualInstrument).Skip(i).Take(40);

                using (var client = new WebsocketClient(wsUrl))
                {
                    var messageString = "";

                    client
                       .MessageReceived
                       .Subscribe(msg =>
                       {
                           messageString = msg.Text;
                           messages.Add(messageString);

                       });


                    await client.Start();

                    foreach (var instrument in instrumentsBatch)
                    {
                        var requestText = new
                        {
                            jsonrpc = "2.0",
                            id = instrument.Id,
                            method = "public/get_book_summary_by_instrument",
                            @params = new
                            {
                                instrument_name = instrument.InstrumentName
                            }
                        };

                        var jsonText = Newtonsoft.Json.Linq.JObject.FromObject(requestText).ToString();

                        client.Send(jsonText);

                    }
                    exitEvent.WaitOne(TimeSpan.FromSeconds(3));
                }
            }
            }
            foreach (var mmm in messages)

            {
                var jsonResult = System.Text.Json.JsonSerializer.Deserialize<JsonDocument>(mmm);
                var jString = jsonResult.RootElement.TryGetProperty("result", out var value) ? value.ToString() : "";
                var bookSummary = JsonConvert.DeserializeObject<BookSummary[]>(jString).First();

   
                    bookSummary.RequestTime = DateTime.Today;
                    listA.Where(d => d.InstrumentName == bookSummary.InstrumentName).FirstOrDefault().BookSummaries.Add(bookSummary);
               
            }
      

            var strike2 =  strike == null ? "0": strike;
            double strikeInt = double.Parse(strike2);
            //var result = await _context.BookSummaries
            //    .Where(x=>x.UnderlyingIndex == underlyingIndex)
            //    .OrderByDescending(x => x.InstrumentName).Take(100).ToListAsync();

            var result = listA
                .Where(x => x.BookSummaries.Any(x => x.UnderlyingIndex == underlyingIndex)
                && x.ActualInstrument.OptionType.Value == Instrument.OptionTypeEnum.PutEnum).ToList();

            var res2 = result.Where(x=> (double)x.ActualInstrument.Strike.Value >= strikeInt)
                .OrderBy(x => x.ActualInstrument.Strike).Take(10).ToList();


            var port =await _context.DailyBalances
                .Where(x=>x.BalanceDate.Date == DateTime.Today)
                .Include(x=>x.PortfolioEth).FirstOrDefaultAsync();


            var res = res2.Select(r=> new CspSummary {
               VolumeUsd = r.BookSummaries.FirstOrDefault().MarkPrice * r.BookSummaries.FirstOrDefault().EstimatedDeliveryPrice,
            Percentage = r.ActualInstrument.Strike / r.BookSummaries.FirstOrDefault().EstimatedDeliveryPrice,
            Multiplier = multi,
            CapitalMultiUsd = (decimal)0.1 * r.BookSummaries.FirstOrDefault().EstimatedDeliveryPrice * multi,
            PremiumMultiUsd = r.BookSummaries.FirstOrDefault().MarkPrice * r.BookSummaries.FirstOrDefault().EstimatedDeliveryPrice * multi,
            PremiumMultiUsdMonth = r.BookSummaries.FirstOrDefault().MarkPrice * r.BookSummaries.FirstOrDefault().EstimatedDeliveryPrice * multi * 4,
                RiskUsd = r.ActualInstrument.Strike * multi,
            LiqStrike = r.ActualInstrument.Strike - ((decimal)0.1 * r.BookSummaries.FirstOrDefault().EstimatedDeliveryPrice / multi ), 
            RequestTime = r.BookSummaries.FirstOrDefault().RequestTime,
            InstrumentName =r.InstrumentName,
                AskPrice = r.BookSummaries.FirstOrDefault().AskPrice,
                BidPrice = r.BookSummaries.FirstOrDefault().BidPrice,
                Low = r.BookSummaries.FirstOrDefault().Low,
                High = r.BookSummaries.FirstOrDefault().High,
                MarkPrice = r.BookSummaries.FirstOrDefault().MarkPrice,
            EstimatedDeliveryPrice = r.BookSummaries.FirstOrDefault().EstimatedDeliveryPrice,
            ActualLeverage = r.ActualInstrument.Strike * multi / port.PortfolioEth.EquityUsd,
            ActualCapital = port.PortfolioEth.EquityUsd,
            ActualLiqStrike = r.ActualInstrument.Strike - (port.PortfolioEth.EquityUsd / multi),
            });

            return View(res);
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
