using InsuranceWebApi.Application.AppServices.Results;
using InsuranceWebApi.Application.Commands.Requests;
using InsuranceWebApi.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace InsuranceWebApi.Test.WebApi.Controllers;

public class AdvisorControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public AdvisorControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Get_ReturnsListOfAdvisorsEmpty()
    {
        // Arrange
        var seed1 = new AdvisorCreateUpdateRequest
        {
            Sin = "123456789",
            Name = "Joe John",
            Phone = "12345678",
            Address = "123 test street"
        };

        var seed2 = new AdvisorCreateUpdateRequest
        {
            Sin = "123456788",
            Name = "Mary John",
            Phone = "87654321",
            Address = "125 test street"
        };

        var seed3 = new AdvisorCreateUpdateRequest
        {
            Sin = "123456787",
            Name = "John Berry",
            Phone = "12348765",
            Address = "133 test ave"
        };

        await _client.PostAsync("/api/advisor", JsonContent.Create(seed1));

        await _client.PostAsync("/api/advisor", JsonContent.Create(seed2));

        await _client.PostAsync("/api/advisor", JsonContent.Create(seed3));

        // Act
        var response = await _client.GetAsync("/api/advisor");
        response.EnsureSuccessStatusCode();

        // Assert
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
        var result = JsonConvert.DeserializeObject<CommandResult<IEnumerable<Advisor>>>(content);

        result.Data.Should().HaveCount(3);
        result.Data.Should().ContainEquivalentOf(seed1)
                            .And.ContainEquivalentOf(seed2)
                            .And.ContainEquivalentOf(seed3);
    }

    [Fact]
    public async Task GetbyId_ReturnsSingleAdvisor()
    {
        // arrange
        var seed1 = new AdvisorCreateUpdateRequest
        {
            Sin = "123456789",
            Name = "Joe John",
            Phone = "12345678",
            Address = "123 test street"
        };

        var seed2 = new AdvisorCreateUpdateRequest
        {
            Sin = "123456788",
            Name = "Mary John",
            Phone = "87654321",
            Address = "125 test street"
        };

        var seed3 = new AdvisorCreateUpdateRequest
        {
            Sin = "123456787",
            Name = "John Berry",
            Phone = "12348765",
            Address = "133 test ave"
        };

        await _client.PostAsync("/api/advisor", JsonContent.Create(seed1));

        await _client.PostAsync("/api/advisor", JsonContent.Create(seed2));

        await _client.PostAsync("/api/advisor", JsonContent.Create(seed3));


        //act
        var sin = "123456789"; 

        var response = await _client.GetAsync($"/api/advisor/{sin}");
        response.EnsureSuccessStatusCode();

        // Assert
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
        var result = JsonConvert.DeserializeObject<CommandResult<Advisor>>(content);
       
        result.Data.Should().BeEquivalentTo(seed1);
    }


    [Fact]
    public async Task Create_ReturnsSingleAdvisor()
    {
        // act
        var seed = new AdvisorCreateUpdateRequest
        {
            Sin = "123456789",
            Name = "Joe John",
            Phone = "12345678",
            Address = "123 test street"
        };

        await _client.PostAsync("/api/advisor", JsonContent.Create(seed));

        // Assert
        var sin = "123456789";

        var response = await _client.GetAsync($"/api/advisor/{sin}");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
        var result = JsonConvert.DeserializeObject<CommandResult<Advisor>>(content);

        result.Data.Should().BeEquivalentTo(seed);
    }

    [Fact]
    public async Task Update_ReturnsSingleAdvisor()
    {
        // arrange
        var seed = new AdvisorCreateUpdateRequest
        {
            Sin = "123456789",
            Name = "Joe John",
            Phone = "12345678",
            Address = "123 test street"
        };

        await _client.PostAsync("/api/advisor", JsonContent.Create(seed));

        // act 

        seed = new AdvisorCreateUpdateRequest
        {
            Sin = "123456789",
            Name = "Joe lisson",
            Phone = "87654321",
            Address = "453 test street"
        };

        await _client.PutAsync($"/api/advisor/{seed.Sin}", JsonContent.Create(seed));

        // Assert
        var sin = "123456789";

        var response = await _client.GetAsync($"/api/advisor/{sin}");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
        var result = JsonConvert.DeserializeObject<CommandResult<Advisor>>(content);

        result.Data.Should().BeEquivalentTo(seed);
    }

    [Fact]
    public async Task Delete_ReturnsListOfAdvisorsEmpty()
    {
        // Arrange
        var seed1 = new AdvisorCreateUpdateRequest
        {
            Sin = "123456789",
            Name = "Joe John",
            Phone = "12345678",
            Address = "123 test street"
        };

        var seed2 = new AdvisorCreateUpdateRequest
        {
            Sin = "123456788",
            Name = "Mary John",
            Phone = "87654321",
            Address = "125 test street"
        };

        var seed3 = new AdvisorCreateUpdateRequest
        {
            Sin = "123456787",
            Name = "John Berry",
            Phone = "12348765",
            Address = "133 test ave"
        };

        await _client.PostAsync("/api/advisor", JsonContent.Create(seed1));

        await _client.PostAsync("/api/advisor", JsonContent.Create(seed2));

        await _client.PostAsync("/api/advisor", JsonContent.Create(seed3));

        // Act
        var sin = "123456788";
        var response = await _client.DeleteAsync($"/api/advisor/{sin}");
        response.EnsureSuccessStatusCode();

        // Assert
        var responseAssert = await _client.GetAsync("/api/advisor");
        var contentAssert = await responseAssert.Content.ReadAsStringAsync();
        contentAssert.Should().NotBeNullOrEmpty();
        var result = JsonConvert.DeserializeObject<CommandResult<IEnumerable<Advisor>>>(contentAssert);

        result.Data.Should().HaveCount(2);
        result.Data.Should().ContainEquivalentOf(seed1)
                            .And.NotContainEquivalentOf(seed2)
                            .And.ContainEquivalentOf(seed3);
    }
}
