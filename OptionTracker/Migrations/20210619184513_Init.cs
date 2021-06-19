using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace OptionTracker.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Instruments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QuoteCurrency = table.Column<int>(type: "integer", nullable: true),
                    Kind = table.Column<int>(type: "integer", nullable: true),
                    TickSize = table.Column<decimal>(type: "numeric", nullable: true),
                    ContractSize = table.Column<decimal>(type: "numeric", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    OptionType = table.Column<int>(type: "integer", nullable: true),
                    MinTradeAmount = table.Column<decimal>(type: "numeric", nullable: true),
                    InstrumentName = table.Column<string>(type: "text", nullable: true),
                    SettlementPeriod = table.Column<int>(type: "integer", nullable: true),
                    Strike = table.Column<decimal>(type: "numeric", nullable: true),
                    BaseCurrency = table.Column<int>(type: "integer", nullable: true),
                    CreationTimestamp = table.Column<long>(type: "bigint", nullable: true),
                    ExpirationTimestamp = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instruments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OptionActivities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TickerId = table.Column<int>(type: "integer", nullable: false),
                    Ticker = table.Column<string>(type: "text", nullable: true),
                    ActivityDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CallVolume = table.Column<decimal>(type: "numeric", nullable: false),
                    PutVolume = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalVolume = table.Column<decimal>(type: "numeric", nullable: false),
                    CallPutRatio = table.Column<decimal>(type: "numeric", nullable: false),
                    OptionVolumeChange = table.Column<decimal>(type: "numeric", nullable: false),
                    AssetType = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptionActivities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OptionChainRaw",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Symbol = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Data = table.Column<JsonDocument>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptionChainRaw", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OptionContracts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HistoricalOptionContractId = table.Column<int>(type: "integer", nullable: false),
                    PutCall = table.Column<string>(type: "text", nullable: true),
                    Symbol = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ExchangeName = table.Column<string>(type: "text", nullable: true),
                    Bid = table.Column<decimal>(type: "numeric", nullable: false),
                    Ask = table.Column<decimal>(type: "numeric", nullable: false),
                    Last = table.Column<decimal>(type: "numeric", nullable: false),
                    Mark = table.Column<decimal>(type: "numeric", nullable: false),
                    BidSize = table.Column<int>(type: "integer", nullable: false),
                    AskSize = table.Column<int>(type: "integer", nullable: false),
                    BidAskSize = table.Column<string>(type: "text", nullable: true),
                    LastSize = table.Column<string>(type: "text", nullable: true),
                    HighPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    LowPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    OpenPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    ClosePrice = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalVolume = table.Column<int>(type: "integer", nullable: false),
                    TradeDate = table.Column<string>(type: "text", nullable: true),
                    TradeTimeInLong = table.Column<long>(type: "bigint", nullable: true),
                    QuoteTimeInLong = table.Column<long>(type: "bigint", nullable: true),
                    NetChange = table.Column<decimal>(type: "numeric", nullable: false),
                    Volatility = table.Column<string>(type: "text", nullable: true),
                    Delta = table.Column<string>(type: "text", nullable: true),
                    Gamma = table.Column<string>(type: "text", nullable: true),
                    Theta = table.Column<string>(type: "text", nullable: true),
                    Vega = table.Column<string>(type: "text", nullable: true),
                    Rho = table.Column<string>(type: "text", nullable: true),
                    OpenInterest = table.Column<int>(type: "integer", nullable: false),
                    TimeValue = table.Column<decimal>(type: "numeric", nullable: false),
                    TheoreticalOptionValue = table.Column<string>(type: "text", nullable: true),
                    TheoreticalVolatility = table.Column<string>(type: "text", nullable: true),
                    OptionDeliverablesList = table.Column<string>(type: "text", nullable: true),
                    StrikePrice = table.Column<decimal>(type: "numeric", nullable: false),
                    ExpirationDate = table.Column<long>(type: "bigint", nullable: false),
                    DaysToExpiration = table.Column<int>(type: "integer", nullable: false),
                    ExpirationType = table.Column<string>(type: "text", nullable: true),
                    LastTradingDay = table.Column<long>(type: "bigint", nullable: false),
                    Multiplier = table.Column<decimal>(type: "numeric", nullable: false),
                    SettlementType = table.Column<string>(type: "text", nullable: true),
                    DeliverableNote = table.Column<string>(type: "text", nullable: true),
                    IsIndexOption = table.Column<string>(type: "text", nullable: true),
                    PercentChange = table.Column<decimal>(type: "numeric", nullable: false),
                    MarkChange = table.Column<decimal>(type: "numeric", nullable: false),
                    MarkPercentChange = table.Column<decimal>(type: "numeric", nullable: false),
                    NonStandard = table.Column<bool>(type: "boolean", nullable: false),
                    Mini = table.Column<bool>(type: "boolean", nullable: false),
                    InTheMoney = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptionContracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PortfoliosEth",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PortfolioDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    MaintenanceMargin = table.Column<decimal>(type: "numeric", nullable: true),
                    AvailableWithdrawalFunds = table.Column<decimal>(type: "numeric", nullable: true),
                    InitialMargin = table.Column<decimal>(type: "numeric", nullable: true),
                    AvailableFunds = table.Column<decimal>(type: "numeric", nullable: true),
                    options_session_upl = table.Column<decimal>(type: "numeric", nullable: true),
                    options_gamma = table.Column<decimal>(type: "numeric", nullable: true),
                    options_theta = table.Column<decimal>(type: "numeric", nullable: true),
                    delta_total = table.Column<decimal>(type: "numeric", nullable: true),
                    futures_session_rpl = table.Column<decimal>(type: "numeric", nullable: true),
                    total_pl = table.Column<decimal>(type: "numeric", nullable: true),
                    options_session_rpl = table.Column<decimal>(type: "numeric", nullable: true),
                    options_delta = table.Column<decimal>(type: "numeric", nullable: true),
                    futures_pl = table.Column<decimal>(type: "numeric", nullable: true),
                    session_upl = table.Column<decimal>(type: "numeric", nullable: true),
                    creation_timestamp = table.Column<long>(type: "bigint", nullable: false),
                    options_pl = table.Column<decimal>(type: "numeric", nullable: true),
                    projected_initial_margin = table.Column<decimal>(type: "numeric", nullable: true),
                    projected_maintenance_margin = table.Column<decimal>(type: "numeric", nullable: true),
                    session_rpl = table.Column<decimal>(type: "numeric", nullable: true),
                    options_vega = table.Column<decimal>(type: "numeric", nullable: true),
                    projected_delta_total = table.Column<decimal>(type: "numeric", nullable: true),
                    futures_session_upl = table.Column<decimal>(type: "numeric", nullable: true),
                    options_value = table.Column<decimal>(type: "numeric", nullable: true),
                    Currency = table.Column<int>(type: "integer", nullable: true),
                    MarginBalance = table.Column<decimal>(type: "numeric", nullable: true),
                    Equity = table.Column<decimal>(type: "numeric", nullable: true),
                    EquityUsd = table.Column<decimal>(type: "numeric", nullable: true),
                    Balance = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfoliosEth", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tickers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    AssetType = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    PictureUrl = table.Column<string>(type: "text", nullable: true),
                    MarketCap = table.Column<int>(type: "integer", nullable: false),
                    ClosePrice = table.Column<decimal>(type: "numeric", nullable: false),
                    LastOptionVolume = table.Column<int>(type: "integer", nullable: false),
                    OptionVolumeChange = table.Column<decimal>(type: "numeric", nullable: false),
                    NextEarnings = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Traders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdentityUserId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Traders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VolumeAnals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Symbol = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolumeAnals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Watchlist",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Watchlist", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InstrumentHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InstrumentName = table.Column<string>(type: "text", nullable: true),
                    ActualInstrumentId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstrumentHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstrumentHistories_Instruments_ActualInstrumentId",
                        column: x => x.ActualInstrumentId,
                        principalTable: "Instruments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DailyBalances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BalanceDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    PortfolioEthId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyBalances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyBalances_PortfoliosEth_PortfolioEthId",
                        column: x => x.PortfolioEthId,
                        principalTable: "PortfoliosEth",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ticker",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    AssetType = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    PictureUrl = table.Column<string>(type: "text", nullable: true),
                    MarketCap = table.Column<int>(type: "integer", nullable: false),
                    ClosePrice = table.Column<decimal>(type: "numeric", nullable: false),
                    LastOptionVolume = table.Column<int>(type: "integer", nullable: false),
                    OptionVolumeChange = table.Column<decimal>(type: "numeric", nullable: false),
                    NextEarnings = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TraderId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticker", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ticker_Traders_TraderId",
                        column: x => x.TraderId,
                        principalTable: "Traders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VolumeDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Symbol = table.Column<string>(type: "text", nullable: true),
                    Time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Volume = table.Column<int>(type: "integer", nullable: false),
                    OptionType = table.Column<int>(type: "integer", nullable: true),
                    VolumeAnalId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolumeDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VolumeDatas_VolumeAnals_VolumeAnalId",
                        column: x => x.VolumeAnalId,
                        principalTable: "VolumeAnals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RequestTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UnderlyingPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    UnderlyingIndex = table.Column<string>(type: "text", nullable: true),
                    Timestamp = table.Column<long>(type: "bigint", nullable: false),
                    State = table.Column<string>(type: "text", nullable: true),
                    SettlementPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    OpenInterest = table.Column<decimal>(type: "numeric", nullable: true),
                    MinPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    MaxPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    MarkPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    MarkIv = table.Column<decimal>(type: "numeric", nullable: true),
                    LastPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    InterestRate = table.Column<decimal>(type: "numeric", nullable: true),
                    InstrumentName = table.Column<string>(type: "text", nullable: true),
                    IndexPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    EstimatedDeliveryPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    BidIv = table.Column<decimal>(type: "numeric", nullable: true),
                    BestBidPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    BestBidAmount = table.Column<decimal>(type: "numeric", nullable: true),
                    BestAskPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    BestAskAmount = table.Column<decimal>(type: "numeric", nullable: true),
                    AskIv = table.Column<decimal>(type: "numeric", nullable: true),
                    InstrumentHistoryId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookDetails_InstrumentHistories_InstrumentHistoryId",
                        column: x => x.InstrumentHistoryId,
                        principalTable: "InstrumentHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookSummaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InstrumentHistoryId = table.Column<int>(type: "integer", nullable: true),
                    RequestTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UnderlyingIndex = table.Column<string>(type: "text", nullable: true),
                    Volume = table.Column<decimal>(type: "numeric", nullable: true),
                    VolumeUsd = table.Column<decimal>(type: "numeric", nullable: true),
                    UnderlyingPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    BidPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    OpenInterest = table.Column<decimal>(type: "numeric", nullable: true),
                    QuoteCurrency = table.Column<string>(type: "text", nullable: true),
                    High = table.Column<decimal>(type: "numeric", nullable: true),
                    EstimatedDeliveryPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    Last = table.Column<decimal>(type: "numeric", nullable: true),
                    MidPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    InterestRate = table.Column<decimal>(type: "numeric", nullable: true),
                    Funding8h = table.Column<decimal>(type: "numeric", nullable: true),
                    MarkPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    AskPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    InstrumentName = table.Column<string>(type: "text", nullable: true),
                    Low = table.Column<decimal>(type: "numeric", nullable: true),
                    BaseCurrency = table.Column<string>(type: "text", nullable: true),
                    CreationTimestamp = table.Column<long>(type: "bigint", nullable: true),
                    CurrentFunding = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookSummaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookSummaries_InstrumentHistories_InstrumentHistoryId",
                        column: x => x.InstrumentHistoryId,
                        principalTable: "InstrumentHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Direction = table.Column<int>(type: "integer", nullable: false),
                    AveragePriceUsd = table.Column<decimal>(type: "numeric", nullable: true),
                    EstimatedLiquidationPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    FloatingProfitLoss = table.Column<decimal>(type: "numeric", nullable: false),
                    FloatingProfitLossUsd = table.Column<decimal>(type: "numeric", nullable: true),
                    OpenOrdersMargin = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalProfitLoss = table.Column<decimal>(type: "numeric", nullable: false),
                    RealizedProfitLoss = table.Column<decimal>(type: "numeric", nullable: true),
                    Delta = table.Column<decimal>(type: "numeric", nullable: false),
                    InitialMargin = table.Column<decimal>(type: "numeric", nullable: false),
                    Size = table.Column<decimal>(type: "numeric", nullable: false),
                    MaintenanceMargin = table.Column<decimal>(type: "numeric", nullable: false),
                    Kind = table.Column<int>(type: "integer", nullable: false),
                    MarkPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    AveragePrice = table.Column<decimal>(type: "numeric", nullable: false),
                    SettlementPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    IndexPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    InstrumentName = table.Column<string>(type: "text", nullable: false),
                    SizeCurrency = table.Column<decimal>(type: "numeric", nullable: true),
                    DailyBalanceId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Positions_DailyBalances_DailyBalanceId",
                        column: x => x.DailyBalanceId,
                        principalTable: "DailyBalances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Greeks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BookDetailId = table.Column<int>(type: "integer", nullable: true),
                    RequestTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    InstrumentName = table.Column<string>(type: "text", nullable: true),
                    Vega = table.Column<decimal>(type: "numeric", nullable: true),
                    Theta = table.Column<decimal>(type: "numeric", nullable: true),
                    Rho = table.Column<decimal>(type: "numeric", nullable: true),
                    Gamma = table.Column<decimal>(type: "numeric", nullable: true),
                    Delta = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Greeks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Greeks_BookDetails_BookDetailId",
                        column: x => x.BookDetailId,
                        principalTable: "BookDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Stats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BookDetailId = table.Column<int>(type: "integer", nullable: true),
                    RequestTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    InstrumentName = table.Column<string>(type: "text", nullable: true),
                    Volume = table.Column<decimal>(type: "numeric", nullable: true),
                    PriceChange = table.Column<decimal>(type: "numeric", nullable: true),
                    Low = table.Column<decimal>(type: "numeric", nullable: true),
                    High = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stats_BookDetails_BookDetailId",
                        column: x => x.BookDetailId,
                        principalTable: "BookDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookDetails_InstrumentHistoryId",
                table: "BookDetails",
                column: "InstrumentHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BookSummaries_InstrumentHistoryId",
                table: "BookSummaries",
                column: "InstrumentHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyBalances_PortfolioEthId",
                table: "DailyBalances",
                column: "PortfolioEthId");

            migrationBuilder.CreateIndex(
                name: "IX_Greeks_BookDetailId",
                table: "Greeks",
                column: "BookDetailId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InstrumentHistories_ActualInstrumentId",
                table: "InstrumentHistories",
                column: "ActualInstrumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_DailyBalanceId",
                table: "Positions",
                column: "DailyBalanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Stats_BookDetailId",
                table: "Stats",
                column: "BookDetailId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ticker_TraderId",
                table: "Ticker",
                column: "TraderId");

            migrationBuilder.CreateIndex(
                name: "IX_VolumeDatas_VolumeAnalId",
                table: "VolumeDatas",
                column: "VolumeAnalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BookSummaries");

            migrationBuilder.DropTable(
                name: "Greeks");

            migrationBuilder.DropTable(
                name: "OptionActivities");

            migrationBuilder.DropTable(
                name: "OptionChainRaw");

            migrationBuilder.DropTable(
                name: "OptionContracts");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "Stats");

            migrationBuilder.DropTable(
                name: "Ticker");

            migrationBuilder.DropTable(
                name: "Tickers");

            migrationBuilder.DropTable(
                name: "VolumeDatas");

            migrationBuilder.DropTable(
                name: "Watchlist");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "DailyBalances");

            migrationBuilder.DropTable(
                name: "BookDetails");

            migrationBuilder.DropTable(
                name: "Traders");

            migrationBuilder.DropTable(
                name: "VolumeAnals");

            migrationBuilder.DropTable(
                name: "PortfoliosEth");

            migrationBuilder.DropTable(
                name: "InstrumentHistories");

            migrationBuilder.DropTable(
                name: "Instruments");
        }
    }
}
