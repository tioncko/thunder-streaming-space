using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace thunder_streaming_space.Settings
{
    internal class Parameters
    {
        //Classe que retorna a configuração do appsettings.json
        public static IConfiguration Build()
        {
            IHost host = CreateHostBuilder().Build();
            Configuration worker = ActivatorUtilities.CreateInstance<Configuration>(host.Services);
            return worker.GetConfiguration();
        }

        private static IHostBuilder CreateHostBuilder() //or private static IHostBuilder CreateHostBuilder() => 
        {
            return Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((context, configuration) =>
                {
                    configuration.Sources.Clear();
                    configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                });
        }
    }
}
