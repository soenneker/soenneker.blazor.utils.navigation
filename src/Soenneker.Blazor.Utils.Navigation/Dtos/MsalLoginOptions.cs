using Soenneker.Blazor.Utils.Navigation.Enums;
using System.Collections.Generic;

namespace Soenneker.Blazor.Utils.Navigation.Dtos;

/// <summary>
/// Options for MSAL interactive sign-in from Blazor.
/// </summary>
public sealed class MsalLoginOptions
{
    /// <summary>Optional return URL after auth completes.</summary>
    public string? ReturnUrl { get; set; }

    /// <summary>Optional API scopes to request during login.</summary>
    public IEnumerable<string>? Scopes { get; set; }

    /// <summary>Prompt behavior. Use SelectAccount to always show the account picker.</summary>
    public SignInPrompt Prompt { get; set; } = SignInPrompt.Default;

    /// <summary>Extra query parameters to pass to the identity provider.</summary>
    public IDictionary<string, string>? ExtraParameters { get; set; }
}
