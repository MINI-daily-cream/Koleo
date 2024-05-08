namespace API.DTOs
{
    public class TicketInfoDTO 
    {
        public string Id { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public TimeSpan Duration { get; set; }
        public string TrainNumber { get; set; }
        public string StartStation { get; set; }
        public string EndStation { get; set; }
        public string ProviderName { get; set; }
        public string SourceCity { get; set; }
        public string DestinationCity { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }
        public int KmNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
