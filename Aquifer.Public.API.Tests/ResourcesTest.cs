using Aquifer.Data;
using Aquifer.Data.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.Public.API.Tests;

public class ResourcesTest : IDisposable
{
    private readonly SqliteConnection _connection;
    private readonly DbContextOptions<AquiferDbContext> _contextOptions;


    #region ConstructorAndDispose

    public ResourcesTest()
    {
        // Create and open a connection. This creates the SQLite in-memory database, which will persist until the connection is closed
        // at the end of the test (see Dispose below).
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();
        

        // Add getutcdate() function. Exists in SQL Server, but not SQLite, so need to add
        _connection.CreateFunction("getutcdate", () => DateTime.UtcNow);

        // These options will be used by the context instances in this test suite, including the connection opened above.
        _contextOptions = new DbContextOptionsBuilder<AquiferDbContext>()
            .UseSqlite(_connection)
            .Options;

        // Create the schema and seed some data
        using var context = new AquiferDbContext(_contextOptions);
        context.Database.EnsureCreated();

        var parentResource = new ParentResourceEntity
        {
            ShortName = "CBBTER",
            DisplayName = "Translation Guide (SRV)",
            ComplexityLevel = ResourceTypeComplexityLevel.Basic,
            ResourceType = ResourceType.Guide
        };
        var resource = new ResourceEntity
        {
            ParentResource = parentResource, EnglishLabel = "FIA Luke 11:14-32", ExternalId = "FIA Luke 11:14-32"
        };

        var language =
            new LanguageEntity
            {
                ISO6393Code = "eng",
                EnglishDisplay = "English",
                DisplayName = "English",
                ScriptDirection = ScriptDirection.LTR,
                Enabled = true
            };
        var company = new CompanyEntity { Name = "TestCompany" };
        var projectPlatform = new ProjectPlatformEntity { Name = "TestPlatformEntity" }; 
        
        context.AddRange(language, parentResource, company);
        context.SaveChanges();
        context.Add(projectPlatform);
        context.SaveChanges();
        context.AddRange(
            resource,
            new ResourceContentEntity
            {
                Resource = resource,
                Language = language,
                Status = ResourceContentStatus.New,
                MediaType = ResourceContentMediaType.Text,
                Versions =
                [
                    new ResourceContentVersionEntity
                    {
                        AssignedUser = new UserEntity
                        {
                            Company = company,
                            Email = "testing@biblionexus.org",
                            EmailVerified = true,
                            FirstName = "TestUser",
                            LastName = "UnitTester",
                            Role = UserRole.Admin,
                            ProviderId = "auth0|blahblahblah"
                        },
                        DisplayName = "Here Is a Display Name",
                        Content = "Here is some content about stuff"
                    }
                ]
            },
            new PassageEntity { EndVerse = new VerseEntity { Id = 1045008025 }, StartVerse = new VerseEntity { Id = 1045008004 } });

        context.SaveChanges();
    }

    // dumb smoke test just to show db getting successfully seeded.
    [Fact]
    public void GetUser()
    {
        using var context = CreateContext();
        var userFirstName = context.Users.Where(u => u.FirstName == "TestUser").Select(u => u.FirstName).Single();
        Assert.Equal("TestUser", userFirstName);
        Dispose();
    }


    private AquiferDbContext CreateContext()
    {
        return new AquiferDbContext(_contextOptions);
    }

    public void Dispose()
    {
        _connection.Dispose();
    }

    #endregion
}