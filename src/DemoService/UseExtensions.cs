using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DemoService
{
    public static class UseExtensions
    {
        public static IHostBuilder UsePrinter(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices(services =>
                     services.AddHostedService<PrinterHostedService>());
        }
    }
}