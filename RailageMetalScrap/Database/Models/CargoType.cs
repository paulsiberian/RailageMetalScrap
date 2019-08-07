using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RailageMetalScrap.Database.Models
{
    /// <summary>
    /// Класс объекта тип груза
    /// </summary>
    public class CargoType
    {
        /// <summary>
        /// ИД типа груза
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        /// <summary>
        /// Строка с типом груза
        /// </summary>
        [Column(TypeName = "varchar(128)")]
        public string Type { get; set; }

        /// <summary>
        /// Грузы с данным типом
        /// </summary>
        public HashSet<Cargo> Cargoes;

        public override bool Equals(object obj)
        {
            return Equals(obj as CargoType);
        }

        public bool Equals(CargoType other)
        {
            return other != null && Id.Equals(other.Id)
                                 && Type.Equals(other.Type);
        }

        public override int GetHashCode()
        {
            var hashCode = 352033288;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + Type.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return new StringBuilder()
                .Append(Type).ToString();
        }
    }
}