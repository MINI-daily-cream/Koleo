using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ComplaintWithAnswer
    {
        public string ticketId { get; set; }
        public string content { get; set; }
        public string response { get; set; }
        public string complaintId {get; set;}
    }
}
