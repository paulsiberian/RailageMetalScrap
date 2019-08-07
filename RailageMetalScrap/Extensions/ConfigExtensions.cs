using Microsoft.Extensions.Configuration;

namespace RailageMetalScrap.Extensions
{
    /// <summary>
    /// Класс является расширением для интерфейса <c>Microsoft.Extensions.Configuration.IConfiguration</c>
    /// </summary>
    public static class ConfigExtensions
    {
        /// <summary>
        /// Метод быстрого доступа к <c>GetSection("RegisterColumnLetters")[name]</c>
        /// </summary>
        /// <param name="configuration">Интерфейс, для которого написано расширение</param>
        /// <param name="name">Имя параметра</param>
        /// <returns>Значение параметра</returns>
        public static string GetRegisterColumnLetter(this IConfiguration configuration, string name)
        {
            return configuration?.GetSection("RegisterColumnLetters")?[name];
        }
    }
}