namespace Koleo.Models
{
    public class User
    {
        private int ID;
        public (bool, int?) BuyTicket() 
        {
            return (false, null);
        } // czy sie udalo, id bileta
        public List<int> ListTickets()
        {
            return new List<int>();
        }
        public (bool, int) ExchangeTicket(int ticketId)
        {
            return (false, -1);
        }//czy sie udalo, nowy bilet
        public bool DropTicket(int ticketId)
        {
            return false;
        }// czy sie udalo
        public int MakeComplaint()
        {
            return -1;
        }// id complaint-a (czy caly complaint??)
        public List<int> ListComplaints()
        {
            return new List<int>();
        }// (...??)
        public bool EditComplaint(int complaintId)
        {
            return false;
        }
        public bool RemoveComplaint(int complaintId)
        {
            return false;
        }
        public int GetAccountInfo()
        {
            return -1;
        }
        public bool UpdateAccountInfo()
        {
            return false;
        }
        public int GetStatistics()
        {
            return -1;
        }// moze lista?
        public List<int> GetRankings()
        {
            return new List<int>();
        }

    }
}
