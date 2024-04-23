using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KoleoPL.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Xunit;
using Koleo.Models;

namespace KoleoPL.Tests
{
    public class DatabaseServiceIntegrationTests : IAsyncLifetime
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
        public async Task AddUser_And_GetUser_Should_Work_Correctly()
        {
            // Arrange
            var newUser = new User { Name = "TestUser", Surname = "TestSurname", Email = "TestEmail", Password = "TestPassword", CardNumber = "1" };
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            // Act
            var users = await _context.Users.ToListAsync();
            
            // Assert
            Assert.NotNull(users);
            Assert.True(users.Count > 0);
        }
    }
}
