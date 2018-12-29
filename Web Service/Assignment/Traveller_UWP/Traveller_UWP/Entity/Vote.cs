using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traveller_UWP.Entity
{
    public class Vote
    {
        public int id { get; set; }
        public int vote1 { get; set; }
        public int Post_id { get; set; }
        public int Traveler_id { get; set; }
    }
}
