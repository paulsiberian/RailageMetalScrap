using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RailageMetalScrap.Database
{
    public class Cargo
    {
        [Key]
        public int Code { get; set; }
        public double Volume { get; set; }
        public int Count { get; set; }
        public DateTime DispatchDate { get; set; }
        
        public int CargoTypeId { get; set; }
        public CargoType CargoType { get; set; }
        
        public int SenderId { get; set; }
        public Company Sender { get; set; }
        
        public int RecipientId { get; set; }
        public Company Recipient { get; set; }
        
        public int ShipmentStationId { get; set; }
        public Station ShipmentStation { get; set; }
        
        public int DestinationStationId { get; set; }
        public Station DestinationStation { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Cargo);
        }

        public bool Equals(Cargo other)
        {
            return other != null && Code.Equals(other.Code)
                                 && Volume.Equals(other.Volume)
                                 && Count.Equals(other.Count)
                                 && DispatchDate.Equals(other.DispatchDate)
                                 && CargoType.Equals(other.CargoType)
                                 && Sender.Equals(other.Sender)
                                 && Recipient.Equals(other.Recipient)
                                 && ShipmentStation.Equals(other.ShipmentStation)
                                 && DestinationStation.Equals(other.DestinationStation);
        }

        public override int GetHashCode()
        {
            var hashCode = 352033288;
            hashCode = hashCode * -1521134295 + Code.GetHashCode();
            hashCode = hashCode * -1521134295 + Volume.GetHashCode();
            hashCode = hashCode * -1521134295 + Count.GetHashCode();
            hashCode = hashCode * -1521134295 + DispatchDate.GetHashCode();
            hashCode = hashCode * -1521134295 + CargoType.GetHashCode();
            hashCode = hashCode * -1521134295 + Sender.GetHashCode();
            hashCode = hashCode * -1521134295 + Recipient.GetHashCode();
            hashCode = hashCode * -1521134295 + ShipmentStation.GetHashCode();
            hashCode = hashCode * -1521134295 + DestinationStation.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return new StringBuilder()
                .Append(DispatchDate.ToString("d")).Append('\t')
                .Append(Code).Append('\t')
                .Append(CargoType).Append('\t')
                .Append(ShipmentStation.State.Country).Append('\t')
                .Append(ShipmentStation.State).Append('\t')
                .Append(ShipmentStation.RailRoad).Append('\t')
                .Append(ShipmentStation).Append('\t')
                .Append(Sender).Append('\t')
                .Append(DestinationStation.State.Country).Append('\t')
                .Append(DestinationStation.State).Append('\t')
                .Append(DestinationStation.RailRoad).Append('\t')
                .Append(DestinationStation).Append('\t')
                .Append(Recipient).Append('\t')
                .Append(Volume).Append('\t')
                .Append(Count).ToString();
        }
    }
}