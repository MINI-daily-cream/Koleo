using System;
using System.Globalization;

namespace Koleo.Models
{
    public class Connection
    {
        public Guid Id { get; set; }
        public string StartStation_Id { get; set; }
        public string EndStation_Id { get; set; }
        public string Train_Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int KmNumber { get; set; }
        public TimeSpan Duration { get; set; }

        public const int Seats_Count  = 10;
        public List<int> Seats { get; set; } = new List<int>(new int[Seats_Count]);

        public int[] Seats2 { get; set; } = new int[10];
        public string TestValue { get; set; }
        public string TestValue2 { get; set; } = "przetestowane2";


        public Connection()
        {
            TestValue = "przetestowane";
        }
    }
}
