using System;
using System.Reflection;
using Core.Entities;
using FlowService.Models.Anal;
using FlowService.Models.ChainModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OptionTracker.Models;
using OptionTracker.Models.Anal;
using OptionTracker.Models.Crypto;
using Org.OpenAPITools.Models;

namespace OptionTracker.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<TickerSymbol>().Property(p => p.Id).IsRequired();
            builder.Entity<TickerSymbol>().Property(p => p.Name).IsRequired().HasMaxLength(5);
            builder.Entity<TickerSymbol>().Property(p => p.Description).IsRequired();
            builder.Entity<TickerSymbol>().Property(p => p.PictureUrl).IsRequired();
            builder.Entity<TickerSymbol>().HasOne(p => p.TickerSector).WithMany()
                .HasForeignKey(p => p.TickerSectorId);
            builder.Entity<TickerSymbol>().HasOne(p => p.TickerType).WithMany()
                .HasForeignKey(p => p.TickerTypeId);
        }


        // Trade Watchlist
        public DbSet<TickerType> TickerTypes { get; set; }
        public DbSet<TickerSector> TickerSectors { get; set; }
        public DbSet<Trader> Traders { get; set; }
        public DbSet<TickerSymbol> TickerSymbols { get; set; }

        // Stock Op
        public DbSet<OptionChainRaw> OptionChainRaw { get; set; }
        public DbSet<OptionContract> OptionContracts { get; set; }
  


        // Crypto
        public DbSet<PortfolioEth> PortfoliosEth { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<DailyBalance> DailyBalances { get; set; }
        public DbSet<Instrument> Instruments { get; set; }
        public DbSet<InstrumentHistory> InstrumentHistories { get; set; }
        public DbSet<BookSummary> BookSummaries { get; set; }
        public DbSet<BookDetail> BookDetails { get; set; }
        public DbSet<Greeks> Greeks { get; set; }
        public DbSet<Stats> Stats { get; set; }

        // Option Volume Datas

        public DbSet<VolumeAnal> VolumeAnals { get; set; }
        public DbSet<VolumeData> VolumeDatas { get; set; }

        public DbSet<OptionActivity> OptionActivities { get; set; }
    }


}

