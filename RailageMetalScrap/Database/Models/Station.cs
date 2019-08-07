using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RailageMetalScrap.Database.Models
{
    /// <summary>
    /// Класс объекта станция
    /// </summary>
    public class Station
    {
        /// <summary>
        /// ИД станции
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        /// <summary>
        /// Название станции
        /// </summary>
        [Column(TypeName = "varchar(128)")]
        public string Name { get; set; }
        
        /// <summary>
        /// ИД региона
        /// </summary>
        public int StateId { get; set; }
        
        /// <summary>
        /// Регион
        /// </summary>
        public State State { get; set; }
        
        /// <summary>
        /// ИД железной дороги
        /// </summary>
        public int RailRoadId { get; set; }
        
        /// <summary>
        /// Железная дорога
        /// </summary>
        public RailRoad RailRoad { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Station);
        }

        public bool Equals(Station other)
        {
            return other != null && Id.Equals(other.Id)
                                 && Name.Equals(other.Name)
                                 && State.Equals(other.State)
                                 && RailRoad.Equals(other.RailRoad);
        }

        public override int GetHashCode()
        {
            var hashCode = 352033288;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + Name.GetHashCode();
            hashCode = hashCode * -1521134295 + State.GetHashCode();
            hashCode = hashCode * -1521134295 + RailRoad.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return new StringBuilder()
                .Append(Name).ToString();
        }
    }
}