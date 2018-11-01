using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace App1
{
    public class YoutubeEntity
    {
        private static readonly string Baseurl = "http://youtube-video-api-1608.appspot.com/youtube/api";

        public static async Task<List<RootObject>> GetYoutube()
        {
            var http = new HttpClient();
            var response = await http.GetAsync(Baseurl);
            var result = await response.Content.ReadAsStringAsync();
            List<RootObject> objects = JsonConvert.DeserializeObject<List<RootObject>>(result);
            return objects;
        }
    }

    public class RootObject
    {
        public string videoId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string keywords { get; set; }
        public string category { get; set; }
        public string genre { get; set; }
        public string authorName { get; set; }
        public string authorEmail { get; set; }
        public string birthday { get; set; }
    }
}
