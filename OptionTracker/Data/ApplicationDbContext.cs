using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
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

        public DbSet<OptionTracker.Models.Watchlist> Watchlist { get; set; }

        public DbSet<OptionTracker.Models.Ticker> Ticker { get; set; }
    }
}
