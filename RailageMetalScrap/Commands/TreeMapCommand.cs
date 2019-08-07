using System.Collections.Generic;
using CommandLine;

namespace RailageMetalScrap.Commands
{
    /// <summary>
    /// Класс представляет собой экземпляр команды приложения, создающей иерархическую диаграмму по выбранным данным из базы данных
    /// </summary>
    [Verb("treemap")]
    public class TreeMap
    {
        [Value(0, Min = 1, Max = 2)]
        public IEnumerable<string> Values { get; set; }
    }
}