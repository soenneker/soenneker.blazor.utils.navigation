using System;
using AwesomeAssertions;
using Soenneker.Blazor.Utils.Navigation.Abstract;
using Soenneker.Blazor.Utils.Navigation.Dtos;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Blazor.Utils.Navigation.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public class NavigationUtilTests : HostedUnitTest
{
    private readonly INavigationUtil _util;

    public NavigationUtilTests(Host host) : base(host)
    {
        _util = Resolve<INavigationUtil>(true);
    }

    [Test]
    public void Login_rejects_external_return_url()
    {
        Action act = () => _util.Login(loginOptions: new MsalLoginOptions
        {
            ReturnUrl = "https://attacker.example/callback"
        });

        act.Should().Throw<ArgumentException>();
    }
}
