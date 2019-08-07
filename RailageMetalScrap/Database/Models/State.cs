using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RailageMetalScrap.Database.Models
{
    /// <summary>
    /// Класс объекта регион
    /// </summary>
    public class State
    {
        /// <summary>
        /// ИД региона
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        /// <summary>
        /// Название региона
        /// </summary>
        [Column(TypeName = "varchar(128)")]
        public string Name { get; set; }
        
        /// <summary>
        /// ИД страны
        /// </summary>
        public int CountryId { get; set; }
        
        /// <summary>
        /// Страна
        /// </summary>
        public Country Country { get; set; }
        
        /// <summary>
        /// Станции расположенные в данном регионе
        /// </summary>
        public HashSet<Station> Stations { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as State);
        }

        public bool Equals(State other)
        {
            return other != null && Id.Equals(other.Id)
                                 && Name.Equals(other.Name)
                                 && Country.Equals(other.Country);
        }

        public override int GetHashCode()
        {
            var hashCode = 352033288;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + Name.GetHashCode();
            hashCode = hashCode * -1521134295 + Country.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return new StringBuilder()
                .Append(Name).ToString();
        }
    }
}