using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OptionTracker.Models;

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
        public DbSet<Ticker> Ticker { get; set; }
        public DbSet<OptionChainRaw> OptionChainRaw { get; set; }
        public DbSet<ChainRaw> ChainRaw { get; set; }
        public DbSet<OptionResultViewModel> CompareRaw { get; set; }
        public DbSet<OptionTracker.Models.DateChain> DateChain { get; set; }

    }
}
