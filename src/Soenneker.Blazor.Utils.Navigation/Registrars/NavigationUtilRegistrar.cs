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
    public static IServiceCollection AddNavigationUtilAsScoped(this IServiceCollection services)
    {
        services.TryAddScoped<INavigationUtil, NavigationUtil>();
        return services;
    }

    /// <summary>
    /// Call AFTER the WebAssembly/IServiceProvider has been built, aka builder.Build()
    /// </summary>
    public static IServiceProvider WarmupNavigation(this IServiceProvider provider)
    {
        provider.GetService<INavigationUtil>();
        return provider;
    }
}
