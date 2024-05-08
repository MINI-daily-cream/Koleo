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
        public int Ranking_Id { get; set; }
        public int User_Id { get; set; }

        public int Points { get; set; }

        public int Position { get; set; }


    }
}
