using System;

namespace ConsoleHost
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new HostBuilder()
                //logging
                .ConfigureLogging(factory =>
                {
                    //use nlog
                    factory.AddNLog(new NLogProviderOptions { CaptureMessageTemplates = true, CaptureMessageProperties = true });
                    NLog.LogManager.LoadConfiguration("nlog.config");
                })
                //host config
                .ConfigureHostConfiguration(config =>
                {
                    //command line
                    if (args != null)
                    {
                        config.AddCommandLine(args);
                    }
                })
                //app config
                .ConfigureAppConfiguration((hostContext, config) =>
                {
                    var env = hostContext.HostingEnvironment;
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

                    config.AddEnvironmentVariables();

                    if (args != null)
                    {
                        config.AddCommandLine(args);
                    }
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddOptions();
                    services.Configure<AppSettings>(hostContext.Configuration.GetSection("AppSettings"));
                })
                //here use extensions
                .UsePrinter();

            //console 
            await builder.RunConsoleAsync();
        }
    }
}
