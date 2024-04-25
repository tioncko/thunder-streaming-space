using Microsoft.Extensions.Configuration;

namespace thunder_streaming_space.Settings
{
    internal class Configuration
    {
        //Classe que retorna os parametros solicitados dentro do appsettings.json
        private readonly IConfiguration _configuration;

        public Configuration(IConfiguration configuration) => _configuration = configuration;
        public IConfiguration GetConfiguration() => _configuration;
    }
}
