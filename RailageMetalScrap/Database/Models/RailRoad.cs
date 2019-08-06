using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RailageMetalScrap.Database
{
    public class RailRoad
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column(TypeName = "varchar(128)")]
        public string Name { get; set; }
        
        public int CountryId { get; set; }
        public Country Country { get; set; }
        
        public HashSet<Station> Stations { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as RailRoad);
        }

        public bool Equals(RailRoad other)
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