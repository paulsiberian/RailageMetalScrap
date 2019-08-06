using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace RailageMetalScrap.Spreadsheet
{
    public interface IRegister
    {
        void Read(IEnumerable<string> files = null, bool display = false);
        HashSet<Entry> Entries { get; }
        IConfigurationRoot Config { get; }
    }
}