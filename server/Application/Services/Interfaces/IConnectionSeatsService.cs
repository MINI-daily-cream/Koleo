using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    public interface IConnectionSeatsService
    {
        Task<bool> SaveConnectionSeatInfo(string Connection_Id, string seatText);
        Task<bool> RemoveConnectionSeatInfo(string Connection_Id, string seatText);
        Task<List<int>> GetConnectionSeats(string Connection_Id);
    }
}