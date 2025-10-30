namespace Koleo.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CardNumber { get; set; }
        public string Role { get; set; } = "User";

        // [TICKET CONTROLLER]
        //public (bool, int?) BuyTicket() 
        //{
        //    return (false, null);
        //} // czy sie udalo, id bileta
        //public List<int> ListTickets() 
        //{
        //    return new List<int>();
        //}
        //public (bool, int) ExchangeTicket(int ticketId)
        //{
        //    return (false, -1);
        //}//czy sie udalo, nowy bilet
        //public bool DropTicket(int ticketId)
        //{
        //    return false;
        //}// czy sie udalo

        // [ACCOUNT CONTROLLER]
        //public int GetAccountInfo()
        //{
        //    return -1;
        //}
        //public bool UpdateAccountInfo()
        //{
        //    return false;
        //}


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
