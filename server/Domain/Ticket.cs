namespace Koleo.Models
{
    public class Ticket
    {
        public Guid Id { get; set; }
        public string User_Id { get; set; }
        public string Target_Name { get; set; }
        public string Target_Surname { get; set; }
        public string Seat { get; set; }
    }
}
