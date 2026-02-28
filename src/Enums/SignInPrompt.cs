using Soenneker.Gen.EnumValues;

namespace Soenneker.Blazor.Utils.Navigation.Enums;

[EnumValue<string>]
public sealed partial class SignInPrompt
{
    /// <summary>
    /// Default interactive sign-in behavior.
    /// </summary>
    /// <remarks>
    /// No <c>prompt</c> parameter is sent. The identity provider decides how much UI to show.
    /// <para>
    /// Common outcomes:
    /// </para>
    /// <list type="bullet">
    ///   <item>
    ///     <description>If the browser already has a valid SSO session for a single account, the user may be signed in with minimal or no UI.</description>
    ///   </item>
    ///   <item>
    ///     <description>If multiple accounts are present or interaction is needed, the provider will show the appropriate UI (e.g., account chooser, login form).</description>
    ///   </item>
    /// </list>
    /// <para>
    /// Use this for the normal sign-in path when you don’t need to force account selection or re-authentication.
    /// </para>
    /// <example>
    /// In Blazor WASM:
    /// <code>
    /// var opts = new InteractiveRequestOptions { Interaction = InteractionType.SignIn };
    /// // No extra prompt parameter added.
    /// _navigationManager.NavigateToLogin("authentication/login", opts);
    /// </code>
    /// </example>
    /// </remarks>
    public static readonly SignInPrompt Default = new ("default");

    /// <summary>
    /// Always show the account picker.
    /// </summary>
    /// <remarks>
    /// Sends <c>prompt=select_account</c> to the identity provider, which instructs it to display an account
    /// chooser even if there is an existing SSO session.
    /// <para>Use this when you want the user to explicitly choose (or switch) accounts.</para>
    /// <example>
    /// In Blazor WASM:
    /// <code>
    /// var opts = new InteractiveRequestOptions { Interaction = InteractionType.SignIn };
    /// opts.TryAddAdditionalParameter("prompt", "select_account");
    /// _navigationManager.NavigateToLogin("authentication/login", opts);
    /// </code>
    /// </example>
    /// </remarks>
    public static readonly SignInPrompt SelectAccount = new ("select_account");

    /// <summary>
    /// Force re-authentication (fresh credentials/MFA).
    /// </summary>
    /// <remarks>
    /// Sends <c>prompt=login</c>, which requires the user to actively authenticate even if a valid SSO session exists.
    /// <para>
    /// Typical effect: the user is asked to re-enter credentials and/or complete MFA. This is useful before sensitive
    /// actions (e.g., changing security settings or payment details).
    /// </para>
    /// <example>
    /// <code>
    /// var opts = new InteractiveRequestOptions { Interaction = InteractionType.SignIn };
    /// opts.TryAddAdditionalParameter("prompt", "login");
    /// _navigationManager.NavigateToLogin("authentication/login", opts);
    /// </code>
    /// </example>
    /// </remarks>
    public static readonly SignInPrompt Login = new("login");

    /// <summary>
    /// Force the consent screen.
    /// </summary>
    /// <remarks>
    /// Sends <c>prompt=consent</c>, which asks the user to grant consent for the requested scopes even if consent
    /// was previously granted. Identity providers may ignore this if consent is not applicable.
    /// <para>Use this when you need the user to explicitly reconfirm permissions.</para>
    /// <example>
    /// <code>
    /// var opts = new InteractiveRequestOptions { Interaction = InteractionType.SignIn, Scopes = new[] { "api://app/read", "api://app/write" } };
    /// opts.TryAddAdditionalParameter("prompt", "consent");
    /// _navigationManager.NavigateToLogin("authentication/login", opts);
    /// </code>
    /// </example>
    /// </remarks>
    public static readonly SignInPrompt Consent = new ("consent");

    public static readonly SignInPrompt None = new ("none");
}
