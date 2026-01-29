using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace LibraryAPI.Tests;

public class BooksEndpointAuthTests
{
    private const string TestApiKey = "TestApiKey123";

    private sealed class TestAppFactory : WebApplicationFactory<Program>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(config =>
            {
                config.AddInMemoryCollection(new Dictionary<string, string?>
                {
                    ["ApiKey"] = TestApiKey
                });
            });

            return base.CreateHost(builder);
        }
    }

    private static HttpClient CreateClient(TestAppFactory factory)
    {
        // Avoid HTTPS redirection (307) by issuing "https://" requests in tests.
        return factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            BaseAddress = new Uri("https://localhost"),
            AllowAutoRedirect = false
        });
    }

    [Fact]
    public async Task GetBooks_NoApiKeyHeader_ReturnsUnauthorized()
    {
        await using var factory = new TestAppFactory();
        using var client = CreateClient(factory);

        var response = await client.GetAsync("/Books");

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetBooks_InvalidApiKey_ReturnsUnauthorized()
    {
        await using var factory = new TestAppFactory();
        using var client = CreateClient(factory);
        client.DefaultRequestHeaders.Add("X-API-Key", "WrongKey");

        var response = await client.GetAsync("/Books");

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetBooks_ValidApiKey_ReturnsOk()
    {
        await using var factory = new TestAppFactory();
        using var client = CreateClient(factory);
        client.DefaultRequestHeaders.Add("X-API-Key", TestApiKey);

        var response = await client.GetAsync("/Books");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}

