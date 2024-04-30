using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class AdminComplaint
    {
        public Guid Id { get; set; }
        public int Admin_Id { get; set; }
        public int Complaint_Id { get; set; }
        public string Response { get; set;}
    }
}
