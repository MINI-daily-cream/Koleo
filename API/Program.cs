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

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedData(context);

    //Seed.ClearTickets(context);

    //Seed.ClearConnectionsEtc(context);
    //await Seed.SeedConnectionsEtc(context);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration");
    
}

//DatabaseServiceAPI dbService = new DatabaseServiceAPI(builder.Configuration);
//var guid = Guid.NewGuid().ToString().ToUpper();
//string insertTicketSql = $"INSERT INTO Tickets (Id, User_Id, Target_Name, Target_Surname) VALUES ('{guid}', 'C4630E12-DEE8-411E-AF44-E3CA970455CE', 'Ptr', 'Glo')";
//string deleteTicketsSql = $"DELETE FROM Tickets WHERE User_Id='C4630E12-DEE8-411E-AF44-E3CA970455CE'";
//await dbService.ExecuteSQL(deleteTicketsSql);


//Console.WriteLine("-------------------USERS-----------------------------------");
////await dbService.ExecuteSQL("INSERT INTO Users (Id, Name, Surname, Email, Password, CardNumber) VALUES (3, 'Wojciech', 'Domitrz', 'wd@mini.pw.edu.pl', '123', '333')");
//var users = await DatabaseService.ExecuteSQL("SELECT * FROM Users");


//Console.WriteLine($"Number of users: {users.Count}");
//foreach (var row in users)
//{
//    foreach (var rec in row) Console.Write($"{rec} ");
//    Console.WriteLine();
//}

// dbService.Backup();

app.Run();
