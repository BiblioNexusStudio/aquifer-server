using Aquifer.API.Modules.Bibles;

namespace Aquifer.API.Tests.Modules.Bibles;

public class BiblesTest
{
    [Fact]
    public async Task GetAllBibles_ReturnsBiblesResponse()
    {
        // Arrange
        await using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();

        // Act
        var response = await client.GetAsync("/bibles");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var bibles = JsonUtilities.DefaultDeserialize<List<BibleResponse>>(content);
        Assert.NotNull(bibles);
        Assert.NotEmpty(bibles);
    }

    [Fact]
    public async Task GetBiblesByLanguage_ReturnsBiblesResponse()
    {
        // Arrange
        await using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();

        // Act
        var response = await client.GetAsync("/bibles/language/1");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var bibles = JsonUtilities.DefaultDeserialize<List<BibleWithBooksMetadataResponse>>(content);
        Assert.NotNull(bibles);
        Assert.NotEmpty(bibles);
    }

    [Fact]
    public async Task GetBibleBookDetails_ReturnsBibleBookResponse()
    {
        // Arrange
        await using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();

        // Act
        var response = await client.GetAsync("/bibles/1/book/GEN");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var bibleBook = JsonUtilities.DefaultDeserialize<BibleBookResponse>(content);
        Assert.NotNull(bibleBook);
    }
}