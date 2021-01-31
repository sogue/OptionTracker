using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using OptionTracker.Models;
using OptionTracker.Models;

namespace OptionTracker.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ChainResult> ChainResults { get; set; }
        public DbSet<OptionResult> OptionResults { get; set; }
        public DbSet<OptionContract> OptionContracts { get; set; }
        public DbSet<Watchlist> Watchlist { get; set; }
        public DbSet<Ticker> Ticker { get; set; }
        public DbSet<OptionChain> OptionChain { get; set; }
        public DbSet<OptionChainRaw> OptionChainRaw { get; set; }

    }
}
