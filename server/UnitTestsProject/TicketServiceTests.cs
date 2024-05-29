using API.Services.Interfaces;
using Domain;
using Koleo.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestsProject
{
    public class TicketServiceTests
    {
        [Fact]
        public async Task Remove_Returns_True_When_Ticket_Removed_Successfully()
        {
            var mockPaymentService = new Mock<IPaymentService>();
            mockPaymentService.Setup(x => x.CancelPayment())
                              .ReturnsAsync(true);

            var mockDatabaseService = new Mock<IDatabaseServiceAPI>();
            mockDatabaseService.SetupSequence(x => x.ExecuteSQL(It.IsAny<string>()))
                               .ReturnsAsync((null, true))
                               .ReturnsAsync((null, true));

            var ticketService = new TicketService(mockDatabaseService.Object, mockPaymentService.Object);

            var result = await ticketService.Remove("ticketId");
            Assert.True(result);
        }
    }
}
