using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KoleoPL.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Xunit;
using Koleo.Models;
using Koleo.Services;

namespace KoleoPL.Tests
{
    public class DatabaseServiceTests : IAsyncLifetime
    {
        private IConfiguration _configuration;
        private DataContext _context;

        public async Task InitializeAsync()
        {
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Development.json")
                .Build();

            var options = new DbContextOptionsBuilder<DataContext>()
                .UseSqlite(_configuration.GetConnectionString("DefaultConnection"))
                .Options;

            _context = new DataContext(options);

            await _context.Database.EnsureCreatedAsync();
        }

        public Task DisposeAsync()
        {
            _context.Dispose();
            return Task.CompletedTask;
        }

        [Fact]
        public async Task ExecuteSQL_Should_Return_Results()
        {
            // Arrange
            var databaseService = new DatabaseServiceAPI(_configuration);
            var sql = "Select * From Users";

      
            var newUser = new User { Name = "TestUser", Surname="TestSurname",Email="TestEmail",Password="TestPassword", CardNumber="1" };
   
            await _context.SaveChangesAsync();

            // Act
            var list = await databaseService.ExecuteSQL(sql);
            var users = await _context.Users.ToListAsync();

            // Assert
            Assert.NotNull(list);
            
            Assert.True(list.Count > 0);
        }

        [Fact]
        public Task Backup_Should_Create_Backup_Database()
        {
            // Arrange
            var databaseService = new DatabaseServiceAPI(_configuration);

            // Act
             databaseService.Backup();

            // Assert
            Assert.True(File.Exists("KoleoBackup.db"));
            return Task.CompletedTask;
        }
    }
}
