using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FileWatcherDemo
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args)
                        .ConfigureServices((hostContext, services) =>
                        {
                            IConfiguration configuration = hostContext.Configuration;
                            DirectoryConifg options = configuration.GetSection("DirectoryConifg").Get<DirectoryConifg>();
                            services.AddSingleton(options);
                            services.AddHostedService<Worker>();
                        })
                        .UseWindowsService()
                        .Build();
            host.Run();
        }
    }
}