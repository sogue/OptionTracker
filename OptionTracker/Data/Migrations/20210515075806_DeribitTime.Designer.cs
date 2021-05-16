﻿// <auto-generated />
using System;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using OptionTracker.Data;
using OptionTracker.Models;

namespace OptionTracker.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210515075806_DeribitTime")]
    partial class DeribitTime
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("FlowService.Models.ChainModels.HistoricalChain", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ChainSymbol")
                        .HasColumnType("text");

                    b.Property<int>("TickerId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TickerId")
                        .IsUnique();

                    b.ToTable("HistoricalChains");
                });

            modelBuilder.Entity("FlowService.Models.ChainModels.HistoricalDate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("DateSymbol")
                        .HasColumnType("text");

                    b.Property<int>("HistoricalChainId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("HistoricalChainId");

                    b.ToTable("HistoricalDates");
                });

            modelBuilder.Entity("FlowService.Models.ChainModels.HistoricalOptionContract", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ContractSymbol")
                        .HasColumnType("text");

                    b.Property<ICollection<OptionContract>>("Contracts")
                        .HasColumnType("jsonb");

                    b.Property<int>("HistoricalDateId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("HistoricalDateId");

                    b.ToTable("HistoricalOptionContracts");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("OptionTracker.Models.ChainRaw", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<Chain>("Chain")
                        .HasColumnType("jsonb");

                    b.HasKey("Id");

                    b.ToTable("ChainRaw");
                });

            modelBuilder.Entity("OptionTracker.Models.ChainResultViewModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<float>("ClosePrice")
                        .HasColumnType("real");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("MarketCap")
                        .HasColumnType("integer");

                    b.Property<string>("Ticker")
                        .HasColumnType("text");

                    b.Property<TimeSpan>("TimeChange")
                        .HasColumnType("interval");

                    b.HasKey("Id");

                    b.ToTable("ComparedChains");
                });

            modelBuilder.Entity("OptionTracker.Models.DateChain", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ExpDate")
                        .HasColumnType("text");

                    b.Property<ICollection<OptionContract>>("OptionContracts")
                        .HasColumnType("jsonb");

                    b.Property<string>("Symbol")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("DateChain");
                });

            modelBuilder.Entity("OptionTracker.Models.OptionChainRaw", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<JsonDocument>("Data")
                        .HasColumnType("jsonb");

                    b.HasKey("Id");

                    b.ToTable("OptionChainRaw");
                });

            modelBuilder.Entity("OptionTracker.Models.OptionContract", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<decimal>("Ask")
                        .HasColumnType("numeric");

                    b.Property<int>("AskSize")
                        .HasColumnType("integer");

                    b.Property<decimal>("Bid")
                        .HasColumnType("numeric");

                    b.Property<string>("BidAskSize")
                        .HasColumnType("text");

                    b.Property<int>("BidSize")
                        .HasColumnType("integer");

                    b.Property<decimal>("ClosePrice")
                        .HasColumnType("numeric");

                    b.Property<int>("DaysToExpiration")
                        .HasColumnType("integer");

                    b.Property<string>("DeliverableNote")
                        .HasColumnType("text");

                    b.Property<string>("Delta")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("ExchangeName")
                        .HasColumnType("text");

                    b.Property<long>("ExpirationDate")
                        .HasColumnType("bigint");

                    b.Property<string>("ExpirationType")
                        .HasColumnType("text");

                    b.Property<string>("Gamma")
                        .HasColumnType("text");

                    b.Property<decimal>("HighPrice")
                        .HasColumnType("numeric");

                    b.Property<int>("HistoricalOptionContractId")
                        .HasColumnType("integer");

                    b.Property<bool>("InTheMoney")
                        .HasColumnType("boolean");

                    b.Property<string>("IsIndexOption")
                        .HasColumnType("text");

                    b.Property<decimal>("Last")
                        .HasColumnType("numeric");

                    b.Property<string>("LastSize")
                        .HasColumnType("text");

                    b.Property<long>("LastTradingDay")
                        .HasColumnType("bigint");

                    b.Property<decimal>("LowPrice")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Mark")
                        .HasColumnType("numeric");

                    b.Property<decimal>("MarkChange")
                        .HasColumnType("numeric");

                    b.Property<decimal>("MarkPercentChange")
                        .HasColumnType("numeric");

                    b.Property<bool>("Mini")
                        .HasColumnType("boolean");

                    b.Property<decimal>("Multiplier")
                        .HasColumnType("numeric");

                    b.Property<decimal>("NetChange")
                        .HasColumnType("numeric");

                    b.Property<bool>("NonStandard")
                        .HasColumnType("boolean");

                    b.Property<int>("OpenInterest")
                        .HasColumnType("integer");

                    b.Property<decimal>("OpenPrice")
                        .HasColumnType("numeric");

                    b.Property<string>("OptionDeliverablesList")
                        .HasColumnType("text");

                    b.Property<decimal>("PercentChange")
                        .HasColumnType("numeric");

                    b.Property<string>("PutCall")
                        .HasColumnType("text");

                    b.Property<long?>("QuoteTimeInLong")
                        .HasColumnType("bigint");

                    b.Property<string>("Rho")
                        .HasColumnType("text");

                    b.Property<string>("SettlementType")
                        .HasColumnType("text");

                    b.Property<decimal>("StrikePrice")
                        .HasColumnType("numeric");

                    b.Property<string>("Symbol")
                        .HasColumnType("text");

                    b.Property<string>("TheoreticalOptionValue")
                        .HasColumnType("text");

                    b.Property<string>("TheoreticalVolatility")
                        .HasColumnType("text");

                    b.Property<string>("Theta")
                        .HasColumnType("text");

                    b.Property<decimal>("TimeValue")
                        .HasColumnType("numeric");

                    b.Property<int>("TotalVolume")
                        .HasColumnType("integer");

                    b.Property<string>("TradeDate")
                        .HasColumnType("text");

                    b.Property<long?>("TradeTimeInLong")
                        .HasColumnType("bigint");

                    b.Property<string>("Vega")
                        .HasColumnType("text");

                    b.Property<string>("Volatility")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("OptionContracts");
                });

            modelBuilder.Entity("OptionTracker.Models.OptionResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<decimal>("ClosePrice")
                        .HasColumnType("numeric");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("OpenInterest")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("OptionResults");
                });

            modelBuilder.Entity("OptionTracker.Models.OptionResultViewModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("ChainResultViewModelId")
                        .HasColumnType("integer");

                    b.Property<string>("ChartCode")
                        .HasColumnType("text");

                    b.Property<decimal>("ClosePrice")
                        .HasColumnType("numeric");

                    b.Property<decimal>("ClosePriceChange")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("CompareDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("OpenInterest")
                        .HasColumnType("integer");

                    b.Property<int>("OpenInterestChange")
                        .HasColumnType("integer");

                    b.Property<string>("Symbol")
                        .HasColumnType("text");

                    b.Property<decimal>("Volatility")
                        .HasColumnType("numeric");

                    b.Property<decimal>("VolatilityChange")
                        .HasColumnType("numeric");

                    b.Property<int>("Volume")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ChainResultViewModelId");

                    b.ToTable("CompareRaw");
                });

            modelBuilder.Entity("OptionTracker.Models.Ticker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<float>("ClosePrice")
                        .HasColumnType("real");

                    b.Property<int>("MarketCap")
                        .HasColumnType("integer");

                    b.Property<DateTime>("NextEarnings")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Symbol")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Ticker");
                });

            modelBuilder.Entity("OptionTracker.Models.Trader", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("IdentityUserId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Traders");
                });

            modelBuilder.Entity("OptionTracker.Models.Watchlist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.HasKey("Id");

                    b.ToTable("Watchlist");
                });

            modelBuilder.Entity("Org.OpenAPITools.Models.BookSummary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<decimal?>("AskPrice")
                        .IsRequired()
                        .HasColumnType("numeric");

                    b.Property<string>("BaseCurrency")
                        .HasColumnType("text");

                    b.Property<decimal?>("BidPrice")
                        .IsRequired()
                        .HasColumnType("numeric");

                    b.Property<long?>("CreationTimestamp")
                        .IsRequired()
                        .HasColumnType("bigint");

                    b.Property<decimal?>("CurrentFunding")
                        .HasColumnType("numeric");

                    b.Property<decimal?>("EstimatedDeliveryPrice")
                        .HasColumnType("numeric");

                    b.Property<decimal?>("Funding8h")
                        .HasColumnType("numeric");

                    b.Property<decimal?>("High")
                        .IsRequired()
                        .HasColumnType("numeric");

                    b.Property<int>("InstrumentId")
                        .HasColumnType("integer");

                    b.Property<string>("InstrumentName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal?>("InterestRate")
                        .HasColumnType("numeric");

                    b.Property<decimal?>("Last")
                        .IsRequired()
                        .HasColumnType("numeric");

                    b.Property<decimal?>("Low")
                        .IsRequired()
                        .HasColumnType("numeric");

                    b.Property<decimal?>("MarkPrice")
                        .IsRequired()
                        .HasColumnType("numeric");

                    b.Property<decimal?>("MidPrice")
                        .IsRequired()
                        .HasColumnType("numeric");

                    b.Property<decimal?>("OpenInterest")
                        .IsRequired()
                        .HasColumnType("numeric");

                    b.Property<string>("QuoteCurrency")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("RequestTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("UnderlyingIndex")
                        .HasColumnType("text");

                    b.Property<decimal?>("UnderlyingPrice")
                        .HasColumnType("numeric");

                    b.Property<decimal?>("Volume")
                        .IsRequired()
                        .HasColumnType("numeric");

                    b.Property<decimal?>("VolumeUsd")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("InstrumentId");

                    b.ToTable("BookSummaries");
                });

            modelBuilder.Entity("Org.OpenAPITools.Models.Instrument", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("BaseCurrency")
                        .HasColumnType("integer");

                    b.Property<decimal?>("ContractSize")
                        .IsRequired()
                        .HasColumnType("numeric");

                    b.Property<long?>("CreationTimestamp")
                        .IsRequired()
                        .HasColumnType("bigint");

                    b.Property<long?>("ExpirationTimestamp")
                        .IsRequired()
                        .HasColumnType("bigint");

                    b.Property<string>("InstrumentName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool?>("IsActive")
                        .IsRequired()
                        .HasColumnType("boolean");

                    b.Property<int>("Kind")
                        .HasColumnType("integer");

                    b.Property<decimal?>("MinTradeAmount")
                        .IsRequired()
                        .HasColumnType("numeric");

                    b.Property<int?>("OptionType")
                        .HasColumnType("integer");

                    b.Property<int>("QuoteCurrency")
                        .HasColumnType("integer");

                    b.Property<int>("SettlementPeriod")
                        .HasColumnType("integer");

                    b.Property<decimal?>("Strike")
                        .HasColumnType("numeric");

                    b.Property<decimal?>("TickSize")
                        .IsRequired()
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("Instruments");
                });

            modelBuilder.Entity("TickerTrader", b =>
                {
                    b.Property<int>("TickersId")
                        .HasColumnType("integer");

                    b.Property<int>("TradersId")
                        .HasColumnType("integer");

                    b.HasKey("TickersId", "TradersId");

                    b.HasIndex("TradersId");

                    b.ToTable("TickerTrader");
                });

            modelBuilder.Entity("FlowService.Models.ChainModels.HistoricalChain", b =>
                {
                    b.HasOne("OptionTracker.Models.Ticker", null)
                        .WithOne("Chain")
                        .HasForeignKey("FlowService.Models.ChainModels.HistoricalChain", "TickerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FlowService.Models.ChainModels.HistoricalDate", b =>
                {
                    b.HasOne("FlowService.Models.ChainModels.HistoricalChain", null)
                        .WithMany("Dates")
                        .HasForeignKey("HistoricalChainId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FlowService.Models.ChainModels.HistoricalOptionContract", b =>
                {
                    b.HasOne("FlowService.Models.ChainModels.HistoricalDate", null)
                        .WithMany("OptionContracts")
                        .HasForeignKey("HistoricalDateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OptionTracker.Models.OptionResultViewModel", b =>
                {
                    b.HasOne("OptionTracker.Models.ChainResultViewModel", null)
                        .WithMany("OptionsResults")
                        .HasForeignKey("ChainResultViewModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Org.OpenAPITools.Models.BookSummary", b =>
                {
                    b.HasOne("Org.OpenAPITools.Models.Instrument", null)
                        .WithMany("Summaries")
                        .HasForeignKey("InstrumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TickerTrader", b =>
                {
                    b.HasOne("OptionTracker.Models.Ticker", null)
                        .WithMany()
                        .HasForeignKey("TickersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OptionTracker.Models.Trader", null)
                        .WithMany()
                        .HasForeignKey("TradersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FlowService.Models.ChainModels.HistoricalChain", b =>
                {
                    b.Navigation("Dates");
                });

            modelBuilder.Entity("FlowService.Models.ChainModels.HistoricalDate", b =>
                {
                    b.Navigation("OptionContracts");
                });

            modelBuilder.Entity("OptionTracker.Models.ChainResultViewModel", b =>
                {
                    b.Navigation("OptionsResults");
                });

            modelBuilder.Entity("OptionTracker.Models.Ticker", b =>
                {
                    b.Navigation("Chain");
                });

            modelBuilder.Entity("Org.OpenAPITools.Models.Instrument", b =>
                {
                    b.Navigation("Summaries");
                });
#pragma warning restore 612, 618
        }
    }
}