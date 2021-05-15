using FlowService.Models.ChainModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OptionTracker.Models;
using Org.OpenAPITools.Models;

namespace OptionTracker.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<OptionResult> OptionResults { get; set; }
        public DbSet<Watchlist> Watchlist { get; set; }
        public DbSet<Trader> Traders { get; set; }
        public DbSet<Ticker> Ticker { get; set; }
        public DbSet<OptionChainRaw> OptionChainRaw { get; set; }
        public DbSet<ChainRaw> ChainRaw { get; set; }
        public DbSet<OptionResultViewModel> CompareRaw { get; set; }
        public DbSet<OptionTracker.Models.DateChain> DateChain { get; set; }
        public DbSet<ChainResultViewModel> ComparedChains { get; set; }
        public DbSet<OptionContract> OptionContracts { get; set; }
        public DbSet<HistoricalOptionContract> HistoricalOptionContracts { get; set; }
        public DbSet<HistoricalDate> HistoricalDates { get; set; }
        public DbSet<HistoricalChain> HistoricalChains { get; set; }

        public DbSet<Instrument> Instruments { get; set; }

        public DbSet<BookSummary> BookSummaries { get; set; }
    }
}
