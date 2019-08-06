using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RailageMetalScrap.Database
{
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