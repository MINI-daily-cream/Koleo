using System;
using System.Globalization;

namespace Koleo.Models
{
    public class Connection
    {
        public Guid Id { get; set; }
        public int StartStation_Id { get; set; }
        public int EndStation_Id { get; set; }
        public int Train_Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int KmNumber { get; set; }
        public DateTime Duration { get; set; }
    }
}
