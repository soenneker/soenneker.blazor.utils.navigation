using Soenneker.Blazor.Utils.Navigation.Dtos;
using System;
using System.Collections.Generic;

namespace Soenneker.Blazor.Utils.Navigation.Abstract;

/// <summary>
/// A Blazor WebAssembly library that features navigate back, login/logout, reload and more <para/>
/// Register as Scoped.
/// </summary>
public interface INavigationUtil : IAsyncDisposable
{
    /// <summary>
    /// Navigates to the specified url.
    /// </summary>
    /// <param name="uri">The destination url (relative or absolute).</param>
    /// <param name="forceLoad"></param>
    void NavigateTo(string uri, bool forceLoad = false);

    /// <summary>
    /// Navigates to the specified url with query strings attached (in dictionary form)
    /// </summary>
    /// <param name="uri">The destination url (relative or absolute).</param>
    /// <param name="queryString"></param>
    /// <param name="forceLoad"></param>
    void NavigateTo(string uri, IDictionary<string, string> queryString, bool forceLoad = false);

    /// <summary>
    /// Returns true if it is possible to navigate to the previous UR.
    /// </summary>
    bool CanNavigateBack { get; }

    /// <summary>
    /// Reloads at the current URI.
    /// </summary>
    void Reload(bool forceLoad);

    /// <summary>
    /// Navigates to the previous url if possible or does nothing if it is not.
    /// </summary>
    void NavigateBack();

    /// <summary>
    /// Executes the login operation.
    /// </summary>
    /// <param name="loginPath">The login path.</param>
    /// <param name="loginOptions">The login options.</param>
    void Login(string loginPath = "authentication/login", MsalLoginOptions? loginOptions = null);

    /// <summary>
    /// Executes the login select account operation.
    /// </summary>
    /// <param name="loginPath">The login path.</param>
    /// <param name="returnUrl">The return url.</param>
    /// <param name="scopes">The scopes.</param>
    void LoginSelectAccount(string loginPath = "authentication/login", string? returnUrl = null, IEnumerable<string>? scopes = null);

    /// <summary>
    /// Executes the logout operation.
    /// </summary>
    /// <param name="logoutPath">The logout path.</param>
    /// <param name="returnUrl">The return url.</param>
    void Logout(string logoutPath = "authentication/logout", string? returnUrl = null);

    /// <summary>
    /// Gets current uri.
    /// </summary>
    /// <returns>The result of the operation.</returns>
    Uri GetCurrentUri();
}
