using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.WebHost;
using System.Web.Routing;
using System.Web.SessionState;
using Newtonsoft.Json;

namespace Assignment2.Areas.Traveller.Controllers
{
    #region Multipart form provider class
    public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        public CustomMultipartFormDataStreamProvider(string path)
            : base(path)
        {

        }

        //below only allows images and pdf files to be uploaded.
        public override Stream GetStream(System.Net.Http.HttpContent parent, System.Net.Http.Headers.HttpContentHeaders headers)
        {

            // following line handles other form fields other than files.
            if (String.IsNullOrEmpty(headers.ContentDisposition.FileName)) return base.GetStream(parent, headers);

            // restrict what filetypes can be uploaded
            List<string> extensions = new List<string> { "png", "gif",
                "jpg", "jpeg", "tiff", "pdf", "tif", "bmp","doc","docx","ods","xls","odt","csv","txt","rtf" };
            var filename = headers.ContentDisposition.FileName.Replace("\"", string.Empty); // correct for chrome.

            //make sure it has an extension
            if (filename.IndexOf('.') < 0)
            {
                return Stream.Null;
            }

            //get the extension
            var extension = filename.Split('.').Last();

            //Return stream if match otherwise return null stream.
            return extensions.Contains(extension) ? base.GetStream(parent, headers) : Stream.Null;

        }

        public override string GetLocalFileName(System.Net.Http.Headers.HttpContentHeaders headers)
        {
            var name = !string.IsNullOrWhiteSpace(headers.ContentDisposition.FileName) ? headers.ContentDisposition.FileName : "NoName";
            name = name.Replace("\"", string.Empty);
            //name = (Guid.NewGuid()).ToString() +System.IO.Path.GetExtension(name); //this is here because Chrome submits files in quotation marks which get treated as part of the filename and get escaped

            name = System.IO.Path.GetRandomFileName().Replace(".", string.Empty) + System.IO.Path.GetExtension(name); //this is here because Chrome submits files in quotation marks which get treated as part of the filename and get escaped

            return name;
        }
    }
    #endregion

    [RoutePrefix("api/v1/guide")]
    public class UploaderController : ApiController
    {
        private const string BASE_URL = "http://localhost:27489/Upload/Images/";

        List<Image> listImages = new List<Image>();

        Assignment2_ServicesContext db = new Assignment2_ServicesContext();

        [Route("upload")]
        public Task<IEnumerable<string>> Post()
        {
            //throw new Exception("Custom error thrown for script error handling test!");

            if (Request.Content.IsMimeMultipartContent())
            {
                //Simulate large file upload
                System.Threading.Thread.Sleep(5000);


                string fullPath = HttpContext.Current.Server.MapPath("~/Upload/Images/");
                CustomMultipartFormDataStreamProvider streamProvider = new CustomMultipartFormDataStreamProvider(fullPath);
                var task = Request.Content.ReadAsMultipartAsync(streamProvider).ContinueWith(t =>
                {
                    if (t.IsFaulted || t.IsCanceled)
                        throw new HttpResponseException(HttpStatusCode.InternalServerError);

                    var fileInfo = streamProvider.FileData.Select(i =>
                    {
                        var info = new FileInfo(i.LocalFileName);
                        var url = BASE_URL + info.Name;
                        var image = new Image(url, info.Name, info.Name,DateTime.Now,DateTime.Now);
                        listImages.Add(image);
                        return url;
                    });
                    return fileInfo;

                });
                return task;
            }
            else
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable, "Invalid Request!"));
            }
        }

        [NonAction]
        public void SaveImageToDb(int postId)
        {
            var image = new Image();
            for (int i = 0; i < listImages.Count; i++)
            {
                image.Post_id = postId;
                image.name = listImages[i].name;
                image.description = listImages[i].description;
                image.url = listImages[i].url;
                image.createdAt = listImages[i].createdAt;
                image.updatedAt = listImages[i].updatedAt;
                db.Images.Add(image);
                db.SaveChanges();
            }
        }
    }
}