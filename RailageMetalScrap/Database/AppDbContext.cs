using System.Linq;
using Microsoft.EntityFrameworkCore;
using RailageMetalScrap.Database.Models;

namespace RailageMetalScrap.Database
{
    /// <summary>
    /// Класс экземпляра контекста базы данных
    /// </summary>
    public class AppDbContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<RailRoad> RailRoads { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<CargoType> CargoTypes { get; set; }
        public DbSet<Cargo> Cargoes { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        /// <summary>
        /// Метод возвращает экземпляр класса <see cref="Country"/> или, если в таблице нет такой записи, создаёт её
        /// </summary>
        /// <param name="name">Название страны</param>
        /// <returns>Экземпляр класса <see cref="Country"/></returns>
        public Country GetCountry(string name)
        {
            var country = Countries.FirstOrDefault(c => c.Name.Equals(name));
            
            if (country == null)
            {
                country = new Country {Name = name};
                Countries.Add(country);
                SaveChanges();
            }

            return country;
        }
        
        /// <summary>
        /// Метод возвращает экземпляр класса <see cref="RailRoad"/> или, если в таблице нет такой записи, создаёт её
        /// </summary>
        /// <param name="name">Название железной дороги</param>
        /// <param name="countryName">Название страны</param>
        /// <returns>Экземпляр класса <see cref="RailRoad"/></returns>
        public RailRoad GetRailRoad(string name, string countryName)
        {
            var railRoad = RailRoads.FirstOrDefault(r => r.Name.Equals(name) 
                                                         && r.Country.Name.Equals(countryName));
            if (railRoad == null)
            {
                railRoad = new RailRoad {Name = name, Country = GetCountry(countryName)};
                RailRoads.Add(railRoad);
                SaveChanges();
            }

            return railRoad;
        }
        
        /// <summary>
        /// Метод возвращает экземпляр класса <see cref="State"/> или, если в таблице нет такой записи, создаёт её
        /// </summary>
        /// <param name="name">Название региона</param>
        /// <param name="countryName">Название страны</param>
        /// <returns>Экземпляр класса <see cref="State"/></returns>
        public State GetState(string name, string countryName)
        {
            var state = States.FirstOrDefault(s => s.Name.Equals(name) 
                                                   && s.Country.Name.Equals(countryName));
            if (state == null)
            {
                state = new State {Name = name, Country = GetCountry(countryName)};
                States.Add(state);
                SaveChanges();
            }

            return state;
        }

        /// <summary>
        /// Метод возвращает экземпляр класса <see cref="Station"/> или, если в таблице нет такой записи, создаёт её
        /// </summary>
        /// <param name="name">Название станции</param>
        /// <param name="railRoadName">Название железной дороги</param>
        /// <param name="stateName">Название региона</param>
        /// <param name="countryName">Название страны</param>
        /// <returns>Экземпляр класса <see cref="Station"/></returns>
        public Station GetStation(string name, string railRoadName, string stateName, string countryName)
        {
            var station = Stations.FirstOrDefault(s => s.Name.Equals(name)
                                                         && s.State.Name.Equals(stateName)
                                                         && s.RailRoad.Name.Equals(railRoadName));
            if (station == null)
            {
                station = new Station
                {
                    Name = name,
                    State = GetState(stateName, countryName),
                    RailRoad = GetRailRoad(railRoadName, countryName)
                };
                Stations.Add(station);
                SaveChanges();
            }

            return station;
        }

        /// <summary>
        /// Метод возвращает экземпляр класса <see cref="Company"/> или, если в таблице нет такой записи, создаёт её
        /// </summary>
        /// <param name="name">Название компании</param>
        /// <returns>Экземпляр класса <see cref="Company"/></returns>
        public Company GetCompany(string name)
        {
            var company = Companies.FirstOrDefault(c => c.Name.Equals(name));
            
            if (company == null)
            {
                company = new Company {Name = name};
                Companies.Add(company);
                SaveChanges();
            }

            return company;
        }

        /// <summary>
        /// Метод возвращает экземпляр класса <see cref="CargoType"/> или, если в таблице нет такой записи, создаёт её
        /// </summary>
        /// <param name="type">Тип груза</param>
        /// <returns>Экземпляр класса <see cref="CargoType"/></returns>
        public CargoType GetCargoType(string type)
        {
            var cargoType = CargoTypes.FirstOrDefault(t => t.Type.Equals(type));
            
            if (cargoType == null)
            {
                cargoType = new CargoType{Type = type};
                CargoTypes.Add(cargoType);
                SaveChanges();
            }

            return cargoType;
        }
    }
}