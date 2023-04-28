using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Utils.Navigation.Abstract;

namespace Soenneker.Blazor.Utils.Navigation.Registrars;

/// <summary>
/// A Blazor WebAssembly library that features navigate back, login/logout, reload and more
/// </summary>
public static class NavigationRegistrar
{
    /// <summary>
    /// Shorthand for <code>services.TryAddScoped</code>
    /// </summary>
    public static void AddNavigationUtil(this IServiceCollection services)
    {
        services.TryAddScoped<INavigationUtil, NavigationUtil>();
    }

    /// <summary>
    /// Call AFTER the WebAssembly/IServiceProvider has been built, aka builder.Build()
    /// </summary>
    public static void WarmupNavigation(this IServiceProvider provider)
    {
        provider.GetService<INavigationUtil>();
    }
}