using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Extensions.Configuration;
using RailageMetalScrap.Extensions;

namespace RailageMetalScrap.Spreadsheet
{
    /// <summary>
    /// Класс реализует интерфейс <see cref="IRegister"/> для работы с файлами реестров с расширением xlsx с использованием <see cref="DocumentFormat.OpenXml"/>
    /// </summary>
    public class XlsxRegister : IRegister
    {
        /// <summary>
        /// Конфигурация приложения
        /// </summary>
        private readonly IConfigurationRoot _config;

        /// <summary>
        /// Приватный метод считывает записи таблицы одного реестра
        /// </summary>
        /// <param name="fileName">Строковый путь до файла реестра</param>
        /// <param name="display">Вывод данных</param>
        /// <exception cref="Exception">Исключение возникает, если файл имеет расширение отличное от xlsx, или директория не содержит файлов с расширением xlsx.</exception>
        private void SingleRead(string fileName, bool display)
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
                foreach (var file in files) SingleRead(file.FullName, display);
            }
            else
            {
                var file = new FileInfo(fileName);
                if (file.Extension.ToLower().Equals(".xlsx"))
                {
                    using (var document = SpreadsheetDocument.Open(fileName, false))
                    {
                        var workbookPart = document.WorkbookPart;
                        var sheet = workbookPart.Workbook.Descendants<Sheet>().FirstOrDefault(sh => sh.Name.Value.Contains("Данные"));
                        var worksheetPart = (WorksheetPart) workbookPart.GetPartById(sheet.Id);
                        var data = worksheetPart.Worksheet.Descendants<SheetData>().FirstOrDefault();
                        var rows = data.Descendants<Row>();
                        var stringTablePart = workbookPart.GetPartsOfType<SharedStringTablePart>().FirstOrDefault();
                        foreach (var row in rows)
                        {
                            if (row.RowIndex != 1)
                            {
                                var entry = new Entry
                                {
                                    Date = row.GetCellByLetter(_config.GetRegisterColumnLetter("Date")).GetDateTimeValue(),
                                    Code = row.GetCellByLetter(_config.GetRegisterColumnLetter("Code")).GetIntValue(stringTablePart.SharedStringTable),
                                    Type = row.GetCellByLetter(_config.GetRegisterColumnLetter("Type")).GetStringValue(stringTablePart.SharedStringTable),
                                    Sender = row.GetCellByLetter(_config.GetRegisterColumnLetter("Sender")).GetStringValue(stringTablePart.SharedStringTable),
                                    Recipient = row.GetCellByLetter(_config.GetRegisterColumnLetter("Recipient")).GetStringValue(stringTablePart.SharedStringTable),
                                    Volume = row.GetCellByLetter(_config.GetRegisterColumnLetter("Volume")).GetDoubleValue(),
                                    Count = row.GetCellByLetter(_config.GetRegisterColumnLetter("Count")).GetIntValue(stringTablePart.SharedStringTable),
                                    ShipmentCountry = row.GetCellByLetter(_config.GetRegisterColumnLetter("ShipmentCountry")).GetStringValue(stringTablePart.SharedStringTable),
                                    ShipmentState = row.GetCellByLetter(_config.GetRegisterColumnLetter("ShipmentState")).GetStringValue(stringTablePart.SharedStringTable),
                                    ShipmentRailRoad = row.GetCellByLetter(_config.GetRegisterColumnLetter("ShipmentRailRoad")).GetStringValue(stringTablePart.SharedStringTable),
                                    ShipmentStation = row.GetCellByLetter(_config.GetRegisterColumnLetter("ShipmentStation")).GetStringValue(stringTablePart.SharedStringTable),
                                    DestinationCountry = row.GetCellByLetter(_config.GetRegisterColumnLetter("DestinationCountry")).GetStringValue(stringTablePart.SharedStringTable),
                                    DestinationState = row.GetCellByLetter(_config.GetRegisterColumnLetter("DestinationState")).GetStringValue(stringTablePart.SharedStringTable),
                                    DestinationRailRoad = row.GetCellByLetter(_config.GetRegisterColumnLetter("DestinationRailRoad")).GetStringValue(stringTablePart.SharedStringTable),
                                    DestinationStation = row.GetCellByLetter(_config.GetRegisterColumnLetter("DestinationStation")).GetStringValue(stringTablePart.SharedStringTable)
                                };

                                if (!entry.Count.Equals(0))
                                {
                                    Entries.Add(entry);
                                }
                                else
                                {
                                    break;
                                }

                                if (display) Console.WriteLine(entry);
                            }
                        }
                    }
                }
                else
                {
                    throw new Exception("Файл имеет расширение отличное от xlsx");
                }
            }
        }

        /// <summary>
        /// Конструктор создаёт экземпляр класса
        /// </summary>
        /// <param name="config">Конфигурация приложения</param>
        public XlsxRegister(IConfigurationRoot config)
        {
            Entries = new HashSet<Entry>();
            _config = config;
        }

        public void Read(IEnumerable<string> files, bool display)
        {
            if (files == null)
            {
                SingleRead(Directory.GetCurrentDirectory(), display);
            }
            else
            {
                foreach (var file in files) SingleRead(file, display);
            }
        }

        public HashSet<Entry> Entries { get; }
    }
}