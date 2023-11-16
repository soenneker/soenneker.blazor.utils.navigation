[![](https://img.shields.io/nuget/v/Soenneker.Blazor.Utils.Navigation.svg?style=for-the-badge)](https://www.nuget.org/packages/Soenneker.Blazor.Utils.Navigation/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blazor.utils.navigation/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.blazor.utils.navigation/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/Soenneker.Blazor.Utils.Navigation.svg?style=for-the-badge)](https://www.nuget.org/packages/Soenneker.Blazor.Utils.Navigation/)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Blazor.Utils.Navigation

Can work side-by-side existing Blazor [NavigationManager](https://learn.microsoft.com/en-us/aspnet/core/blazor/fundamentals/routing?view=aspnetcore-7.0) usage, and still work with navigate back

## Installation

```
Install-Package Soenneker.Blazor.Navigation
```

## Usage

1. Register `INavigationUtil` within `Program.cs` or wherever your registering your services:

```csharp
public static async Task Main(string[] args)
{
    ...
    builder.Services.AddNavigationUtil();
}
```

2. Warm it up so it can begin recording navigation history. Call after the `WebAssemblyHost` has been built:

```csharp
public static async Task Main(string[] args)
{
    ...
    WebAssemblyHost host = builder.Build();

    host.Services.WarmupNavigation();
}
```

3. Inject `INavigationUtil` within pages/components where you need to access navigation methods:


```csharp
@using Soenneker.Blazor.Navigation.Abstract
@inject INavigationUtil NavigationUtil
```


### Navigating back 
```csharp
NavigationUtil.NavigateBack();
```

### Navigating
```csharp
// within the SPA client
NavigationUtil.NavigateTo("/users");

// forcing a page load
NavigationUtil.NavigateTo("/users", true);
```