using Persistence;

using var db = new DataContext();

var connection = db.Connections.Find(new Guid("035ECAA2-BB7B-45AD-9D72-DBE4C5DFCFFE"));

var connectionSeats = db.ConnectionSeats.Where(c => c.Connection_Id == connection.Id.ToString()).First();

Console.WriteLine(connectionSeats.Id);
Console.WriteLine(connectionSeats.Connection_Id);

var startCity = db.Stations.Find(Guid.Parse(connection.StartStation_Id));
Console.WriteLine(startCity.Name);

var endCity = db.Stations.Find(Guid.Parse(connection.EndStation_Id));
Console.WriteLine(endCity.Name);

Console.WriteLine(connection.StartTime);
Console.WriteLine(connection.EndTime);

connectionSeats.Seats[5] = 9;

db.SaveChanges();


// using Persistence;

// using var db  = new DataContext();

// var users = db.Users.ToList();

// foreach (var user in users)
//     if(user.Role != "Admin")
//         user.Role = "User";
    
// db.SaveChanges();