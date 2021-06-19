﻿using System;
using System.Reflection;
using Core.Entities;
using FlowService.Models.Anal;
using FlowService.Models.ChainModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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

        public DbSet<BookDetail> BookDetails { get; set; }
        public DbSet<Greeks> Greeks { get; set; }
        public DbSet<Stats> Stats { get; set; }
        
        // public DbSet<Watchlist> Watchlist { get; set; }

       // public DbSet<Trader> Traders { get; set; }
        public DbSet<Ticker> Ticker { get; set; }

        public DbSet<Core.Entities.Legacy.Ticker> Tickers { get; set; }
        public DbSet<OptionChainRaw> OptionChainRaw { get; set; }
        public DbSet<OptionContract> OptionContracts { get; set; }

        public DbSet<Instrument> Instruments { get; set; }

        public DbSet<InstrumentHistory> InstrumentHistories { get; set; }
        public DbSet<BookSummary> BookSummaries { get; set; }

        public DbSet<PortfolioEth> PortfoliosEth { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<DailyBalance> DailyBalances { get; set; }



        public DbSet<VolumeAnal> VolumeAnals { get; set; }
        public DbSet<VolumeData> VolumeDatas { get; set; }

        public DbSet<OptionActivity> OptionActivities { get; set; }
    }


}

