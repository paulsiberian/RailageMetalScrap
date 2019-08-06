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
    public class XlsxRegister : IRegister
    {

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
                                    Date = row.GetCellByLetter(Config.GetRegisterColumnLetter("Date")).GetDateTimeValue(),
                                    Code = row.GetCellByLetter(Config.GetRegisterColumnLetter("Code")).GetIntValue(stringTablePart.SharedStringTable),
                                    Type = row.GetCellByLetter(Config.GetRegisterColumnLetter("Type")).GetStringValue(stringTablePart.SharedStringTable),
                                    Sender = row.GetCellByLetter(Config.GetRegisterColumnLetter("Sender")).GetStringValue(stringTablePart.SharedStringTable),
                                    Recipient = row.GetCellByLetter(Config.GetRegisterColumnLetter("Recipient")).GetStringValue(stringTablePart.SharedStringTable),
                                    Volume = row.GetCellByLetter(Config.GetRegisterColumnLetter("Volume")).GetDoubleValue(),
                                    Count = row.GetCellByLetter(Config.GetRegisterColumnLetter("Count")).GetIntValue(stringTablePart.SharedStringTable),
                                    ShipmentCountry = row.GetCellByLetter(Config.GetRegisterColumnLetter("ShipmentCountry")).GetStringValue(stringTablePart.SharedStringTable),
                                    ShipmentState = row.GetCellByLetter(Config.GetRegisterColumnLetter("ShipmentState")).GetStringValue(stringTablePart.SharedStringTable),
                                    ShipmentRailRoad = row.GetCellByLetter(Config.GetRegisterColumnLetter("ShipmentRailRoad")).GetStringValue(stringTablePart.SharedStringTable),
                                    ShipmentStation = row.GetCellByLetter(Config.GetRegisterColumnLetter("ShipmentStation")).GetStringValue(stringTablePart.SharedStringTable),
                                    DestinationCountry = row.GetCellByLetter(Config.GetRegisterColumnLetter("DestinationCountry")).GetStringValue(stringTablePart.SharedStringTable),
                                    DestinationState = row.GetCellByLetter(Config.GetRegisterColumnLetter("DestinationState")).GetStringValue(stringTablePart.SharedStringTable),
                                    DestinationRailRoad = row.GetCellByLetter(Config.GetRegisterColumnLetter("DestinationRailRoad")).GetStringValue(stringTablePart.SharedStringTable),
                                    DestinationStation = row.GetCellByLetter(Config.GetRegisterColumnLetter("DestinationStation")).GetStringValue(stringTablePart.SharedStringTable)
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
            }
        }

        public XlsxRegister(IConfigurationRoot config)
        {
            Entries = new HashSet<Entry>();
            Config = config;
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
        public IConfigurationRoot Config { get; }
    }
}