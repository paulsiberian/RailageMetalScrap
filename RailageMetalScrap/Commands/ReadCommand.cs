using System;
using System.Collections.Generic;
using System.Linq;
using CommandLine;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RailageMetalScrap.Database;
using RailageMetalScrap.Database.Models;
using RailageMetalScrap.Spreadsheet;

namespace RailageMetalScrap.Commands
{
    /// <summary>
    /// Класс представляет собой экземпляр команды приложения считывающего записи из реестра перевозок
    /// </summary>
    [Verb("read")]
    public class Read
    {
        /// <summary>
        /// Пути до файлов и/или директорий с файлами
        /// </summary>
        /// <value>Перечисление строк с путими к файлам и/или директориям</value>
        [Value(0, Default = null)]
        public IEnumerable<string> Values { get; set; }
        
        /// <summary>
        /// Параметр команды, отвечающий за внесение записей в базу данных
        /// </summary>
        /// <value>Логическое значение</value>
        [Option('i', "insert", Default = false)]
        public bool Insert { get; set; }
        
        /// <summary>
        /// Параметр команды, отвечающий за вывод процесса чтения [и внесения в БД] записей реестра
        /// </summary>
        /// <value>Логическое значение</value>
        [Option('d', "display", Default = false)]
        public bool Display { get; set; }

        /// <summary>
        /// Метод запускает процесс работы команды
        /// </summary>
        /// <param name="config">Конфигурация приложения</param>
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