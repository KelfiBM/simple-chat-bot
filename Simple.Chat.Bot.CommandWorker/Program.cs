using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Simple.Chat.Bot.CommandWorker.Services;

namespace Simple.Chat.Bot.CommandWorker
{
  public class Program
  {
    public static void Main(string[] args)
    {
      CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
              services.AddSingleton<IRabbitMQService, RabbitMQService>();
              services.AddHostedService<Worker>();
            });
  }
}
