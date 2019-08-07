using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Spreadsheet;

namespace RailageMetalScrap.Extensions
{
    /// <summary>
    /// Класс является расширением для класса <c>DocumentFormat.OpenXml.Spreadsheet.Cell</c>
    /// </summary>
    public static class XlsxExtensions
    {
        /// <summary>
        /// Метод возвращает значение ячейки в формате даты
        /// </summary>
        /// <param name="cell">Ячейка</param>
        /// <returns>Экземпляр даты</returns>
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

        /// <summary>
        /// Метод возвращает целочисленное значение ячейки в общем формате
        /// </summary>
        /// <param name="cell">Ячейка</param>
        /// <param name="stringTable">Таблица строковых значений листа</param>
        /// <returns>Целое число</returns>
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

        /// <summary>
        /// Метод возвращает значение ячейки в числовом формате
        /// </summary>
        /// <param name="cell">Ячейка</param>
        /// <returns>Число с плавующей точкой</returns>
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

        /// <summary>
        /// Метод возвращает значение ячейки в текстовом формате
        /// </summary>
        /// <param name="cell">Ячейка</param>
        /// <param name="stringTable">Таблица строковых значений листа</param>
        /// <returns>Текст ячейки</returns>
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

        /// <summary>
        /// Метод возвращает ячейку из строки по букве
        /// </summary>
        /// <param name="row">Строка</param>
        /// <param name="s">Буква</param>
        /// <returns>Ячейка</returns>
        public static Cell GetCellByLetter(this Row row, string s)
        {
            return row.Descendants<Cell>().FirstOrDefault(c => Regex.Replace(c.CellReference, @"[\d-]", String.Empty).Equals(s));
        }
    }
}