using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Soenneker.Fixtures.Unit;
using Soenneker.Utils.Test;
using Soenneker.Blazor.Utils.Navigation.Registrars;
using Soenneker.Blazor.Mocks.NavigationManager.Registrars;

namespace Soenneker.Blazor.Utils.Navigation.Tests;

public class Fixture : UnitFixture
{
    public override System.Threading.Tasks.ValueTask InitializeAsync()
    {
        SetupIoC(Services);

        Services.AddMockNavigationManagerAsScoped();

        return base.InitializeAsync();
    }

    private static void SetupIoC(IServiceCollection services)
    {
        services.AddLogging(builder =>
        {
            builder.AddSerilog(dispose: true);
        });

        IConfiguration config = TestUtil.BuildConfig();
        services.AddSingleton(config);

        services.AddNavigationUtilAsScoped();
    }
}