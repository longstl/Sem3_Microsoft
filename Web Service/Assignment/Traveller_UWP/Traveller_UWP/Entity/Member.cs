using System;
using System.Collections.Generic;
using Windows.System;
using Traveller_UWP.Entity;

namespace Traveller_UWP
{
    public class Member
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string dob { get; set; }
        public string password { get; set; }
        public string salt { get; set; }
        public bool IsEmailVerified { get; set; }
        public string ActivationCode { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public int Role_id { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Posts> Posts { get; set; }
        public Role Role { get; set; }
        public List<SubComment> Sub_Comment { get; set; }
        public List<Vote> Votes { get; set; }
    }
}