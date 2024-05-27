// using Org.BouncyCastle.Asn1;
// using Persistence;

// using var db = new DataContext();

// Console.WriteLine("halochen");

// var connection = db.Connections.First();
// Console.WriteLine(connection.StartStation_Id);

// var startStation = db.Stations.Find(Guid.Parse(connection.StartStation_Id));
// var endStation = db.Stations.Find(Guid.Parse(connection.EndStation_Id));

// Console.WriteLine(startStation.Name);
// Console.WriteLine(endStation.Name);
// List<int> seats = connection.Seats;
// Console.WriteLine(seats.Count);
// seats[0] = 0;
// foreach (var seat in seats)
// {
//     Console.WriteLine(seat);
// }

using Koleo.Models;
using Persistence;

using var db = new DataContext();

var stations = db.Stations.ToList();
var trains = db.Trains.ToList();

int day = 28;

Console.WriteLine("bylo connections: " + db.Connections.Count());

var newConnection = new Connection
{
    StartStation_Id = stations.Find(station => station.Name == "Warszawa Centralna").Id.ToString().ToUpper(),
    EndStation_Id = stations.Find(station => station.Name == "Łódź Fabryczna").Id.ToString().ToUpper(),
    Train_Id = trains.Find(train => train.Name == "RUBINSTEIN").Id.ToString().ToUpper(),
    StartTime = new DateTime(2024, 05, day, 9, 23, 0),
    EndTime = new DateTime(2024, 05, day, 10, 39, 0),
    KmNumber = 2,
    Duration = new DateTime(2024, 05, day, 10, 39, 0).Subtract(new DateTime(2024, 05, day, 9, 23, 0)),
    TestValue2 = "xd"
};

db.Add(newConnection);
db.SaveChanges();

Console.WriteLine("jest connections: " + db.Connections.Count());
