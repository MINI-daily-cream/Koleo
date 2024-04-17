using System.Text;
using API.Interfaces;
using API.Services;
using API.Services.Interfaces;
using Koleo.Models;
using KoleoPL.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Persistence;
using SQLitePCL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection")//,
        // b => b.MigrationsAssembly("Persistence")
    )
);

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"])),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



// using var scope = app.Services.CreateScope();
// var services = scope.ServiceProvider;
// try
// {
//     var context = services.GetRequiredService<DataContext>();
//     await context.Database.MigrateAsync();
//     await Seed.SeedData(context);
// }
// catch (Exception ex)
// {
//     var logger = services.GetRequiredService<ILogger<Program>>();
//     logger.LogError(ex, "An error occured during migration");
    
// }

// DatabaseServiceAPI dbService = new DatabaseServiceAPI(builder.Configuration);

// Console.WriteLine("-------------------USERS-----------------------------------");
// //await dbService.ExecuteSQL("INSERT INTO Users (Id, Name, Surname, Email, Password, CardNumber) VALUES (3, 'Wojciech', 'Domitrz', 'wd@mini.pw.edu.pl', '123', '333')");
// var users = await DatabaseService.ExecuteSQL("SELECT * FROM Users");

// // Act


// Console.WriteLine($"Number of users: {users.Count}");
// foreach (var row in users)
// {
//     foreach (var rec in row) Console.Write($"{rec} ");
//     Console.WriteLine();
// }

app.UseAuthentication();
app.UseAuthorization(); 

app.MapControllers();

// dbService.Backup();

app.Run();