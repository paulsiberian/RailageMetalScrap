using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace RailageMetalScrap.Spreadsheet
{
    /// <summary>
    /// Интерфейс реестров записей
    /// </summary>
    public interface IRegister
    {
        /// <summary>
        /// Записи реестра
        /// </summary>
        /// <value>Неупорядоченная коллекция записей</value>
        HashSet<Entry> Entries { get; }
        
        /// <summary>
        /// Метод считывает записи из таблиц одного или несколький реестров
        /// </summary>
        /// <param name="files">Строковые пути до файлов реестров</param>
        /// <param name="display">Вывод данных</param>
        void Read(IEnumerable<string> files = null, bool display = false);
    }
}