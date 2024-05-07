using API.Services.Interfaces;
using Koleo.Models;
using Koleo.Services;
using KoleoPL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Persistence;
using SQLitePCL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IDatabaseServiceAPI, DatabaseServiceAPI>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<ITicketServive, TicketService>();
builder.Services.AddScoped<IStatisticsService, StatisticsService>();
builder.Services.AddScoped<IRankingService, RankingService>();


builder.Services.AddControllers(opt =>
{
    //var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    //opt.Filters.Add(new AuthorizeFilter(policy));
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(opt => {
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyOrigin()
        //.SetIsOriginAllowedToAllowWildcardSubdomains()
        .AllowAnyMethod().AllowAnyHeader();
        //;
        //.AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.MapControllers();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedData(context);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration");
    
}

DatabaseServiceAPI dbService = new DatabaseServiceAPI(builder.Configuration);

Console.WriteLine("-------------------USERS-----------------------------------");
//await dbService.ExecuteSQL("INSERT INTO Users (Id, Name, Surname, Email, Password, CardNumber) VALUES (3, 'Wojciech', 'Domitrz', 'wd@mini.pw.edu.pl', '123', '333')");
//



// Act


//await dbService.ExecuteSQLLastRow("INSERT INTO STATISTICS (Id, USER_ID, KMNUMBER, TRAINNUMBER,CONNECTIONSNUMBER,LONGESTCONNECTIONTIME,POINTS) VALUES (123456,12345, 3, 3, 3, '3', 3)");
//await dbService.ExecuteSQL("INSERT INTO Statistics (Id, User_Id, KmNumber, TrainNumber, ConnectionsNumber, LongestConnectionTime, Points) VALUES (2, 1, 100, 5, 2, '2024-05-07T12:00:00', 10)");
///var users = await DatabaseService.ExecuteSQL("SELECT Id, User_Id, KmNumber, TrainNumber, ConnectionsNumber, LongestConnectionTime, Points FROM Statistics;");
//await dbService.ExecuteSQL($"INSERT INTO Statistics ( id,User_Id, KmNumber, TrainNumber, ConnectionsNumber, LongestConnectionTime, Points)  VALUES (123456,12, 100, 5, 2, '2024-05-07T12:00:00', 10)");
var users = await dbService.ExecuteSQLLastRow("SELECT * FROM STATISTICS");



//Console.WriteLine(users.Count);
//users = await DatabaseService.ExecuteSQL("SELECT user_id FROM STATISTICS where id='1'");
//await dbService.ExecuteSQL("INSERT INTO STATISTICS (Id, USER_ID, KMNUMBER, TRAINNUMBER,CONNECTIONSNUMBER,LONGESTCONNECTIONTIME,POINTS) VALUES (33,33,3 ,3,3,'3',3)");
Console.WriteLine($"Number of users: {users.Item1[0][3]}");
//foreach (var row in users)
//{
//    foreach (var rec in row) Console.Write($"{rec} ");
//    Console.WriteLine();
//}

// dbService.Backup();

app.Run();



record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
