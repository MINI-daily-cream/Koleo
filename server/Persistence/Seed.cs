using Application;
using Domain;
using iTextSharp.text.pdf.parser.clipper;
using Koleo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
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
                    UserName = "Sigm69",
                    PasswordHash=Encoding.UTF8.GetBytes("brak"),
                    PasswordSalt=Encoding.UTF8.GetBytes("brak"),
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
                },
                new City
                {
                    Name = "Katowice"
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
                    Name = "Warszawa Gdańska"
                },
                new Station
                {
                    Name = "Kraków Główny"
                },
                new Station
                {
                    Name = "Łódź Fabryczna"
                },
                new Station
                {
                    Name = "Łódź Widzew"
                },
                new Station
                {
                    Name = "Łódź Kaliska"
                },
                new Station
                {
                    Name = "Katowice"
                },
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
                new Train
                {
                    Provider_Id = providers.Find(provider => provider.Name == "PKP Intercity").Id.ToString().ToUpper(),
                    Name = "RUBINSTEIN"
                },
                new Train
                {
                    Provider_Id = providers.Find(provider => provider.Name == "PKP Intercity").Id.ToString().ToUpper(),
                    Name = "Żubr"
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
                    City_Id = cities.Find(city => city.Name == "Warszawa").Id,
                    Station_Id = stations.Find(station => station.Name == "Warszawa Gdańska").Id
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
                new CityStation
                {
                    City_Id = cities.Find(city => city.Name == "Łódź").Id,
                    Station_Id = stations.Find(station => station.Name == "Łódź Kaliska").Id
                },
                new CityStation
                {
                    City_Id = cities.Find(city => city.Name == "Łódź").Id,
                    Station_Id = stations.Find(station => station.Name == "Łódź Widzew").Id
                },
                new CityStation
                {
                    City_Id = cities.Find(city => city.Name == "Katowice").Id,
                    Station_Id = stations.Find(station => station.Name == "Katowice").Id
                },
            };

            await context.CityStations.AddRangeAsync(cityStations);
            await context.Trains.AddRangeAsync(trains);

            await context.SaveChangesAsync();

            trains = context.Trains.ToList();

            var connections = SeedConnections(stations, trains);

            await context.Connections.AddRangeAsync(connections);
            //await context.Users.AddRangeAsync(connections);
            await context.SaveChangesAsync();

            var _connections = context.Connections;
            List<ConnectionSeats> connectionSeats = new List<ConnectionSeats>();

            foreach (var connection in _connections)
            {
                Console.WriteLine("kk: " + connection.Id);
                connectionSeats.Add(new ConnectionSeats{
                    Connection_Id = connection.Id.ToString()
                });
            }
            await context.ConnectionSeats.AddRangeAsync(connectionSeats);
            await context.SaveChangesAsync();
        }

        private static List<Connection> SeedConnections(List<Station> stations, List<Train> trains)
        {
            var connections = new List<Connection>();
            for (int day = 22; day < 31; day++)
            {
                connections.AddRange(
                    new List<Connection>
                    {
                        new Connection
                        {
                            StartStation_Id = stations.Find(station => station.Name == "Warszawa Centralna").Id.ToString().ToUpper(),
                            EndStation_Id = stations.Find(station => station.Name == "Kraków Główny").Id.ToString().ToUpper(),
                            Train_Id = trains.Find(train => train.Name == "Żubr").Id.ToString().ToUpper(),
                            StartTime = new DateTime(2024, 06, day, 8, 37, 0),
                            EndTime = new DateTime(2024, 06, day, 12, 12, 0),
                            KmNumber = 1,
                            Duration = new DateTime(2024, 06, day, 12, 12, 0).Subtract(new DateTime(2024, 06, day, 8, 37, 0))
                        },
                        new Connection
                        {
                            StartStation_Id = stations.Find(station => station.Name == "Warszawa Centralna").Id.ToString().ToUpper(),
                            EndStation_Id = stations.Find(station => station.Name == "Kraków Główny").Id.ToString().ToUpper(),
                            Train_Id = trains.Find(train => train.Name == "Żubr").Id.ToString().ToUpper(),
                            StartTime = new DateTime(2024, 06, day, 12, 37, 0),
                            EndTime = new DateTime(2024, 06, day, 16, 12, 0),
                            KmNumber = 1,
                            Duration = new DateTime(2024, 06, day, 16, 12, 0).Subtract(new DateTime(2024, 06, day, 12, 37, 0))
                        },
                        new Connection
                        {
                            StartStation_Id = stations.Find(station => station.Name == "Warszawa Centralna").Id.ToString().ToUpper(),
                            EndStation_Id = stations.Find(station => station.Name == "Łódź Fabryczna").Id.ToString().ToUpper(),
                            Train_Id = trains.Find(train => train.Name == "RUBINSTEIN").Id.ToString().ToUpper(),
                            StartTime = new DateTime(2024, 06, day, 9, 23, 0),
                            EndTime = new DateTime(2024, 06, day, 10, 39, 0),
                            KmNumber = 1,
                            Duration = new DateTime(2024, 06, day, 10, 39, 0).Subtract(new DateTime(2024, 06, day, 9, 23, 0))
                        },
                        new Connection
                        {
                            StartStation_Id = stations.Find(station => station.Name == "Warszawa Zachodnia").Id.ToString().ToUpper(),
                            EndStation_Id = stations.Find(station => station.Name == "Łódź Fabryczna").Id.ToString().ToUpper(),
                            Train_Id = trains.Find(train => train.Name == "RUBINSTEIN").Id.ToString().ToUpper(),
                            StartTime = new DateTime(2024, 06, day, 9, 23, 0),
                            EndTime = new DateTime(2024, 06, day, 10, 39, 0),
                            KmNumber = 1,
                            Duration = new DateTime(2024, 06, day, 10, 39, 0).Subtract(new DateTime(2024, 06, day, 9, 23, 0))
                        },
                        new Connection
                        {
                            StartStation_Id = stations.Find(station => station.Name == "Warszawa Centralna").Id.ToString().ToUpper(),
                            EndStation_Id = stations.Find(station => station.Name == "Łódź Widzew").Id.ToString().ToUpper(),
                            Train_Id = trains.Find(train => train.Name == "RUBINSTEIN").Id.ToString().ToUpper(),
                            StartTime = new DateTime(2024, 06, day, 9, 23, 0),
                            EndTime = new DateTime(2024, 06, day, 10, 39, 0),
                            KmNumber = 1,
                            Duration = new DateTime(2024, 06, day, 10, 39, 0).Subtract(new DateTime(2024, 06, day, 9, 23, 0))
                        },
                        new Connection
                        {
                            StartStation_Id = stations.Find(station => station.Name == "Warszawa Gdańska").Id.ToString().ToUpper(),
                            EndStation_Id = stations.Find(station => station.Name == "Łódź Fabryczna").Id.ToString().ToUpper(),
                            Train_Id = trains.Find(train => train.Name == "RUBINSTEIN").Id.ToString().ToUpper(),
                            StartTime = new DateTime(2024, 06, day, 9, 23, 0),
                            EndTime = new DateTime(2024, 06, day, 10, 39, 0),
                            KmNumber = 1,
                            Duration = new DateTime(2024, 06, day, 10, 39, 0).Subtract(new DateTime(2024, 06, day, 9, 23, 0))
                        },
                        new Connection
                        {
                            StartStation_Id = stations.Find(station => station.Name == "Warszawa Gdańska").Id.ToString().ToUpper(),
                            EndStation_Id = stations.Find(station => station.Name == "Łódź Kaliska").Id.ToString().ToUpper(),
                            Train_Id = trains.Find(train => train.Name == "RUBINSTEIN").Id.ToString().ToUpper(),
                            StartTime = new DateTime(2024, 06, day, 9, 23, 0),
                            EndTime = new DateTime(2024, 06, day, 10, 39, 0),
                            KmNumber = 1,
                            Duration = new DateTime(2024, 06, day, 10, 39, 0).Subtract(new DateTime(2024, 06, day, 9, 23, 0))
                        },
                        new Connection
                        {
                            StartStation_Id = stations.Find(station => station.Name == "Łódź Fabryczna").Id.ToString().ToUpper(),
                            EndStation_Id = stations.Find(station => station.Name == "Katowice").Id.ToString().ToUpper(),
                            Train_Id = trains.Find(train => train.Name == "Oleńka").Id.ToString().ToUpper(),
                            StartTime = new DateTime(2024, 06, day, 10, 56, 0),
                            EndTime = new DateTime(2024, 06, day, 15, 40, 0),
                            KmNumber = 1,
                            Duration = new DateTime(2024, 06, day, 15, 40, 0).Subtract(new DateTime(2024, 06, day, 10, 56, 0))
                        },
                        new Connection
                        {
                            StartStation_Id = stations.Find(station => station.Name == "Kraków Główny").Id.ToString().ToUpper(),
                            EndStation_Id = stations.Find(station => station.Name == "Katowice").Id.ToString().ToUpper(),
                            Train_Id = trains.Find(train => train.Name == "Sobieski").Id.ToString().ToUpper(),
                            StartTime = new DateTime(2024, 06, day, 10, 56, 0),
                            EndTime = new DateTime(2024, 06, day, 15, 40, 0),
                            KmNumber = 1,
                            Duration = new DateTime(2024, 06, day, 15, 40, 0).Subtract(new DateTime(2024, 06, day, 10, 56, 0))
                        },
                        new Connection
                        {
                            StartStation_Id = stations.Find(station => station.Name == "Kraków Główny").Id.ToString().ToUpper(),
                            EndStation_Id = stations.Find(station => station.Name == "Katowice").Id.ToString().ToUpper(),
                            Train_Id = trains.Find(train => train.Name == "Sobieski").Id.ToString().ToUpper(),
                            StartTime = new DateTime(2024, 06, day, 12, 56, 0),
                            EndTime = new DateTime(2024, 06, day, 17, 40, 0),
                            KmNumber = 1,
                            Duration = new DateTime(2024, 06, day, 17, 40, 0).Subtract(new DateTime(2024, 06, day, 12, 56, 0))
                        },
                        new Connection
                        {
                            StartStation_Id = stations.Find(station => station.Name == "Kraków Główny").Id.ToString().ToUpper(),
                            EndStation_Id = stations.Find(station => station.Name == "Katowice").Id.ToString().ToUpper(),
                            Train_Id = trains.Find(train => train.Name == "Sobieski").Id.ToString().ToUpper(),
                            StartTime = new DateTime(2024, 06, day, 14, 56, 0),
                            EndTime = new DateTime(2024, 06, day, 19, 40, 0),
                            KmNumber = 1,
                            Duration = new DateTime(2024, 06, day, 19, 40, 0).Subtract(new DateTime(2024, 06, day, 14, 56, 0))
                        },
                    });
                if (day % 2 == 0)
                {
                    connections.Add(
                        new Connection
                        {
                            StartStation_Id = stations.Find(station => station.Name == "Warszawa Centralna").Id.ToString().ToUpper(),
                            EndStation_Id = stations.Find(station => station.Name == "Katowice").Id.ToString().ToUpper(),
                            Train_Id = trains.Find(train => train.Name == "Sobieski").Id.ToString().ToUpper(),
                            StartTime = new DateTime(2024, 06, day, 8, 40, 0),
                            EndTime = new DateTime(2024, 06, day, 11, 07, 0),
                            KmNumber = 1,
                            Duration = new DateTime(2024, 06, day, 11, 07, 0).Subtract(new DateTime(2024, 06, day, 8, 40, 0))
                        });
                }
                connections.Add(
                        new Connection
                        {
                            StartStation_Id = stations.Find(station => station.Name == "Warszawa Centralna").Id.ToString().ToUpper(),
                            EndStation_Id = stations.Find(station => station.Name == "Katowice").Id.ToString().ToUpper(),
                            Train_Id = trains.Find(train => train.Name == "Sobieski").Id.ToString().ToUpper(),
                            StartTime = new DateTime(2024, 06, day, 0, 20, 0),
                            EndTime = new DateTime(2024, 06, day, 11, 07, 0),
                            KmNumber = 1,
                            Duration = new DateTime(2024, 06, day, 11, 07, 0).Subtract(new DateTime(2024, 06, day, 8, 40, 0))
                        });
            }

            return connections;
        }

        public static async Task SeedAchievements(DataContext context)
        {
            if (context.Achievement.Any()) return;

            var achievements = new List<Achievement>
            {
                         new Achievement
                {
                    Id = Guid.NewGuid(),
                    Name = "Przejechane 1 km"
                },
                new Achievement
                {
                    Id = Guid.NewGuid(),
                    Name = "Przejechane 10 km"
                },
                new Achievement
                {
                    Id = Guid.NewGuid(),
                    Name = "Przejechane 100 km"
                },
                new Achievement
                {
                    Id = Guid.NewGuid(),
                    Name = "Przejechane 1000 km"
                }
            };

            await context.Achievement.AddRangeAsync(achievements);
            await context.SaveChangesAsync();
        }

        public static void ClearConnectionsEtc(DataContext context)
        {
            Console.WriteLine("Clearing data---------------------------------------------------------------------------");
            var _context = context;

            var allCities = _context.Cities.ToList();
            _context.Cities.RemoveRange(allCities);

            var allStations = _context.Stations.ToList();
            _context.Stations.RemoveRange(allStations);

            var allCityStations = _context.CityStations.ToList();
            _context.CityStations.RemoveRange(allCityStations);

            var allTicketConnections = _context.TicketConnections.ToList();
            _context.TicketConnections.RemoveRange(allTicketConnections);

            var allTrains = _context.Trains.ToList();
            _context.Trains.RemoveRange(allTrains);

            var allProviders = _context.Providers.ToList();
            _context.Providers.RemoveRange(allProviders);

            var allConnections = _context.Connections.ToList();
            _context.Connections.RemoveRange(allConnections);

            var allConnectionSeats = _context.ConnectionSeats.ToList();
            _context.ConnectionSeats.RemoveRange(allConnectionSeats);

            _context.SaveChanges();
        }

        public static void ClearTickets(DataContext context)
        {
            Console.WriteLine("Clearing tickets---------------------------------------------------------------------------");
            var _context = context;

            var allTickets = _context.Tickets.ToList();
            _context.Tickets.RemoveRange(allTickets);

            var allTicketConnections = _context.TicketConnections.ToList();
            _context.TicketConnections.RemoveRange(allTicketConnections);

            _context.SaveChanges();
        }
        public static async Task SeedRankings(DataContext context)
        {



            if (context.Rankings.Any()) return;

      

            var rankings = new List<Ranking>
    {
        new Ranking
        {
            Id = Guid.NewGuid(),
            Description = "KmNumber",
            Name = "KmNumber"
        },
        new Ranking
        {
            Id = Guid.NewGuid(),
            Description = "ConnectionsNumber",
            Name = "ConnectionsNumber"
        },
        new Ranking
        {
            Id = Guid.NewGuid(),
            Description = "LongestConnectionTime",
            Name = "LongestConnectionTime"
        },
        new Ranking
        {
            Id = Guid.NewGuid(),
            Description = "TrainNumber",
            Name = "TrainNumber"
        },
        new Ranking
        {
            Id = Guid.NewGuid(),
            Description = "Points",
            Name = "Points"
        },
    };


            await context.Rankings.AddRangeAsync(rankings);



            await context.SaveChangesAsync();
        }

        public static async Task SeedTestUsersAndStaticsData(DataContext context)
        {
            if (context.Statistics.Any()) return;
            using var hmac = new HMACSHA512();


                var users = new List<User>
            {
                new User
                {
                    UserName = "Wojaktest",
                                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("123")),
                PasswordSalt = hmac.Key,
                    Name = "Wojak",
                    Surname = "Wojak",
                    Email = "Wojak@pw.edu.pl",
                    Password = "123",
                    CardNumber = "11111111111111111111111111",
                },
                             new User
                {
                    UserName = "Wojaktest2",
                                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("123")),
                PasswordSalt = hmac.Key,
                    Name = "Wojak",
                    Surname = "Wojak",
                    Email = "Wojak2@pw.edu.pl",
                    Password = "123",
                    CardNumber = "11111111111111111111111111",
                },
            };

            context.Users.AddRange(users);

            // Save changes to the database
            await context.SaveChangesAsync();
            var user1 = await context.Users.FirstOrDefaultAsync(u => u.Email == "Wojak@pw.edu.pl");
            var user2 = await context.Users.FirstOrDefaultAsync(u => u.Email == "Wojak2@pw.edu.pl");

            // List to hold new statistics records
            var statisticsList = new List<Statistics>();

            if (user1 != null)
            {
                statisticsList.Add(new Statistics
                {
                    Id = Guid.NewGuid(),
                    User_Id = user1.Id,
                    KmNumber = 1,
                    ConnectionsNumber = 1,
                    LongestConnectionTime = TimeSpan.Zero,
                    TrainNumber = 10,
                    Points = 0
                });
            }

            if (user2 != null)
            {
                statisticsList.Add(new Statistics
                {
                    Id = Guid.NewGuid(),
                    User_Id = user2.Id,
                    KmNumber = 200,
                    ConnectionsNumber = 1,
                    LongestConnectionTime = TimeSpan.Zero,
                    TrainNumber = 1,
                    Points = 1
                });
            }


            context.Statistics.AddRange(statisticsList);

            await context.SaveChangesAsync();
        }

    }
}