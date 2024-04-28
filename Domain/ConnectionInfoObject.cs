using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ConnectionInfoObject
    {
        public DateTime Date { get; set; }
        public string TrainNumber { get; set; }
        public string StartStation { get; set; }
        public string EndStation { get; set; }
        public string ProviderName { get; set; }
        public string SourceCity { get; set; }
        public string DestinationCity { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }
        public int KmNumber { get; set; }
        public TimeSpan Duration { get; set; }
        public ConnectionInfoObject() { }
        public ConnectionInfoObject(DateTime date, string trainNumber, string startStation, string endStation, string providerName, string sourceCity, string destinationCity, string departureTime, string arrivalTime, int kmNumber, TimeSpan duration)
        {
            Date = date;
            TrainNumber = trainNumber;
            StartStation = startStation;
            EndStation = endStation;
            ProviderName = providerName;
            SourceCity = sourceCity;
            DestinationCity = destinationCity;
            DepartureTime = departureTime;
            ArrivalTime = arrivalTime;
            KmNumber = kmNumber;
            Duration = duration;
        }
    }
}
