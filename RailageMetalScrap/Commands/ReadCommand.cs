using System;
using System.Collections.Generic;
using System.Linq;
using CommandLine;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RailageMetalScrap.Database;
using RailageMetalScrap.Spreadsheet;

namespace RailageMetalScrap.Commands
{
    [Verb("read")]
    public class Read
    {
        [Value(0, Default = null)]
        public IEnumerable<string> Values { get; set; }
        
        [Option('i', "insert", Default = false)]
        public bool Insert { get; set; }
        
        [Option('d', "display", Default = false)]
        public bool Display { get; set; }

        public void Run(IConfigurationRoot config)
        {
            var register = new XlsxRegister(config);
            register.Read(Values, Display);

            Console.WriteLine("Прочитано записей: " + register.Entries.Count);
            
            if (Insert)
            {
                var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
                var options = optionsBuilder
                    .UseSqlite(config.GetConnectionString("DefaultConnection"))
                    .Options;
                
                using (var db = new AppDbContext(options))
                {
                    foreach (var entry in register.Entries)
                    {
                        var cargo = new Cargo
                        {
                            Code = entry.Code,
                            DispatchDate = entry.Date,
                            Volume = entry.Volume,
                            Count = entry.Count,
                            CargoType = db.GetCargoType(entry.Type),
                            Sender = db.GetCompany(entry.Sender),
                            Recipient = db.GetCompany(entry.Recipient),
                            ShipmentStation = db.GetStation(
                                entry.ShipmentStation, entry.ShipmentRailRoad,
                                entry.ShipmentState, entry.ShipmentCountry),
                            DestinationStation = db.GetStation(
                                entry.DestinationStation, entry.DestinationRailRoad,
                                entry.DestinationState, entry.DestinationCountry)
                        };
                        
                        db.Cargoes.Add(cargo);
                        db.SaveChanges();

                        if (Display) Console.WriteLine(cargo);
                    }

                    Console.WriteLine("Записано записей: " + db.Cargoes.Count());
                }
            }
        }
    }
}