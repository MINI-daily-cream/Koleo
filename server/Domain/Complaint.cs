namespace Koleo.Models
{
    public class Complaint
    {
        public Guid Id { get; set; }
        public string User_Id { get; set; }
        public string Ticket_Id { get; set; }
        public string Content { get; set; }
    }
}
