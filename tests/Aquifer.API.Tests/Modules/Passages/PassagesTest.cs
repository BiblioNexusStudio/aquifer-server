using Aquifer.API.Modules.Passages;

namespace Aquifer.API.Tests.Modules.Passages;

public class PassagesTest
{
    [Fact]
    public async Task PassagesByLanguageAndResource_ReturnsPassagesByBookResponse()
    {
        // Arrange
        await using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();

        // Act
        var response = await client.GetAsync("/passages/language/1/resource/CBBTER");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var passages = JsonUtilities.DefaultDeserialize<List<PassagesByBookResponse>>(content);
        Assert.NotNull(passages);
        Assert.NotEmpty(passages);
    }

    [Fact]
    public async Task PassageDetailsForLanguage_ReturnsPassageResponse()
    {
        // Arrange
        await using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();

        // Act
        var response = await client.GetAsync("/passages/2/language/1");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var passage = JsonUtilities.DefaultDeserialize<PassageResponse>(content);
        Assert.NotNull(passage);
    }
}