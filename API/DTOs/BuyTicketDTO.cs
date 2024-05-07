using Koleo.Models;

namespace API.DTOs
{
    public class BuyTicketDTO
    {
        //public string userId;
        public List<Connection> connections { get; set; }
        public string targetName { get; set; }
        public string targetSurname { get; set; }
    }
}
