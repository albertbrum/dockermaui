using Microsoft.Extensions.Logging;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using SharedDB.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net.Http;
using Microsoft.AspNetCore.Components.WebView.Maui;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace AppMaui;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Adicionar DbContext para MAUI (cliente local)
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite("Data Source=app.db"));

        // Registrar Blazor WebView para reutilizar a UI compartilhada (SharedUI)
        builder.Services.AddMauiBlazorWebView();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
        builder.Services.AddLogging(logging =>
        {
            logging.AddFilter("Microsoft.AspNetCore.Components.WebView", LogLevel.Trace);
        });
#endif

        // AuthenticationStateProvider necessário para AuthorizeView funcionar no MAUI
        builder.Services.AddAuthorizationCore();
        builder.Services.AddScoped<AuthenticationStateProvider, MauiAuthenticationStateProvider>();

        // Registrar HttpClient para chamadas à API do servidor WebApp.
        builder.Services.AddSingleton(s => new HttpClient { BaseAddress = new Uri("https://localhost:5001") });

        // EmailSender ainda pode ser usado pelo cliente para chamadas locais/depuração
        builder.Services.AddTransient<SharedDB.Services.EmailSender>();

        return builder.Build();
    }
}

/// <summary>
/// AuthenticationStateProvider para MAUI. Inicia como anônimo.
/// Chame NotifyAuthenticationStateChanged após login via API.
/// </summary>
public class MauiAuthenticationStateProvider : AuthenticationStateProvider
{
    private ClaimsPrincipal _currentUser = new(new ClaimsIdentity());

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        return Task.FromResult(new AuthenticationState(_currentUser));
    }

    public void SetUser(ClaimsPrincipal user)
    {
        _currentUser = user;
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public void ClearUser()
    {
        _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}