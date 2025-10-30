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
    }
}
