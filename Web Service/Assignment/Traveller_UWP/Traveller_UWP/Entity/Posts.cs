using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traveller_UWP.Entity
{
    public class Posts
    {
        public int id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string link { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public int Traveler_id { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Image> Images { get; set; }
        public Member Traveler { get; set; }
        public List<Vote> Votes { get; set; }
        public List<Tag> Tags { get; set; }
    }
}
