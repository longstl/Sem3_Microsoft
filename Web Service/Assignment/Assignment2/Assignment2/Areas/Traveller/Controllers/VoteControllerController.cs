using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Assignment2.Areas.Traveller.Models;

namespace Assignment2.Areas.Traveller.Controllers
{
    [RoutePrefix("api/v1/traveller")]
    public class VoteControllerController : ApiController
    {
        Assignment2_ServicesContext db = new Assignment2_ServicesContext();

        [HttpPost]
        [Route("Vote/add")]
        public HttpResponseMessage AddVote([FromBody] CreateParrameters1 pram)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (ModelState.IsValid)
            {
                db.Votes.Add(pram.Vote);
                db.SaveChanges();
            }

            pram.Comment.createdAt = DateTime.Now;
            pram.Comment.updatedAt = DateTime.Now;
            db.Comments.Add(pram.Comment);
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, pram);
        }

        [HttpGet]
        [Route("Vote/")]
        public HttpResponseMessage GetVoteByPostId([FromUri] int Post_id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!VoteExists(Post_id))
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Post does not exist");
            }

            var v = CalVote(Post_id);
            return Request.CreateResponse(HttpStatusCode.OK, v);
        }

        [NonAction]
        public float CalVote(int Post_id)
        {
            var p = db.Votes.Where(a => a.Post_id == Post_id).ToList();
            var count = p.Count;
            int total = 0;
            for (int i = 0; i < p.Count; i++)
            {
                total += p[i].vote1;
            }

            var f = total / count;
            float cal = f;
            return cal;
        }

        private bool VoteExists(int id)
        {
            return db.Votes.Where(e => e.Traveler_id == id) != null;
        }

        private bool PostExists(int id)
        {
            return db.Posts.Count(e => e.id == id) > 0;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
