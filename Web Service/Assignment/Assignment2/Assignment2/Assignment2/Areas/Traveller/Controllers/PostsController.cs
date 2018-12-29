using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Assignment2.Areas.Traveller.Controllers
{
    [RoutePrefix("api/v1/guide")]
    public class PostsController : ApiController
    {
        Assignment2_ServicesContext db = new Assignment2_ServicesContext();
        UploaderController uploader = new UploaderController();

        [Route("posts/add")]
        [HttpPost]
        public HttpResponseMessage AddPosts(Post post, List<Tag> tag)
        {
            List<Tag> listTags = new List<Tag>();

            if (ModelState.IsValid)
            {
                post.createdAt = DateTime.Now;
                post.updatedAt = DateTime.Now;
                db.Posts.Add(post);
                db.SaveChanges();
            }

            listTags = tag;
            for (int i = 0; i < listTags.Count; i++)
            {
                db.Tags.Add(listTags[i]);
                db.SaveChanges();
            }
            
            uploader.SaveImageToDb(post.id);
            return null;
        }

        [Route("posts")]
        [HttpGet]
        public List<Post> GetAllPosts()
        {
            return db.Posts.ToList();
        }

        [Route("posts/{Traveler_id}")]
        [HttpGet]
        public List<Post> GetAllGuidePosts(int Traveler_id)
        {
            return db.Posts.Where(a => a.Traveler_id == Traveler_id).ToList();
        }

        [Route("posts/{id}")]
        [HttpGet]
        public HttpResponseMessage GetPostsById(int id)
        {
            Post post = db.Posts.Where(a => a.id == id).FirstOrDefault();
            if (post == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Post not found.");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, post);
            }
        }

        [Route("posts/edit/{id}")]
        [HttpPut]
        public HttpResponseMessage EditById(int id, Post post)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != post.id)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Can not edit post id.");
            }

            db.Entry(post).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Post not found.");
                }
                else
                {
                    throw;
                }
            }

            return Request.CreateErrorResponse(HttpStatusCode.OK, "Edit success.");
        }

        [Route("posts/delete/{id}")]
        [HttpDelete]
        public HttpResponseMessage DeleteById(int id)
        {
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Post not found.");
            }

            db.Posts.Remove(post);
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK, post);
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
