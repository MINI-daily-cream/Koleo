using API.Services.Interfaces;
using Koleo.Models;
using Koleo.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace UnitTests.TicketServiceUnitTests
{
    public class TicketServiceTests
    {
        private readonly TicketService _ticketService;
        private readonly Mock<IDatabaseServiceAPI> _databaseservice;
        private readonly Mock<IPaymentService> _paymentService;

        public TicketServiceTests()
        {
            _databaseservice = new Mock<IDatabaseServiceAPI>();
            _paymentService = new Mock<IPaymentService>();

            //SUT
            _ticketService = new TicketService(_databaseservice.Object, _paymentService.Object);
        }

        //[Fact]
        //public async void TicketServices_Buy_ReturnsSuccess()
        //{
        //    Guid userId = Guid.NewGuid();
        //    string targetName = "Adam";
        //    string targetSurname = "Nowak";
        //    List<Connection> connections = new List<Connection>();

        //    _paymentService.Setup(x => x.ProceedPayment()).ReturnsAsync(true);

        //    var result = await _ticketService.Buy(userId, connections, targetName, targetSurname);

        //    Assert.True(result);
        //}
        //[Fact]
        //public async void TicketServices_Add_ReturnsSuccess()
        //{
        //    Guid userId = Guid.NewGuid();
        //    Guid ticketId = Guid.NewGuid();
        //    Guid Id1 = Guid.NewGuid();
        //    Guid Id2 = Guid.NewGuid();
        //    string targetName = "Adam";
        //    string targetSurname = "Nowak";
        //    List<Koleo.Models.Connection> connections = new List<Connection>();
        //    Connection connection = new Koleo.Models.Connection(Guid.NewGuid());

        //    _databaseservice.Setup(x => x.ExecuteSQL(
        //        $"INSERT INTO Tickets (User_Id, Target_Name, Target_Surname) VALUES ('{userId}', '{targetName}', '{targetSurname}')"))
        //        .ReturnsAsync(new List<string[]>());
        //    _databaseservice.Setup(x => x.ExecuteSQL("SELECT last_insert_rowid()"))
        //        .ReturnsAsync(new List<string[]> { new string[] { "1", "2", "3" }, new string[] { "1", "2", "3" } });

        //    _databaseservice.Setup(x => x.ExecuteSQL($"INSERT INTO TicketConnections (Ticket_Id, Connection_Id) VALUES ('{ticketId}', '{connection.Id}')")).ReturnsAsync(new List<string[]>()).Verifiable();

        //    // Act
        //    await _ticketService.Add(userId, connections, targetName, targetSurname);

        //    // Assert
        //    // Assert that ExecuteSQL was called with the correct SQL queries
        //    _databaseservice.Verify(x => x.ExecuteSQL(It.IsAny<string>()), Times.Exactly(3));
        //}

    }
}
