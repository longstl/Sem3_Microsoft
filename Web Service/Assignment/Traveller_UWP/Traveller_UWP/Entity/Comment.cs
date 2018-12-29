using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traveller_UWP.Entity
{
    public class Comment
    {
        public int id { get; set; }
        public string comment1 { get; set; }
        public int Post_id { get; set; }
        public int Traveler_id { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public Member Traveler { get; set; }
        public List<SubComment> Sub_Comment { get; set; }
    }
}
