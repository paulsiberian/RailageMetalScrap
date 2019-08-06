using System.IO;
using Microsoft.Extensions.Configuration;

namespace RailageMetalScrap.Tools
{
    public static class ConfigBuilder
    {
        public static IConfigurationRoot Build(string json)
        {
            var configBuilder = new ConfigurationBuilder();
            configBuilder.SetBasePath(Directory.GetCurrentDirectory());
            configBuilder.AddJsonFile(json + ".json");
            return configBuilder.Build();
        }
    }
}