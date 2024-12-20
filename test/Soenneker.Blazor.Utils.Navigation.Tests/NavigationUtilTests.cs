using Soenneker.Blazor.Utils.Navigation.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;


namespace Soenneker.Blazor.Utils.Navigation.Tests;

[Collection("Collection")]
public class NavigationUtilTests : FixturedUnitTest
{
    private readonly INavigationUtil _util;

    public NavigationUtilTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _util = Resolve<INavigationUtil>(true);
    }

    [Fact]
    public void Default()
    {

    }
}
