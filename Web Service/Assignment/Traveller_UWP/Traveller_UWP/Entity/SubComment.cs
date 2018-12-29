using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traveller_UWP.Entity
{
    public class SubComment
    {
        public int id { get; set; }
        public string sub_Comment1 { get; set; }
        public int Comment_id { get; set; }
        public int Traveler_id { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updateAt { get; set; }
    }
}
