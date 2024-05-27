using Persistence;

using var db = new DataContext();

var connection = db.Connections.Find(new Guid("07E07F56-4746-47AB-A6D4-DD0FD4C4DBDC"));

var connectionSeats = db.ConnectionSeats.Where(c => c.Connection_Id == connection.Id.ToString()).First();

Console.WriteLine(connectionSeats.Id);
Console.WriteLine(connectionSeats.Connection_Id);

var startCity = db.Stations.Find(Guid.Parse(connection.StartStation_Id));
Console.WriteLine(startCity.Name);

var endCity = db.Stations.Find(Guid.Parse(connection.EndStation_Id));
Console.WriteLine(endCity.Name);

Console.WriteLine(connection.StartTime);
Console.WriteLine(connection.EndTime);

connectionSeats.Seats[0] = 7;

db.SaveChanges();
