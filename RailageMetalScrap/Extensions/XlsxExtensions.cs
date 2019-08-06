using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Spreadsheet;

namespace RailageMetalScrap.Extensions
{
    public static class XlsxExtensions
    {
        public static DateTime GetDateTimeValue(this Cell cell)
        {
            var value = DateTime.Today;
            if (cell != null)
            {
                if (cell.CellValue.Text != null)
                {
                    value = DateTime.FromOADate(cell.GetDoubleValue());
                }
            }
            return value;
        }

        public static int GetIntValue(this Cell cell, SharedStringTable stringTable)
        {
            var value = 0;
            if (cell != null)
            {
                if (cell.CellValue.Text != null)
                {
                    value = int.Parse(cell.GetStringValue(stringTable));
                }
            }
            return value;
        }

        public static double GetDoubleValue(this Cell cell)
        {
            var value = 0.0;
            if (cell != null)
            {
                if (cell.CellValue.Text != null)
                {
                    value = double.Parse(cell.CellValue.Text, CultureInfo.InvariantCulture);
                }
            }
            return value;
        }

        public static string GetStringValue(this Cell cell, SharedStringTable stringTable)
        {
            var value = "";
            if (cell != null)
            {
                value = cell.CellValue.Text;
                if (value != null)
                {
                    var dataType = cell.DataType;
                    if (dataType != null)
                    {
                        if (dataType.Value == CellValues.SharedString)
                        {
                            value = stringTable.ElementAt(int.Parse(value)).InnerText;
                        }
                    }
                }
            }
            return value;
        }

        public static Cell GetCellByLetter(this Row row, string s)
        {
            return row.Descendants<Cell>().FirstOrDefault(c => Regex.Replace(c.CellReference, @"[\d-]", String.Empty).Equals(s));
        }
    }
}