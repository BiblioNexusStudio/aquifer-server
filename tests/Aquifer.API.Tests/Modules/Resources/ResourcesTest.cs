using Aquifer.API.Modules.Resources;

namespace Aquifer.API.Tests.Modules.Resources;

public class ResourcesTest
{
    // Todo: This test passes but the assertion is just checking if the data was returned.
    // Todo: We need to figure out how to deserialize the data. 
    [Fact]
    public async Task GetResourceContentById_ReturnsRedirectHttpResult()
    {
        // Arrange
        await using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();

        // Act
        var response = await client.GetAsync("/resources/1440/content");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var tipTap = JsonUtilities.DefaultDeserialize(content);
        Assert.NotNull(tipTap);
    }

    [Fact]
    public async Task GetResourceMetadataById_ReturnsResourceItemMetadataResponse()
    {
        // Arrange
        await using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();

        // Act
        var response = await client.GetAsync("/resources/1440/metadata");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var resourceContent = JsonUtilities.DefaultDeserialize<ResourceItemMetadataResponse>(content);
        Assert.NotNull(resourceContent);
    }
}