using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traveller_UWP.Entity
{
    public class Role
    {
        public int id { get; set; }
        public string role_name { get; set; }
        public List<Member> Travelers { get; set; }
    }
}
