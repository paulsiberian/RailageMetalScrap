using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RailageMetalScrap.Database.Models
{
    /// <summary>
    /// Класс объекта компания
    /// </summary>
    public class Company
    {
        /// <summary>
        /// ИД компании
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        /// <summary>
        /// Название компании
        /// </summary>
        [Column(TypeName = "varchar(128)")]
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Company);
        }

        public bool Equals(Company other)
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