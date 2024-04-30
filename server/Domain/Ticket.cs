namespace Koleo.Models
{
    public class Ticket
    {
        public Guid Id { get; set; }
        public int User_Id { get; set; }
        public string Target_Name { get; set; }
        public string Target_Surname { get; set; }
    }
}
