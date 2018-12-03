using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace PhotoUpload.Areas.Customer.Controllers
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

    [Route("api/customer/upload")]
    public class UploadController : ApiController
    {
        private const string BASE_URL = "http://localhost:51541/Upload/Images/";
        PhotoUpload.Customer currentCus = CurrentCustomerController.cus;
        List<PhotoUpload.Image> listImages = new List<PhotoUpload.Image>();
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
                        string url = BASE_URL + info.Name;
                        Image image = new Image(currentCus.id, url, info.Name, info.Name);
                        listImages.Add(image);
                        for (int x = 0; x < listImages.Count; x++)
                        {
                            //System.Diagnostics.Debug.WriteLine(listImages[x].url);
                            using (AssignmentContext db = new AssignmentContext())
                            {
                                db.Images.Add(listImages[x]);
                                db.SaveChanges();
                            }
                        }
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
    }
}
