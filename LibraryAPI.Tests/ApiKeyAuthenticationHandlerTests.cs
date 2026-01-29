using System.Security.Claims;
using System.Text.Encodings.Web;
using LibraryAPI.Tests.TestUtils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging.Abstractions;

namespace LibraryAPI.Tests;

public class ApiKeyAuthenticationHandlerTests
{
    private static ApiKeyAuthenticationHandler CreateHandler(
        DefaultHttpContext httpContext,
        string configuredApiKey)
    {
        var optionsMonitor = new TestOptionsMonitor<ApiKeyAuthenticationOptions>(
            new ApiKeyAuthenticationOptions { ApiKey = configuredApiKey });

        var handler = new ApiKeyAuthenticationHandler(
            optionsMonitor,
            NullLoggerFactory.Instance,
            UrlEncoder.Default);

        var scheme = new AuthenticationScheme("ApiKey", "ApiKey", typeof(ApiKeyAuthenticationHandler));
        handler.InitializeAsync(scheme, httpContext).GetAwaiter().GetResult();
        return handler;
    }

    [Fact]
    public async Task HandleAuthenticateAsync_NoHeader_ReturnsNoResult()
    {
        var context = new DefaultHttpContext();
        var handler = CreateHandler(context, configuredApiKey: "TestKey");

        var result = await handler.AuthenticateAsync();

        Assert.False(result.Succeeded);
        Assert.True(result.None);
    }

    [Fact]
    public async Task HandleAuthenticateAsync_BlankHeader_ReturnsNoResult()
    {
        var context = new DefaultHttpContext();
        context.Request.Headers["X-API-Key"] = "   ";
        var handler = CreateHandler(context, configuredApiKey: "TestKey");

        var result = await handler.AuthenticateAsync();

        Assert.False(result.Succeeded);
        Assert.True(result.None);
    }

    [Fact]
    public async Task HandleAuthenticateAsync_ValidKey_ReturnsSuccessWithExpectedClaim()
    {
        var context = new DefaultHttpContext();
        context.Request.Headers["X-API-Key"] = "TestKey";
        var handler = CreateHandler(context, configuredApiKey: "TestKey");

        var result = await handler.AuthenticateAsync();

        Assert.True(result.Succeeded);
        Assert.NotNull(result.Principal);
        Assert.Equal(
            "ApiKeyUser",
            result.Principal!.FindFirstValue(ClaimTypes.NameIdentifier));
        Assert.Equal("ApiKey", result.Ticket!.AuthenticationScheme);
    }

    [Fact]
    public async Task HandleAuthenticateAsync_InvalidKey_ReturnsFail()
    {
        var context = new DefaultHttpContext();
        context.Request.Headers["X-API-Key"] = "WrongKey";
        var handler = CreateHandler(context, configuredApiKey: "TestKey");

        var result = await handler.AuthenticateAsync();

        Assert.False(result.Succeeded);
        Assert.True(result.Failure is not null);
        Assert.Equal("Invalid API Key", result.Failure!.Message);
    }

    [Fact]
    public async Task HandleAuthenticateAsync_MultipleHeaderValues_UsesFirstValue()
    {
        var context = new DefaultHttpContext();
        context.Request.Headers["X-API-Key"] =
            new Microsoft.Extensions.Primitives.StringValues(new[] { "First", "Second" });
        var handler = CreateHandler(context, configuredApiKey: "First");

        var result = await handler.AuthenticateAsync();

        Assert.True(result.Succeeded);
    }
}

