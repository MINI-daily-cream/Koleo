using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class AchievementInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public AchievementInfo(string id, string name)
        {
            Id = id;
            Name = name;
        }
   

    }
}
