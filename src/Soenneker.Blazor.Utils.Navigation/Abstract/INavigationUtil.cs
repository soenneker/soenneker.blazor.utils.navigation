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
    /// Returns true when at least two observed locations are available in this utility's history.
    /// </summary>
    bool CanNavigateBack { get; }

    /// <summary>
    /// Reloads at the current URI.
    /// </summary>
    /// <param name="forceLoad">Whether force load.</param>
    void Reload(bool forceLoad);

    /// <summary>
    /// Navigates to the previously observed location if possible, or does nothing when no prior location is tracked.
    /// </summary>
    void NavigateBack();

    /// <summary>
    /// Logs in navigation.
    /// </summary>
    /// <param name="loginPath">Path of the login to use.</param>
    /// <param name="loginOptions">Login Options for the login operation.</param>
    void Login(string loginPath = "authentication/login", MsalLoginOptions? loginOptions = null);

    /// <summary>
    /// Logs in select Account.
    /// </summary>
    /// <param name="loginPath">Path of the login to use.</param>
    /// <param name="returnUrl">Optional return URL within the application base URI.</param>
    /// <param name="scopes">scopes to process.</param>
    void LoginSelectAccount(string loginPath = "authentication/login", string? returnUrl = null, IEnumerable<string>? scopes = null);

    /// <summary>
    /// Logs out navigation.
    /// </summary>
    /// <param name="logoutPath">Path of the logout to use.</param>
    /// <param name="returnUrl">Optional return URL within the application base URI.</param>
    void Logout(string logoutPath = "authentication/logout", string? returnUrl = null);

    /// <summary>
    /// Gets current uri.
    /// </summary>
    /// <returns>The requested URI.</returns>
    Uri GetCurrentUri();
}
