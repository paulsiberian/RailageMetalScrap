using System.Collections.Generic;
using CommandLine;

namespace RailageMetalScrap.Commands
{
    [Verb("treemap")]
    public class TreeMap
    {
        [Value(0, Min = 1, Max = 2)]
        public IEnumerable<string> Values { get; set; }
    }
}