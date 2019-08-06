using Microsoft.Extensions.Configuration;

namespace RailageMetalScrap.Extensions
{
    public static class ConfigExtensions
    {
        public static string GetRegisterColumnLetter(this IConfiguration configuration, string name)
        {
            return configuration?.GetSection("RegisterColumnLetters")?[name];
        }
    }
}