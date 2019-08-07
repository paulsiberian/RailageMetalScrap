using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
using NPOI.XSSF.UserModel;

namespace RailageMetalScrap.Spreadsheet
{
    /// <summary>
    /// Класс реализует интерфейс <see cref="IRegister"/> для работы с файлами реестров с расширением xlsx с использованием <see cref="NPOI"/>
    /// </summary>
    public class NpoiRegister : IRegister
    {
        private readonly IConfigurationRoot _config;
        
        private void SingleRead(string fileName)
        {
            var attr = File.GetAttributes(fileName);
            if (attr.HasFlag(FileAttributes.Directory))
            {
                var dir = new DirectoryInfo(fileName);
                var files = dir.GetFiles("*.xlsx");
                if (files.Equals(null))
                {
                    throw new Exception("Файлы xlsx не найдены");
                }
                else
                {
                    foreach (var file in files)
                    {
                        SingleRead(file.FullName);
                    }
                }
            }
            else
            {
                var file = new FileInfo(fileName);
                if (file.Extension.ToLower().Equals(".xlsx"))
                {
                    using (var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                    {
                        var workbook = new XSSFWorkbook(fileStream);
                        var sheet = workbook.GetSheet("1 Данные");

                        for (int i = 1; i < 30878; i++)
                        {
                            var row = sheet.GetRow(i);
                            if (row != null)
                            {
                                var a = row.GetCell(0).DateCellValue;
                                var b = int.Parse(row.GetCell(1).StringCellValue);
                                var c = row.GetCell(2).StringCellValue;
                                var h = row.GetCell(7).StringCellValue;
                                var m = row.GetCell(12).StringCellValue;
                                var n = row.GetCell(13).NumericCellValue;
                                var o = int.Parse(row.GetCell(14).StringCellValue);
                                var d = row.GetCell(3).StringCellValue;
                                var e = row.GetCell(4).StringCellValue;
                                var f = row.GetCell(5).StringCellValue;
                                var g = row.GetCell(6).StringCellValue;
                                var ii = row.GetCell(8).StringCellValue;
                                var j = row.GetCell(9).StringCellValue;
                                var k = row.GetCell(10).StringCellValue;
                                var l = row.GetCell(11).StringCellValue;
                                
                                var entry = new Entry
                                {
                                    Date = row.GetCell(0).DateCellValue,
                                    Code = int.Parse(row.GetCell(1).StringCellValue),
                                    Type = row.GetCell(2).StringCellValue,
                                    Sender = row.GetCell(7).StringCellValue,
                                    Recipient = row.GetCell(12).StringCellValue,
                                    Volume = row.GetCell(13).NumericCellValue,
                                    Count = int.Parse(row.GetCell(14).StringCellValue),
                                    ShipmentCountry = row.GetCell(3).StringCellValue,
                                    ShipmentState = row.GetCell(4).StringCellValue,
                                    ShipmentRailRoad = row.GetCell(5).StringCellValue,
                                    ShipmentStation = row.GetCell(6).StringCellValue,
                                    DestinationCountry = row.GetCell(8).StringCellValue,
                                    DestinationState = row.GetCell(9).StringCellValue,
                                    DestinationRailRoad = row.GetCell(10).StringCellValue,
                                    DestinationStation = row.GetCell(11).StringCellValue
                                };
                                Entries.Add(entry);
                            }
                        }
                    }
                }
            }
        }

        public NpoiRegister(IConfigurationRoot config)
        {
            Entries = new HashSet<Entry>();
            _config = config;
        }

        public void Read(IEnumerable<string> files, bool display)
        {
            if (files == null)
            {
                SingleRead(Directory.GetCurrentDirectory());
            }
            else
            {
                foreach (var file in files)
                {
                    SingleRead(file);
                }
            }
        }

        public HashSet<Entry> Entries { get; }
    }
}