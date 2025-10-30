namespace Koleo.Models 
{
    public class ConnectionSeats
    {
        public Guid Id { get; set; }
        public string Connection_Id { get; set; }
        public const int Seats_Count = 180;
        public List<int> Seats { get; set; } = new List<int>(new int[Seats_Count]);
    }
}