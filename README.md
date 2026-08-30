[![](https://img.shields.io/nuget/v/Soenneker.Blazor.Utils.Navigation.svg?style=for-the-badge)](https://www.nuget.org/packages/Soenneker.Blazor.Utils.Navigation/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blazor.utils.navigation/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.blazor.utils.navigation/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/Soenneker.Blazor.Utils.Navigation.svg?style=for-the-badge)](https://www.nuget.org/packages/Soenneker.Blazor.Utils.Navigation/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blazor.utils.navigation/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.blazor.utils.navigation/actions/workflows/codeql.yml)

# Soenneker.Blazor.Utils.Navigation

A scoped Blazor WebAssembly navigation helper that tracks observed locations, builds encoded query strings, and wraps the built-in MSAL login/logout navigation extensions.

It complements `NavigationManager`; it does not replace Blazor routing or the browser History API.

## Installation

```bash
dotnet add package Soenneker.Blazor.Utils.Navigation
```

```csharp
using Soenneker.Blazor.Utils.Navigation.Registrars;

builder.Services.AddNavigationUtilAsScoped();
```

The service starts tracking locations when it is first resolved. Resolve it immediately after building a WebAssembly host if navigation can occur before the first component injects it:

```csharp
WebAssemblyHost host = builder.Build();
host.Services.WarmupNavigation();

await host.RunAsync();
```

```razor
@using Soenneker.Blazor.Utils.Navigation.Abstract
@inject INavigationUtil Navigation
```

## Navigate and add query parameters

```csharp
Navigation.NavigateTo("/orders");
Navigation.NavigateTo("/orders", forceLoad: true);
```

`forceLoad: false` uses normal Blazor navigation when possible. `forceLoad: true` asks the browser to load the destination document.

Query names and values are encoded by `QueryHelpers`:

```csharp
Navigation.NavigateTo("/search", new Dictionary<string, string>
{
    ["q"] = "red shoes",
    ["sort"] = "newest"
});
```

`NavigateTo` accepts relative or absolute destinations just like `NavigationManager`. If a destination can be influenced by a user, validate it before navigating; otherwise it can become an open redirect to an external origin.

## Navigate to the previously observed location

```csharp
if (Navigation.CanNavigateBack)
    Navigation.NavigateBack();
```

The utility records `NavigationManager.LocationChanged` events in memory and navigates to the prior recorded URI. This is not `window.history.back()` and does not inspect the browser’s history stack.

Browser back/forward actions are themselves recorded as new observations. For example, after the browser moves from C back to B, the utility’s previous observed location can be C. Use the browser History API directly when exact browser-stack semantics are required.

History exists only for the current service instance, is capped to recent entries, and is lost on a full page load.

## Reload and current URI

```csharp
Uri current = Navigation.GetCurrentUri();
Navigation.Reload(forceLoad: true);
```

Use `forceLoad: true` for an actual document reload. With `false`, navigating to the current URI follows `NavigationManager` SPA behavior and may be a no-op.

## MSAL sign-in

```csharp
Navigation.Login("authentication/login", new MsalLoginOptions
{
    ReturnUrl = "/account",
    Scopes = ["api://example/orders.read"],
    Prompt = SignInPrompt.SelectAccount,
    ExtraParameters = new Dictionary<string, string>
    {
        ["domain_hint"] = "example.com"
    }
});
```

Convenience account-picker flow:

```csharp
Navigation.LoginSelectAccount(
    returnUrl: "/account",
    scopes: ["api://example/orders.read"]);
```

Supported prompt values are `Default`, `SelectAccount`, `Login`, `Consent`, and `None`. `Default` omits the prompt parameter. The explicit options request identity-provider behavior but do not guarantee it; providers can apply their own policy.

The login path and return URL must remain within the application base URI. Extra parameters cannot override the selected `prompt`. Keep paths, scopes, and protocol parameters under application control rather than copying arbitrary URL input into them.

## Sign out

```csharp
Navigation.Logout(
    logoutPath: "authentication/logout",
    returnUrl: "/signed-out");
```

The logout path and return URL are also restricted to the application base URI. Server-side identity and API authorization remain authoritative; client navigation helpers do not enforce authentication or access control.
