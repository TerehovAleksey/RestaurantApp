using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace RestaurantApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHost(args);
            host.Run();
        }

        public static IWebHost CreateWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>().Build();
    }
}
