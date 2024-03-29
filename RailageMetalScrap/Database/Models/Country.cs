using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RailageMetalScrap.Database.Models
{
    /// <summary>
    /// Класс объекта страна
    /// </summary>
    public class Country
    {
        /// <summary>
        /// ИД страны
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        /// <summary>
        /// Название страны
        /// </summary>
        [Column(TypeName = "varchar(128)")]
        public string Name { get; set; }
        
        /// <summary>
        /// Регионы находящиеся в данной стране
        /// </summary>
        public HashSet<State> States { get; set; }
        
        /// <summary>
        /// Железные дороги проходящие по данной стране
        /// </summary>
        public HashSet<RailRoad> RailRoads { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Country);
        }

        public bool Equals(Country other)
        {
            return other != null && Id.Equals(other.Id)
                                 && Name.Equals(other.Name);
        }

        public override int GetHashCode()
        {
            var hashCode = 352033288;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + Name.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return new StringBuilder()
                .Append(Name).ToString();
        }
    }
}