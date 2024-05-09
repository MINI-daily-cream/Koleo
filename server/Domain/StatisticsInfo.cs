using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class StatisticsInfo
    {
        public string ID { get; set; }
        public string User_Id { get; set; }
        public string KmNumber { get; set; }
        public string TrainNumber { get; set; }
        public string ConnectionsNumber { get; set; }
        public string LongestConnectionTime { get; set; }
        public string Points { get; set; }

        public StatisticsInfo(string iD, string user_Id, string kmNumber, string trainNumber, string connectionsNumber, string longestConnectionTime, string points)
        {
            ID = iD;
            User_Id = user_Id;
            KmNumber = kmNumber;
            TrainNumber = trainNumber;
            ConnectionsNumber = connectionsNumber;
            LongestConnectionTime = longestConnectionTime;
            Points = points;
        }

 
    }
}
