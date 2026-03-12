using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.WebUtilities;
using Soenneker.Blazor.Utils.Navigation.Abstract;
using Soenneker.Blazor.Utils.Navigation.Dtos;
using Soenneker.Blazor.Utils.Navigation.Enums;
using Soenneker.Extensions.String;

namespace Soenneker.Blazor.Utils.Navigation;

///<inheritdoc cref="INavigationUtil"/>
public sealed class NavigationUtil : INavigationUtil
{
    private const int _minHistorySize = 256;
    private const int _additionalHistorySize = 64;
    private readonly NavigationManager _navigationManager;
    private readonly List<string> _history;

    public NavigationUtil(NavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
        _history = new List<string>(_minHistorySize + _additionalHistorySize)
        {
            _navigationManager.Uri
        };

        _navigationManager.LocationChanged += OnLocationChanged;
    }

    public void NavigateTo(string uri, bool forceLoad = false)
    {
        _navigationManager.NavigateTo(uri, forceLoad);
    }

    public void NavigateTo(string uri, IDictionary<string, string> queryString, bool forceLoad = false)
    {
        _navigationManager.NavigateTo(QueryHelpers.AddQueryString(uri, queryString), forceLoad);
    }

    public bool CanNavigateBack => _history.Count >= 2;

    public void NavigateBack()
    {
        if (!CanNavigateBack)
            return;

        string backPageUrl = _history[^2];

        _history.RemoveRange(_history.Count - 2, 2);
        _navigationManager.NavigateTo(backPageUrl);
    }

    public void Login(string loginPath = "authentication/login", MsalLoginOptions? loginOptions = null)
    {
        var opts = new InteractiveRequestOptions
        {
            Interaction = InteractionType.SignIn,
            ReturnUrl = loginOptions?.ReturnUrl,
            Scopes = loginOptions?.Scopes
        };

        string? prompt = null;

        if (loginOptions?.Prompt != null && loginOptions.Prompt != SignInPrompt.Default)
        {
            prompt = loginOptions.Prompt.Value;
        }

        if (prompt.HasContent())
        {
            // Add prompt first so ExtraParameters can't override it accidentally.
            opts.TryAddAdditionalParameter("prompt", prompt);
        }

        // Add any extra params (domain_hint, login_hint, etc.)
        if (loginOptions?.ExtraParameters is not null)
        {
            foreach (KeyValuePair<string, string> kv in loginOptions.ExtraParameters)
            {
                // Respect previously-added keys; TryAdd will no-op on duplicates.
                opts.TryAddAdditionalParameter(kv.Key, kv.Value);
            }
        }

        _navigationManager.NavigateToLogin(loginPath, opts);
    }

    public void LoginSelectAccount(string loginPath = "authentication/login", string? returnUrl = null, IEnumerable<string>? scopes = null)
    {
        var options = new MsalLoginOptions
        {
            ReturnUrl = returnUrl,
            Scopes = scopes,
            Prompt = SignInPrompt.SelectAccount
        };

        Login(loginPath, options);
    }

    public void Logout(string logoutPath = "authentication/logout", string? returnUrl = null)
    {
        _navigationManager.NavigateToLogout(logoutPath, returnUrl);
    }

    private void OnLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        EnsureSize();
        _history.Add(e.Location);
    }

    public void Reload(bool forceLoad)
    {
        _navigationManager.NavigateTo(_navigationManager.Uri, forceLoad);
    }

    private void EnsureSize()
    {
        if (_history.Count < _minHistorySize + _additionalHistorySize)
            return;

        _history.RemoveRange(0, _history.Count - _minHistorySize);
    }

    public Uri GetCurrentUri()
    {
        Uri result = _navigationManager.ToAbsoluteUri(_navigationManager.Uri);
        return result;
    }

    public ValueTask DisposeAsync()
    {
        _navigationManager.LocationChanged -= OnLocationChanged;

        return ValueTask.CompletedTask;
    }
}