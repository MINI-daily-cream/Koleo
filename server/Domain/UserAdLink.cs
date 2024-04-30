using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class UserAdLink
    {
        public Guid Id { get; set; }
        public int User_Id { get; set; }
        public int Ad_Id { get; set; }
    }
}
