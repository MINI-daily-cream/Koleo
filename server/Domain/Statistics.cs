namespace Koleo.Models
{
    public class Statistics
    {
        public Guid Id { get; set; }
        public Guid User_Id { get; set; }
        public int KmNumber { get; set; }
        public int TrainNumber { get; set; }
        public int ConnectionsNumber { get; set; }
        public TimeSpan LongestConnectionTime { get; set; }
        public int Points { get; set; }
    }
}
