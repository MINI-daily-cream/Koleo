using Application;
using iTextSharp.text.pdf.parser.clipper;
using Koleo.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context)
        {
            if (context.Users.Any()) return;

            var users = new List<User>
            {
                new User
                {
                    Name = "Sigma",
                    Surname = "Sigmiarski",
                    Email = "sigma@pw.edu.pl",
                    Password = "12345678",
                    CardNumber = "11111111111111111111111111",
                },

                //new User
                //{
                //    Name = "Sigma2",
                //    Surname = "Sigmiarski2",
                //    Email = "sigma2@pw.edu.pl",
                //    Password = "12345678",
                //    CardNumber = "11111111111111111111111111",
                //},

                //new User
                //{
                //    Name = "Sigma3",
                //    Surname = "Sigmiarski3",
                //    Email = "sigma3@pw.edu.pl",
                //    Password = "12345678",
                //    CardNumber = "11111111111111111111111111",
                //},

            };

            await context.Users.AddRangeAsync(users);
            await context.SaveChangesAsync();


        }

        public static async Task SeedConnectionsEtc(DataContext context)
        {
            if (context.Cities.Any() || context.Connections.Any()) return;

            var cities = new List<City>
            {
                new City
                {
                    Name = "Warszawa"
                },
                new City
                {
                    Name = "Kraków"
                },
                new City
                {
                    Name = "Łódź"
                }
            };

            var stations = new List<Station>
            {
                new Station
                {
                    Name = "Warszawa Centralna"
                },
                new Station
                {
                    Name = "Warszawa Zachodnia"
                },
                new Station
                {
                    Name = "Kraków Główny"
                },
                new Station
                {
                    Name = "Łódź Fabryczna"
                }
            };

            var providers = new List<Provider>
            {
                new Provider
                {
                    Name = "PKP Intercity"
                }
            };
            

            await context.Cities.AddRangeAsync(cities);
            await context.Stations.AddRangeAsync(stations);
            await context.Providers.AddRangeAsync(providers);

            await context.SaveChangesAsync();

            cities = context.Cities.ToList();
            stations = context.Stations.ToList();
            providers = context.Providers.ToList();

            var trains = new List<Train>
            {
                new Train
                {
                    Provider_Id = providers.Find(provider => provider.Name == "PKP Intercity").Id.ToString().ToUpper(),
                    Name = "Sobieski"
                },
                new Train
                {
                    Provider_Id = providers.Find(provider => provider.Name == "PKP Intercity").Id.ToString().ToUpper(),
                    Name = "Oleńka"
                },
            };

            var cityStations = new List<CityStation>
            {
                new CityStation
                {
                    City_Id = cities.Find(city => city.Name == "Warszawa").Id,
                    Station_Id = stations.Find(station => station.Name == "Warszawa Centralna").Id
                },
                new CityStation
                {
                    City_Id = cities.Find(city => city.Name == "Warszawa").Id,
                    Station_Id = stations.Find(station => station.Name == "Warszawa Zachodnia").Id
                },
                new CityStation
                {
                    City_Id = cities.Find(city => city.Name == "Kraków").Id,
                    Station_Id = stations.Find(station => station.Name == "Kraków Główny").Id
                },
                new CityStation
                {
                    City_Id = cities.Find(city => city.Name == "Łódź").Id,
                    Station_Id = stations.Find(station => station.Name == "Łódź Fabryczna").Id
                },
            };

            await context.CityStations.AddRangeAsync(cityStations);
            await context.Trains.AddRangeAsync(trains);

            await context.SaveChangesAsync();


        }
    }
}
