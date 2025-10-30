using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class RankingUser
    {
        public Guid Id { get; set; }
        public Guid Ranking_Id { get; set; }
        public Guid User_Id { get; set; }

        public int Points { get; set; }

        public int Position { get; set; }


    }
}
