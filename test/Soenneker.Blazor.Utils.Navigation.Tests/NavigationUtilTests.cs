using Soenneker.Blazor.Utils.Navigation.Abstract;
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
    public void Default()
    {

    }
}
