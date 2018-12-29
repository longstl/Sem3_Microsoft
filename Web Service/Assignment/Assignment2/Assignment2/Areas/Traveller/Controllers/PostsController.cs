using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Assignment2.Areas.Traveller.Models;

namespace Assignment2.Areas.Traveller.Controllers
{
    [RoutePrefix("api/v1")]
    public class PostsController : ApiController
    {
        Assignment2_ServicesContext db = new Assignment2_ServicesContext();
        List<Tag> listTags = new List<Tag>();
        List<Tag> listCurrentTags = new List<Tag>();

        [Route("guide/posts/add")]
        [HttpPost]
        public HttpResponseMessage AddPosts([FromBody] CreateParameters parameters)
        {

            if (ModelState.IsValid)
            {
                parameters.Post.createdAt = DateTime.Now;
                parameters.Post.updatedAt = DateTime.Now;
                db.Posts.Add(parameters.Post);
                db.SaveChanges();
            }

            listTags = parameters.Tag;
            for (int i = 0; i < listTags.Count; i++)
            {
                if (!TagExists(listTags[i].tag_name))
                {
                    db.Tags.Add(listTags[i]);
                    db.SaveChanges();
                }
            }

            var currentPost = db.Posts.Where(a => a.Traveler_id == parameters.Post.Traveler_id).OrderByDescending(b => b.id).FirstOrDefault();

            for (int z = 0; z < listTags.Count; z++)
            {
                var tagName = listTags[z].tag_name;
                var listTag = db.Tags.Where(t => t.tag_name == tagName).OrderByDescending(b => b.id).FirstOrDefault();
                Debug.WriteLine(listTag.id);
                listCurrentTags.Add(listTag);
            }

            Tag_Post tp = new Tag_Post();
            for (int m = 0; m < listCurrentTags.Count; m++)
            {
                tp.Post_id = currentPost.id;
                tp.Tag_id = listCurrentTags[m].id;
                db.Tag_Post.Add(tp);
                db.SaveChanges();
            }


            return Request.CreateResponse(HttpStatusCode.OK, currentPost);
        }

        [Route("traveller/posts")]
        [HttpGet]
        public List<Post> GetAllPosts()
        {
            return db.Posts.ToList();
        }

        [Route("guide/posts/{Traveler_id}")]
        [HttpGet]
        public List<Post> GetAllGuidePosts(int Traveler_id)
        {
            List<Image> listImg = new List<Image>();
            var listPost = db.Posts.Where(a => a.Traveler_id == Traveler_id).ToList();
            //for (int i = 0; i < listPost.Count; i++)
            //{
            //    var resultImg = db.Images.Where(b => b.Post_id == listPost[i].id).FirstOrDefault();
            //    listImg.Add(resultImg);
            //    for (int j = 0; j < listImg.Count; j++)
            //    {
            //        if (listPost[i].id.CompareTo(listImg[j].Post_id) == 0)
            //        {
            //            listPost[i].Images.Add(listImg[j].);
            //        }
            //    }
            //}
            
            return listPost;
        }

        [Route("traveller/posts/{id}")]
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

        [Route("guide/posts/edit/{id}")]
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

        [Route("guide/posts/delete/{id}")]
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


        private bool TagExists(string tagName)
        {
            return db.Tags.Count(e => e.tag_name == tagName) > 0;
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
