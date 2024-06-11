using API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Koleo.Services
{
    public class ConnectionSeatsService : IConnectionSeatsService
    {
        private readonly DataContext _dataContext;

        public ConnectionSeatsService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> SaveConnectionSeatInfo(string Connection_Id, string seatText)
        {
            int seat = int.Parse(seatText);
            Console.WriteLine("zapisze miejsce: " + seat);
            var connection = await _dataContext.Connections.FindAsync(new Guid(Connection_Id));
            var connectionSeats = await _dataContext.ConnectionSeats.Where(cs => cs.Connection_Id == connection.Id.ToString()).FirstAsync();
            if( connectionSeats.Seats[seat] == 1)
                return false;
            connectionSeats.Seats[seat] = 1;
            await _dataContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveConnectionSeatInfo(string Connection_Id, string seatText)
        {
            int seat = int.Parse(seatText);
            Console.WriteLine("zwalniam miejsce: " + seat);
            var connection = await _dataContext.Connections.FindAsync(new Guid(Connection_Id));
            var connectionSeats = await _dataContext.ConnectionSeats.Where(cs => cs.Connection_Id == connection.Id.ToString()).FirstAsync();
            connectionSeats.Seats[seat] = 0;
            await _dataContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<int>> GetConnectionSeats(string Connection_Id)
        {
            List<int> seats = _dataContext.ConnectionSeats.Where(cs => cs.Connection_Id == Connection_Id.ToLower()).First().Seats;
            return seats;
        }
    }
}