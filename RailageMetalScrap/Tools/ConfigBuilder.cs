using System.IO;
using Microsoft.Extensions.Configuration;

namespace RailageMetalScrap.Tools
{
    /// <summary>
    /// Класс содержит статичный метод для получения корня конфигурационного файла приложения
    /// </summary>
    public static class ConfigBuilder
    {
        /// <summary>
        /// Метод позволяет получить корень конфигурационного файла JSON приложения
        /// </summary>
        /// <param name="json">Имя конфигурационного файла JSON без расширения</param>
        /// <returns>Корень конфигурационного файла</returns>
        /// <example>
        /// <code>
        /// var config = ConfigBuilder.Build("appsettings");
        /// </code>
        /// </example>
        public static IConfigurationRoot Build(string json)
        {
            var configBuilder = new ConfigurationBuilder();
            configBuilder.SetBasePath(Directory.GetCurrentDirectory());
            configBuilder.AddJsonFile(json + ".json");
            return configBuilder.Build();
        }
    }
}