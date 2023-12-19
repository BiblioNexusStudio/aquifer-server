using Aquifer.API.Modules.Languages;

namespace Aquifer.API.Tests.Modules.Languages;

public class LanguagesTest
{
    [Fact]
    public async Task GetLanguages_ReturnsLanguages()
    {
        // Arrange
        await using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();

        // Act
        var response = await client.GetAsync("/languages");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var languages = JsonUtilities.DefaultDeserialize<List<LanguageResponse>>(content);
        Assert.NotNull(languages);
        Assert.NotEmpty(languages);
    }
}