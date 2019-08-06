using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RailageMetalScrap.Database
{
    public class Station
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column(TypeName = "varchar(128)")]
        public string Name { get; set; }
        
        public int StateId { get; set; }
        public State State { get; set; }
        
        public int RailRoadId { get; set; }
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