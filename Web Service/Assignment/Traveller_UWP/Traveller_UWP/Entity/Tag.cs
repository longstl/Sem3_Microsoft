using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traveller_UWP.Entity
{
    public class Tag
    {
        public int id { get; set; }
        public string tag_name { get; set; }
        public List<Posts> Posts { get; set; }
    }
}
