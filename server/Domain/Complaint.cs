namespace Koleo.Models
{
    public class Complaint
    {
        public Guid Id { get; set; }
        public int User_Id { get; set; }
        public int Ticket_Id { get; set; }
        public string Content { get; set; }
    }
}
