using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RailageMetalScrap.Database.Models
{
    /// <summary>
    /// Класс объекта груз
    /// </summary>
    public class Cargo
    {
        /// <summary>
        /// Код груза
        /// </summary>
        /// <value>Целочисленное значение кода</value>
        [Key]
        public int Code { get; set; }
        
        /// <summary>
        /// Объём
        /// </summary>
        /// <value>Число с плавующей точкой</value>
        public double Volume { get; set; }
        
        /// <summary>
        /// Количество
        /// </summary>
        /// <value>Целочисленное значение</value>
        public int Count { get; set; }
        
        /// <summary>
        /// Дата отправления
        /// </summary>
        /// <value>Экземпляр даты отправления</value>
        public DateTime DispatchDate { get; set; }
        
        /// <summary>
        /// ИД типа груза
        /// </summary>
        /// <value>Целое число</value>
        public int CargoTypeId { get; set; }
        
        /// <summary>
        /// Тип груза
        /// </summary>
        /// <value>Экземпаляр класса <see cref="CargoType"/></value>
        public CargoType CargoType { get; set; }
        
        /// <summary>
        /// ИД отправителя (компании)
        /// </summary>
        /// <value>Целое число</value>
        public int SenderId { get; set; }
        
        /// <summary>
        /// Отправитель
        /// </summary>
        /// <value>Экземпляр класса <see cref="Company"/></value>
        public Company Sender { get; set; }
        
        /// <summary>
        /// ИД получалеля (компании)
        /// </summary>
        /// <value>Целое число</value>
        public int RecipientId { get; set; }
        
        /// <summary>
        /// Получатель
        /// </summary>
        /// <value>Экземпляр класса <see cref="Company"/></value>
        public Company Recipient { get; set; }
        
        /// <summary>
        /// ИД станции отправления
        /// </summary>
        /// <value>Целое число</value>
        public int ShipmentStationId { get; set; }
        
        /// <summary>
        /// Станция отправления
        /// </summary>
        /// <value>Экземпляр класса <see cref="Station"/></value>
        public Station ShipmentStation { get; set; }
        
        /// <summary>
        /// ИД станции доставки
        /// </summary>
        /// <value>Целое число</value>
        public int DestinationStationId { get; set; }
        
        /// <summary>
        /// Станция доставки
        /// </summary>
        /// <value>Экземпляр класса <see cref="Station"/></value>
        public Station DestinationStation { get; set; }
        
        /// <summary>
        /// Железная дорога отправления
        /// </summary>
        /// <value>Экземпляр класса <see cref="RailRoad"/></value>
        [NotMapped]
        public RailRoad ShipmentRailRoad => ShipmentStation.RailRoad;

        /// <summary>
        /// Регион отправления
        /// </summary>
        /// <value>Экземпляр класса <see cref="State"/></value>
        [NotMapped]
        public State ShipmentState => ShipmentStation.State;

        /// <summary>
        /// Страна отправления
        /// </summary>
        /// <value>Экземпляр класса <see cref="Country"/></value>
        [NotMapped]
        public Country ShipmentCountry => ShipmentState.Country;
        
        /// <summary>
        /// Железная дорога доставки
        /// </summary>
        /// <value>Экземпляр класса <see cref="RailRoad"/></value>
        [NotMapped]
        public RailRoad DestinationRailRoad => DestinationStation.RailRoad;

        /// <summary>
        /// Регион доставки
        /// </summary>
        /// <value>Экземпляр класса <see cref="State"/></value>
        [NotMapped]
        public State DestinationState => DestinationStation.State;

        /// <summary>
        /// Страна доставки
        /// </summary>
        /// <value>Экземпляр класса <see cref="Country"/></value>
        [NotMapped]
        public Country DestinationCountry => DestinationState.Country;

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