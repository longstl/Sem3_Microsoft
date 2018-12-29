using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment2.Areas.Traveller.Models
{
    public class CreateParameters
    {
        public Post Post { get; set; }
        public List<Tag> Tag { get; set; }
    }
}