using System;
using System.Text;
using RailageMetalScrap.Database;

namespace RailageMetalScrap.Spreadsheet
{
    public class Entry
    {
        public DateTime Date { get; set; }
        public int Code { get; set; }
        public string Type { get; set; }
        public string Sender { get; set; }
        public string Recipient { get; set; }
        public double Volume { get; set; }
        public int Count { get; set; }
        public string ShipmentCountry { get; set; }
        public string ShipmentState { get; set; }
        public string ShipmentRailRoad { get; set; }
        public string ShipmentStation { get; set; }
        public string DestinationCountry { get; set; }
        public string DestinationState { get; set; }
        public string DestinationRailRoad { get; set; }
        public string DestinationStation { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Entry);
        }

        public bool Equals(Entry other)
        {
            return other != null && Code.Equals(other.Code)
                                 && Volume.Equals(other.Volume)
                                 && Count.Equals(other.Count)
                                 && Date.Equals(other.Date)
                                 && Type.Equals(other.Type)
                                 && Sender.Equals(other.Sender)
                                 && Recipient.Equals(other.Recipient)
                                 && ShipmentCountry.Equals(other.ShipmentCountry)
                                 && ShipmentState.Equals(other.ShipmentState)
                                 && ShipmentRailRoad.Equals(other.ShipmentRailRoad)
                                 && ShipmentStation.Equals(other.ShipmentStation)
                                 && DestinationCountry.Equals(other.DestinationCountry)
                                 && DestinationState.Equals(other.DestinationState)
                                 && DestinationRailRoad.Equals(other.DestinationRailRoad)
                                 && DestinationStation.Equals(other.DestinationStation);
        }

        public override int GetHashCode()
        {
            var hashCode = 352033288;
            hashCode = hashCode * -1521134295 + Code.GetHashCode();
            hashCode = hashCode * -1521134295 + Volume.GetHashCode();
            hashCode = hashCode * -1521134295 + Count.GetHashCode();
            hashCode = hashCode * -1521134295 + Date.GetHashCode();
            hashCode = hashCode * -1521134295 + Type.GetHashCode();
            hashCode = hashCode * -1521134295 + Sender.GetHashCode();
            hashCode = hashCode * -1521134295 + Recipient.GetHashCode();
            hashCode = hashCode * -1521134295 + ShipmentCountry.GetHashCode();
            hashCode = hashCode * -1521134295 + ShipmentState.GetHashCode();
            hashCode = hashCode * -1521134295 + ShipmentRailRoad.GetHashCode();
            hashCode = hashCode * -1521134295 + ShipmentStation.GetHashCode();
            hashCode = hashCode * -1521134295 + DestinationCountry.GetHashCode();
            hashCode = hashCode * -1521134295 + DestinationState.GetHashCode();
            hashCode = hashCode * -1521134295 + DestinationRailRoad.GetHashCode();
            hashCode = hashCode * -1521134295 + DestinationStation.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return new StringBuilder()
                .Append(Date.ToString("d")).Append('\t')
                .Append(Code).Append('\t')
                .Append(Type).Append('\t')
                .Append(ShipmentCountry).Append('\t')
                .Append(ShipmentState).Append('\t')
                .Append(ShipmentRailRoad).Append('\t')
                .Append(ShipmentStation).Append('\t')
                .Append(Sender).Append('\t')
                .Append(DestinationCountry).Append('\t')
                .Append(DestinationState).Append('\t')
                .Append(DestinationRailRoad).Append('\t')
                .Append(DestinationStation).Append('\t')
                .Append(Recipient).Append('\t')
                .Append(Volume).Append('\t')
                .Append(Count).ToString();
        }
    }
}