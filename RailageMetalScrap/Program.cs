using CommandLine;
using RailageMetalScrap.Commands;
using RailageMetalScrap.Tools;

namespace RailageMetalScrap
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = ConfigBuilder.Build("appsettings");

            Parser.Default.ParseArguments<Read, TreeMap>(args)
                .WithParsed<Read>(c => c.Run(config))
                .WithParsed<TreeMap>(c => { })
                .WithNotParsed(e => { });
        }
    }
}