namespace Koleo.Models
{
    public class User
    {
        private int ID;
        public (bool, int?) BuyTicket() { } // czy sie udalo, id bileta
        public List<int> ListTickets() { }
        public (bool, int) ExchangeTicket(int ticketId) { } //czy sie udalo, nowy bilet
        public bool DropTicket(int ticketId) { } // czy sie udalo
        public int MakeComplaint() { } // id complaint-a (czy caly complaint??)
        public List<int> ListComplaints() { } // (...??)
        public bool EditComplaint(int complaintId) { }
        public bool RemoveComplaint(int complaintId) { }
        public int GetAccountInfo() {}
        public bool UpdateAccountInfo() { }
        public int GetStatistics() { } // moze lista?
        public List<int> GetRankings() { }

    }
}
