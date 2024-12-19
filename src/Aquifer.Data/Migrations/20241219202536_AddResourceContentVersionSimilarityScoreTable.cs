using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddResourceContentVersionSimilarityScoreTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BibleVersionWordGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BibleVersionWordGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContentSubscribers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnsubscribeId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Organization = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    GetNewsletter = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentSubscribers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Feedback",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Feedback = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactType = table.Column<int>(type: "int", nullable: true),
                    FeedbackType = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedback", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GreekNewTestaments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GreekNewTestaments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GreekNewTestamentWordGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GreekNewTestamentWordGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HelpDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    ThumbnailUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HelpDocuments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IpAddressData",
                columns: table => new
                {
                    IpAddress = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    City = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Region = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IpAddressData", x => x.IpAddress);
                });

            migrationBuilder.CreateTable(
                name: "JobHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    LastProcessed = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ISO6393Code = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    EnglishDisplay = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    ScriptDirection = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParentResources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShortName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LicenseInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ComplexityLevel = table.Column<int>(type: "int", nullable: false),
                    ResourceType = table.Column<int>(type: "int", nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    ForMarketing = table.Column<bool>(type: "bit", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParentResources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectPlatforms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectPlatforms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SqlStatement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    AcceptsDateRange = table.Column<bool>(type: "bit", nullable: false),
                    AcceptsLanguage = table.Column<bool>(type: "bit", nullable: false),
                    AcceptsParentResource = table.Column<bool>(type: "bit", nullable: false),
                    AcceptsCompany = table.Column<bool>(type: "bit", nullable: false),
                    DefaultDateRangeMonths = table.Column<int>(type: "int", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResourceContentRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResourceContentId = table.Column<int>(type: "int", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    SubscriptionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndpointId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceContentRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResourceContentVersionSimilarityScores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BaseVersionId = table.Column<int>(type: "int", nullable: false),
                    BaseVersionType = table.Column<int>(type: "int", nullable: false),
                    ComparedVersionId = table.Column<int>(type: "int", nullable: false),
                    ComparedVersionType = table.Column<int>(type: "int", nullable: false),
                    SimilarityScore = table.Column<double>(type: "float", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceContentVersionSimilarityScores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StrongNumbers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StrongNumbers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Verses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Verses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BibleTexts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BibleId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    ChapterNumber = table.Column<int>(type: "int", nullable: false),
                    VerseNumber = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BibleTexts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BibleTexts_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookChapters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    VerseCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookChapters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookChapters_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NewTestamentAlignments",
                columns: table => new
                {
                    BibleVersionWordGroupId = table.Column<int>(type: "int", nullable: false),
                    GreekNewTestamentWordGroupId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewTestamentAlignments", x => new { x.BibleVersionWordGroupId, x.GreekNewTestamentWordGroupId });
                    table.ForeignKey(
                        name: "FK_NewTestamentAlignments_BibleVersionWordGroups_BibleVersionWordGroupId",
                        column: x => x.BibleVersionWordGroupId,
                        principalTable: "BibleVersionWordGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NewTestamentAlignments_GreekNewTestamentWordGroups_GreekNewTestamentWordGroupId",
                        column: x => x.GreekNewTestamentWordGroupId,
                        principalTable: "GreekNewTestamentWordGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bibles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LicenseInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    LanguageDefault = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    RestrictedLicense = table.Column<bool>(type: "bit", nullable: false),
                    GreekAlignment = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bibles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bibles_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyLanguages",
                columns: table => new
                {
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyLanguages", x => new { x.CompanyId, x.LanguageId });
                    table.ForeignKey(
                        name: "FK_CompanyLanguages_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyLanguages_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContentSubscriberLanguages",
                columns: table => new
                {
                    ContentSubscriberId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentSubscriberLanguages", x => new { x.ContentSubscriberId, x.LanguageId });
                    table.ForeignKey(
                        name: "FK_ContentSubscriberLanguages_ContentSubscribers_ContentSubscriberId",
                        column: x => x.ContentSubscriberId,
                        principalTable: "ContentSubscribers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContentSubscriberLanguages_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmailTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    Template = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailTemplates_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TranslationPairs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TranslationPairs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TranslationPairs_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProviderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailVerified = table.Column<bool>(type: "bit", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    AquiferNotificationsEnabled = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ContentSubscriberParentResources",
                columns: table => new
                {
                    ContentSubscriberId = table.Column<int>(type: "int", nullable: false),
                    ParentResourceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentSubscriberParentResources", x => new { x.ContentSubscriberId, x.ParentResourceId });
                    table.ForeignKey(
                        name: "FK_ContentSubscriberParentResources_ContentSubscribers_ContentSubscriberId",
                        column: x => x.ContentSubscriberId,
                        principalTable: "ContentSubscribers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContentSubscriberParentResources_ParentResources_ParentResourceId",
                        column: x => x.ParentResourceId,
                        principalTable: "ParentResources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParentResourceLocalizations",
                columns: table => new
                {
                    ParentResourceId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParentResourceLocalizations", x => new { x.ParentResourceId, x.LanguageId });
                    table.ForeignKey(
                        name: "FK_ParentResourceLocalizations_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParentResourceLocalizations_ParentResources_ParentResourceId",
                        column: x => x.ParentResourceId,
                        principalTable: "ParentResources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentResourceId = table.Column<int>(type: "int", nullable: false),
                    EnglishLabel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExternalId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SortOrder = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resources_ParentResources_ParentResourceId",
                        column: x => x.ParentResourceId,
                        principalTable: "ParentResources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GreekLemmas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StrongNumberId = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GreekLemmas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GreekLemmas_StrongNumbers_StrongNumberId",
                        column: x => x.StrongNumberId,
                        principalTable: "StrongNumbers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Passages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartVerseId = table.Column<int>(type: "int", nullable: false),
                    EndVerseId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Passages_Verses_EndVerseId",
                        column: x => x.EndVerseId,
                        principalTable: "Verses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Passages_Verses_StartVerseId",
                        column: x => x.StartVerseId,
                        principalTable: "Verses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BibleBookContents",
                columns: table => new
                {
                    BibleId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AudioUrls = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TextSize = table.Column<int>(type: "int", nullable: false),
                    AudioSize = table.Column<int>(type: "int", nullable: false),
                    ChapterCount = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BibleBookContents", x => new { x.BibleId, x.BookId });
                    table.ForeignKey(
                        name: "FK_BibleBookContents_Bibles_BibleId",
                        column: x => x.BibleId,
                        principalTable: "Bibles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BibleBookContents_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BibleVersionWords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BibleId = table.Column<int>(type: "int", nullable: false),
                    WordIdentifier = table.Column<long>(type: "bigint", nullable: false),
                    IsPunctuation = table.Column<bool>(type: "bit", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BibleVersionWords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BibleVersionWords_Bibles_BibleId",
                        column: x => x.BibleId,
                        principalTable: "Bibles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VersificationExclusions",
                columns: table => new
                {
                    BibleId = table.Column<int>(type: "int", nullable: false),
                    BibleVerseId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VersificationExclusions", x => new { x.BibleId, x.BibleVerseId });
                    table.ForeignKey(
                        name: "FK_VersificationExclusions_Bibles_BibleId",
                        column: x => x.BibleId,
                        principalTable: "Bibles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VersificationExclusions_Verses_BibleVerseId",
                        column: x => x.BibleVerseId,
                        principalTable: "Verses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VersificationMappings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BibleId = table.Column<int>(type: "int", nullable: false),
                    BibleVerseId = table.Column<int>(type: "int", nullable: false),
                    BaseVerseId = table.Column<int>(type: "int", nullable: false),
                    VerseIdPart = table.Column<string>(type: "nvarchar(1)", nullable: true),
                    BaseVerseIdPart = table.Column<string>(type: "nvarchar(1)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VersificationMappings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VersificationMappings_Bibles_BibleId",
                        column: x => x.BibleId,
                        principalTable: "Bibles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VersificationMappings_Verses_BaseVerseId",
                        column: x => x.BaseVerseId,
                        principalTable: "Verses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VersificationMappings_Verses_BibleVerseId",
                        column: x => x.BibleVerseId,
                        principalTable: "Verses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CommentThreads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Resolved = table.Column<bool>(type: "bit", nullable: false),
                    ResolvedByUserId = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentThreads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentThreads_Users_ResolvedByUserId",
                        column: x => x.ResolvedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompanyReviewers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyReviewers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyReviewers_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyReviewers_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyReviewers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    ProjectManagerUserId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    ProjectPlatformId = table.Column<int>(type: "int", nullable: false),
                    CompanyLeadUserId = table.Column<int>(type: "int", nullable: true),
                    SourceWordCount = table.Column<int>(type: "int", nullable: false),
                    EffectiveWordCount = table.Column<int>(type: "int", nullable: true),
                    QuotedCost = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    Started = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProjectedDeliveryDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ActualDeliveryDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ProjectedPublishDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ActualPublishDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Projects_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Projects_ProjectPlatforms_ProjectPlatformId",
                        column: x => x.ProjectPlatformId,
                        principalTable: "ProjectPlatforms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Projects_Users_CompanyLeadUserId",
                        column: x => x.CompanyLeadUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Projects_Users_ProjectManagerUserId",
                        column: x => x.ProjectManagerUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssociatedResources",
                columns: table => new
                {
                    ResourceId = table.Column<int>(type: "int", nullable: false),
                    AssociatedResourceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssociatedResources", x => new { x.AssociatedResourceId, x.ResourceId });
                    table.ForeignKey(
                        name: "FK_AssociatedResources_Resources_AssociatedResourceId",
                        column: x => x.AssociatedResourceId,
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssociatedResources_Resources_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resources",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BookChapterResources",
                columns: table => new
                {
                    BookChapterId = table.Column<int>(type: "int", nullable: false),
                    ResourceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookChapterResources", x => new { x.BookChapterId, x.ResourceId });
                    table.ForeignKey(
                        name: "FK_BookChapterResources_BookChapters_BookChapterId",
                        column: x => x.BookChapterId,
                        principalTable: "BookChapters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookChapterResources_Resources_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookResources",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false),
                    ResourceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookResources", x => new { x.BookId, x.ResourceId });
                    table.ForeignKey(
                        name: "FK_BookResources_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookResources_Resources_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResourceContents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResourceId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    Trusted = table.Column<bool>(type: "bit", nullable: false),
                    MediaType = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ExternalVersion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContentUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceContents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResourceContents_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResourceContents_Resources_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VerseResources",
                columns: table => new
                {
                    VerseId = table.Column<int>(type: "int", nullable: false),
                    ResourceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerseResources", x => new { x.VerseId, x.ResourceId });
                    table.ForeignKey(
                        name: "FK_VerseResources_Resources_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VerseResources_Verses_VerseId",
                        column: x => x.VerseId,
                        principalTable: "Verses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GreekSenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExternalId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DefinitionShort = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Entrycode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Domain = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubDomain = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GreekLemmaId = table.Column<int>(type: "int", nullable: false),
                    StrongNumberId = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GreekSenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GreekSenses_GreekLemmas_GreekLemmaId",
                        column: x => x.GreekLemmaId,
                        principalTable: "GreekLemmas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GreekSenses_StrongNumbers_StrongNumberId",
                        column: x => x.StrongNumberId,
                        principalTable: "StrongNumbers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GreekWords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GreekLemmaId = table.Column<int>(type: "int", nullable: false),
                    GrammarType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsageCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GreekWords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GreekWords_GreekLemmas_GreekLemmaId",
                        column: x => x.GreekLemmaId,
                        principalTable: "GreekLemmas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PassageResources",
                columns: table => new
                {
                    PassageId = table.Column<int>(type: "int", nullable: false),
                    ResourceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PassageResources", x => new { x.PassageId, x.ResourceId });
                    table.ForeignKey(
                        name: "FK_PassageResources_Passages_PassageId",
                        column: x => x.PassageId,
                        principalTable: "Passages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PassageResources_Resources_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BibleVersionWordGroupWords",
                columns: table => new
                {
                    BibleVersionWordGroupId = table.Column<int>(type: "int", nullable: false),
                    BibleVersionWordId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BibleVersionWordGroupWords", x => new { x.BibleVersionWordGroupId, x.BibleVersionWordId });
                    table.ForeignKey(
                        name: "FK_BibleVersionWordGroupWords_BibleVersionWordGroups_BibleVersionWordGroupId",
                        column: x => x.BibleVersionWordGroupId,
                        principalTable: "BibleVersionWordGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BibleVersionWordGroupWords_BibleVersionWords_BibleVersionWordId",
                        column: x => x.BibleVersionWordId,
                        principalTable: "BibleVersionWords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ThreadId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_CommentThreads_ThreadId",
                        column: x => x.ThreadId,
                        principalTable: "CommentThreads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectResourceContents",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    ResourceContentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectResourceContents", x => new { x.ProjectId, x.ResourceContentId });
                    table.ForeignKey(
                        name: "FK_ProjectResourceContents_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectResourceContents_ResourceContents_ResourceContentId",
                        column: x => x.ResourceContentId,
                        principalTable: "ResourceContents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ResourceContentVersions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResourceContentId = table.Column<int>(type: "int", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    IsDraft = table.Column<bool>(type: "bit", nullable: false),
                    IsPublished = table.Column<bool>(type: "bit", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentSize = table.Column<int>(type: "int", nullable: false),
                    WordCount = table.Column<int>(type: "int", nullable: true),
                    SourceWordCount = table.Column<int>(type: "int", nullable: true),
                    InlineMediaSize = table.Column<int>(type: "int", nullable: true),
                    AssignedUserId = table.Column<int>(type: "int", nullable: true),
                    ReviewLevel = table.Column<int>(type: "int", nullable: false),
                    AssignedReviewerUserId = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceContentVersions", x => x.Id);
                    table.CheckConstraint("CK_ResourceContentVersions_IsPublishedOrIsDraftNotBoth", "IsPublished = 0 OR IsDraft = 0");
                    table.ForeignKey(
                        name: "FK_ResourceContentVersions_ResourceContents_ResourceContentId",
                        column: x => x.ResourceContentId,
                        principalTable: "ResourceContents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResourceContentVersions_Users_AssignedReviewerUserId",
                        column: x => x.AssignedReviewerUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ResourceContentVersions_Users_AssignedUserId",
                        column: x => x.AssignedUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GreekSenseGlosses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GreekSenseId = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GreekSenseGlosses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GreekSenseGlosses_GreekSenses_GreekSenseId",
                        column: x => x.GreekSenseId,
                        principalTable: "GreekSenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GreekNewTestamentWords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GreekNewTestamentId = table.Column<int>(type: "int", nullable: false),
                    GreekWordId = table.Column<int>(type: "int", nullable: false),
                    WordIdentifier = table.Column<long>(type: "bigint", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GreekNewTestamentWords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GreekNewTestamentWords_GreekNewTestaments_GreekNewTestamentId",
                        column: x => x.GreekNewTestamentId,
                        principalTable: "GreekNewTestaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GreekNewTestamentWords_GreekWords_GreekWordId",
                        column: x => x.GreekWordId,
                        principalTable: "GreekWords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResourceContentVersionAssignedUserHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResourceContentVersionId = table.Column<int>(type: "int", nullable: false),
                    AssignedUserId = table.Column<int>(type: "int", nullable: true),
                    ChangedByUserId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceContentVersionAssignedUserHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResourceContentVersionAssignedUserHistory_ResourceContentVersions_ResourceContentVersionId",
                        column: x => x.ResourceContentVersionId,
                        principalTable: "ResourceContentVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResourceContentVersionAssignedUserHistory_Users_AssignedUserId",
                        column: x => x.AssignedUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ResourceContentVersionAssignedUserHistory_Users_ChangedByUserId",
                        column: x => x.ChangedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ResourceContentVersionCommentThreads",
                columns: table => new
                {
                    CommentThreadId = table.Column<int>(type: "int", nullable: false),
                    ResourceContentVersionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceContentVersionCommentThreads", x => new { x.CommentThreadId, x.ResourceContentVersionId });
                    table.ForeignKey(
                        name: "FK_ResourceContentVersionCommentThreads_CommentThreads_CommentThreadId",
                        column: x => x.CommentThreadId,
                        principalTable: "CommentThreads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResourceContentVersionCommentThreads_ResourceContentVersions_ResourceContentVersionId",
                        column: x => x.ResourceContentVersionId,
                        principalTable: "ResourceContentVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResourceContentVersionEditTimes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResourceContentVersionId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceContentVersionEditTimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResourceContentVersionEditTimes_ResourceContentVersions_ResourceContentVersionId",
                        column: x => x.ResourceContentVersionId,
                        principalTable: "ResourceContentVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResourceContentVersionEditTimes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResourceContentVersionFeedback",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResourceContentVersionId = table.Column<int>(type: "int", nullable: false),
                    ContactValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactType = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Feedback = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserRating = table.Column<byte>(type: "tinyint", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceContentVersionFeedback", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResourceContentVersionFeedback_ResourceContentVersions_ResourceContentVersionId",
                        column: x => x.ResourceContentVersionId,
                        principalTable: "ResourceContentVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResourceContentVersionMachineTranslations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResourceContentVersionId = table.Column<int>(type: "int", nullable: false),
                    SourceId = table.Column<int>(type: "int", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentIndex = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    UserRating = table.Column<byte>(type: "tinyint", nullable: false),
                    ImproveClarity = table.Column<bool>(type: "bit", nullable: false),
                    ImproveTone = table.Column<bool>(type: "bit", nullable: false),
                    ImproveConsistency = table.Column<bool>(type: "bit", nullable: false),
                    RetranslationReason = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceContentVersionMachineTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResourceContentVersionMachineTranslations_ResourceContentVersions_ResourceContentVersionId",
                        column: x => x.ResourceContentVersionId,
                        principalTable: "ResourceContentVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResourceContentVersionMachineTranslations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ResourceContentVersionSnapshots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResourceContentVersionId = table.Column<int>(type: "int", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WordCount = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceContentVersionSnapshots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResourceContentVersionSnapshots_ResourceContentVersions_ResourceContentVersionId",
                        column: x => x.ResourceContentVersionId,
                        principalTable: "ResourceContentVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResourceContentVersionSnapshots_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ResourceContentVersionStatusHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResourceContentVersionId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ChangedByUserId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceContentVersionStatusHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResourceContentVersionStatusHistory_ResourceContentVersions_ResourceContentVersionId",
                        column: x => x.ResourceContentVersionId,
                        principalTable: "ResourceContentVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResourceContentVersionStatusHistory_Users_ChangedByUserId",
                        column: x => x.ChangedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GreekNewTestamentWordGroupWords",
                columns: table => new
                {
                    GreekNewTestamentWordGroupId = table.Column<int>(type: "int", nullable: false),
                    GreekNewTestamentWordId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GreekNewTestamentWordGroupWords", x => new { x.GreekNewTestamentWordGroupId, x.GreekNewTestamentWordId });
                    table.ForeignKey(
                        name: "FK_GreekNewTestamentWordGroupWords_GreekNewTestamentWordGroups_GreekNewTestamentWordGroupId",
                        column: x => x.GreekNewTestamentWordGroupId,
                        principalTable: "GreekNewTestamentWordGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GreekNewTestamentWordGroupWords_GreekNewTestamentWords_GreekNewTestamentWordId",
                        column: x => x.GreekNewTestamentWordId,
                        principalTable: "GreekNewTestamentWords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GreekNewTestamentWordSenses",
                columns: table => new
                {
                    GreekNewTestamentWordId = table.Column<int>(type: "int", nullable: false),
                    GreekSenseId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GreekNewTestamentWordSenses", x => new { x.GreekNewTestamentWordId, x.GreekSenseId });
                    table.ForeignKey(
                        name: "FK_GreekNewTestamentWordSenses_GreekNewTestamentWords_GreekNewTestamentWordId",
                        column: x => x.GreekNewTestamentWordId,
                        principalTable: "GreekNewTestamentWords",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GreekNewTestamentWordSenses_GreekSenses_GreekSenseId",
                        column: x => x.GreekSenseId,
                        principalTable: "GreekSenses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssociatedResources_ResourceId",
                table: "AssociatedResources",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Bibles_LanguageId_LanguageDefault",
                table: "Bibles",
                column: "LanguageId",
                unique: true,
                filter: "LanguageDefault = 1");

            migrationBuilder.CreateIndex(
                name: "IX_BibleTexts_BibleId_BookId_ChapterNumber_VerseNumber",
                table: "BibleTexts",
                columns: new[] { "BibleId", "BookId", "ChapterNumber", "VerseNumber" });

            migrationBuilder.CreateIndex(
                name: "IX_BibleVersionWordGroupWords_BibleVersionWordId",
                table: "BibleVersionWordGroupWords",
                column: "BibleVersionWordId");

            migrationBuilder.CreateIndex(
                name: "IX_BibleVersionWords_BibleId_WordIdentifier",
                table: "BibleVersionWords",
                columns: new[] { "BibleId", "WordIdentifier" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookChapterResources_ResourceId",
                table: "BookChapterResources",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_BookResources_ResourceId",
                table: "BookResources",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ThreadId",
                table: "Comments",
                column: "ThreadId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyReviewers_UserId_CompanyId_LanguageId",
                table: "CompanyReviewers",
                columns: new[] { "UserId", "CompanyId", "LanguageId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GreekNewTestamentWords_GreekNewTestamentId_WordIdentifier",
                table: "GreekNewTestamentWords",
                columns: new[] { "GreekNewTestamentId", "WordIdentifier" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GreekSenseGlosses_GreekSenseId",
                table: "GreekSenseGlosses",
                column: "GreekSenseId");

            migrationBuilder.CreateIndex(
                name: "IX_JobHistory_JobId",
                table: "JobHistory",
                column: "JobId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Languages_ISO6393Code",
                table: "Languages",
                column: "ISO6393Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PassageResources_ResourceId",
                table: "PassageResources",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Passages_StartVerseId_EndVerseId",
                table: "Passages",
                columns: new[] { "StartVerseId", "EndVerseId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectResourceContents_ResourceContentId",
                table: "ProjectResourceContents",
                column: "ResourceContentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_Name",
                table: "Projects",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reports_Slug",
                table: "Reports",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResourceContentRequests_Created",
                table: "ResourceContentRequests",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceContents_LanguageId_MediaType",
                table: "ResourceContents",
                columns: new[] { "LanguageId", "MediaType" })
                .Annotation("SqlServer:Include", new[] { "Created", "ResourceId", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_ResourceContents_ResourceId_LanguageId_MediaType",
                table: "ResourceContents",
                columns: new[] { "ResourceId", "LanguageId", "MediaType" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResourceContents_Status",
                table: "ResourceContents",
                column: "Status")
                .Annotation("SqlServer:Include", new[] { "ContentUpdated", "LanguageId", "ResourceId" });

            migrationBuilder.CreateIndex(
                name: "IX_ResourceContentVersionAssignedUserHistory_ChangedByUserId_Created",
                table: "ResourceContentVersionAssignedUserHistory",
                columns: new[] { "ChangedByUserId", "Created" })
                .Annotation("SqlServer:Include", new[] { "ResourceContentVersionId" });

            migrationBuilder.CreateIndex(
                name: "IX_ResourceContentVersionAssignedUserHistory_Created",
                table: "ResourceContentVersionAssignedUserHistory",
                column: "Created")
                .Annotation("SqlServer:Include", new[] { "AssignedUserId", "ResourceContentVersionId" });

            migrationBuilder.CreateIndex(
                name: "IX_ResourceContentVersionAssignedUserHistory_ResourceContentVersionId_AssignedUserId",
                table: "ResourceContentVersionAssignedUserHistory",
                columns: new[] { "ResourceContentVersionId", "AssignedUserId" });

            migrationBuilder.CreateIndex(
                name: "IX_ResourceContentVersionEditTimes_UserId_Created",
                table: "ResourceContentVersionEditTimes",
                columns: new[] { "UserId", "Created" });

            migrationBuilder.CreateIndex(
                name: "IX_ResourceContentVersionMachineTranslations_ResourceContentVersionId",
                table: "ResourceContentVersionMachineTranslations",
                column: "ResourceContentVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceContentVersions_AssignedUserId",
                table: "ResourceContentVersions",
                column: "AssignedUserId")
                .Annotation("SqlServer:Include", new[] { "ResourceContentId", "SourceWordCount" });

            migrationBuilder.CreateIndex(
                name: "IX_ResourceContentVersions_ResourceContentId_IsDraft",
                table: "ResourceContentVersions",
                column: "ResourceContentId",
                unique: true,
                filter: "IsDraft = 1");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceContentVersions_ResourceContentId_Version",
                table: "ResourceContentVersions",
                columns: new[] { "ResourceContentId", "Version" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResourceContentVersionSnapshots_ResourceContentVersionId",
                table: "ResourceContentVersionSnapshots",
                column: "ResourceContentVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceContentVersionStatusHistory_ChangedByUserId_Created",
                table: "ResourceContentVersionStatusHistory",
                columns: new[] { "ChangedByUserId", "Created" });

            migrationBuilder.CreateIndex(
                name: "IX_ResourceContentVersionStatusHistory_Status_ResourceContentVersionId_Created",
                table: "ResourceContentVersionStatusHistory",
                columns: new[] { "Status", "ResourceContentVersionId", "Created" });

            migrationBuilder.CreateIndex(
                name: "IX_Resources_ExternalId",
                table: "Resources",
                column: "ExternalId");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_ParentResourceId_ExternalId",
                table: "Resources",
                columns: new[] { "ParentResourceId", "ExternalId" },
                unique: true,
                filter: "[ExternalId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TranslationPairs_LanguageId_Key",
                table: "TranslationPairs",
                columns: new[] { "LanguageId", "Key" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ProviderId",
                table: "Users",
                column: "ProviderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VerseResources_ResourceId",
                table: "VerseResources",
                column: "ResourceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssociatedResources");

            migrationBuilder.DropTable(
                name: "BibleBookContents");

            migrationBuilder.DropTable(
                name: "BibleTexts");

            migrationBuilder.DropTable(
                name: "BibleVersionWordGroupWords");

            migrationBuilder.DropTable(
                name: "BookChapterResources");

            migrationBuilder.DropTable(
                name: "BookResources");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "CompanyLanguages");

            migrationBuilder.DropTable(
                name: "CompanyReviewers");

            migrationBuilder.DropTable(
                name: "ContentSubscriberLanguages");

            migrationBuilder.DropTable(
                name: "ContentSubscriberParentResources");

            migrationBuilder.DropTable(
                name: "EmailTemplates");

            migrationBuilder.DropTable(
                name: "Feedback");

            migrationBuilder.DropTable(
                name: "GreekNewTestamentWordGroupWords");

            migrationBuilder.DropTable(
                name: "GreekNewTestamentWordSenses");

            migrationBuilder.DropTable(
                name: "GreekSenseGlosses");

            migrationBuilder.DropTable(
                name: "HelpDocuments");

            migrationBuilder.DropTable(
                name: "IpAddressData");

            migrationBuilder.DropTable(
                name: "JobHistory");

            migrationBuilder.DropTable(
                name: "NewTestamentAlignments");

            migrationBuilder.DropTable(
                name: "ParentResourceLocalizations");

            migrationBuilder.DropTable(
                name: "PassageResources");

            migrationBuilder.DropTable(
                name: "ProjectResourceContents");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "ResourceContentRequests");

            migrationBuilder.DropTable(
                name: "ResourceContentVersionAssignedUserHistory");

            migrationBuilder.DropTable(
                name: "ResourceContentVersionCommentThreads");

            migrationBuilder.DropTable(
                name: "ResourceContentVersionEditTimes");

            migrationBuilder.DropTable(
                name: "ResourceContentVersionFeedback");

            migrationBuilder.DropTable(
                name: "ResourceContentVersionMachineTranslations");

            migrationBuilder.DropTable(
                name: "ResourceContentVersionSimilarityScores");

            migrationBuilder.DropTable(
                name: "ResourceContentVersionSnapshots");

            migrationBuilder.DropTable(
                name: "ResourceContentVersionStatusHistory");

            migrationBuilder.DropTable(
                name: "TranslationPairs");

            migrationBuilder.DropTable(
                name: "VerseResources");

            migrationBuilder.DropTable(
                name: "VersificationExclusions");

            migrationBuilder.DropTable(
                name: "VersificationMappings");

            migrationBuilder.DropTable(
                name: "BibleVersionWords");

            migrationBuilder.DropTable(
                name: "BookChapters");

            migrationBuilder.DropTable(
                name: "ContentSubscribers");

            migrationBuilder.DropTable(
                name: "GreekNewTestamentWords");

            migrationBuilder.DropTable(
                name: "GreekSenses");

            migrationBuilder.DropTable(
                name: "BibleVersionWordGroups");

            migrationBuilder.DropTable(
                name: "GreekNewTestamentWordGroups");

            migrationBuilder.DropTable(
                name: "Passages");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "CommentThreads");

            migrationBuilder.DropTable(
                name: "ResourceContentVersions");

            migrationBuilder.DropTable(
                name: "Bibles");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "GreekNewTestaments");

            migrationBuilder.DropTable(
                name: "GreekWords");

            migrationBuilder.DropTable(
                name: "Verses");

            migrationBuilder.DropTable(
                name: "ProjectPlatforms");

            migrationBuilder.DropTable(
                name: "ResourceContents");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "GreekLemmas");

            migrationBuilder.DropTable(
                name: "Resources");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "StrongNumbers");

            migrationBuilder.DropTable(
                name: "ParentResources");
        }
    }
}
