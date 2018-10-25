using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Load_Channel_Youtube.entity
{
    public class YoutubeProxy
    {
        public static async Task<RootObject> GetChannel()
        {
            var http = new HttpClient();
            var response = await http.GetAsync("https://www.googleapis.com/youtube/v3/activities?part=snippet%2CcontentDetails&channelId=UCgfMLQUWI7xmsRkIrfefNbw&maxResults=25&key=AIzaSyBVpqPYZvJUhdo8S4Xg_2GYB2wB2noxNe0");
            var result = await response.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(RootObject));

            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
            var data = (RootObject)serializer.ReadObject(ms);

            return data;
        }
    }

    [DataContract]
    public class PageInfo
    {
        [DataMember]
        public int totalResults { get; set; }
        [DataMember]
        public int resultsPerPage { get; set; }
    }

    [DataContract]
    public class Default
    {
        [DataMember]
        public string url { get; set; }
        [DataMember]
        public int width { get; set; }
        [DataMember]
        public int height { get; set; }
    }

    [DataContract]
    public class Medium
    {
        [DataMember]
        public string url { get; set; }
        [DataMember]
        public int width { get; set; }
        [DataMember]
        public int height { get; set; }
    }

    [DataContract]
    public class High
    {
        [DataMember]
        public string url { get; set; }
        [DataMember]
        public int width { get; set; }
        [DataMember]
        public int height { get; set; }
    }

    [DataContract]
    public class Standard
    {
        [DataMember]
        public string url { get; set; }
        [DataMember]
        public int width { get; set; }
        [DataMember]
        public int height { get; set; }
    }

    [DataContract]
    public class Maxres
    {
        [DataMember]
        public string url { get; set; }
        [DataMember]
        public int width { get; set; }
        [DataMember]
        public int height { get; set; }
    }

    [DataContract]
    public class Thumbnails
    {
        [DataMember]
        public Default @default { get; set; }
        [DataMember]
        public Medium medium { get; set; }
        [DataMember]
        public High high { get; set; }
        [DataMember]
        public Standard standard { get; set; }
        [DataMember]
        public Maxres maxres { get; set; }
    }

    [DataContract]
    public class Snippet
    {
        [DataMember]
        public string publishedAt { get; set; }
        [DataMember]
        public string channelId { get; set; }
        [DataMember]
        public string title { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public Thumbnails thumbnails { get; set; }
        [DataMember]
        public string channelTitle { get; set; }
        [DataMember]
        public string type { get; set; }
    }

    [DataContract]
    public class Upload
    {
        [DataMember]
        public string videoId { get; set; }
    }

    [DataContract]
    public class ContentDetails
    {
        [DataMember]
        public Upload upload { get; set; }
    }

    [DataContract]
    public class Item
    {
        [DataMember]
        public string kind { get; set; }
        [DataMember]
        public string etag { get; set; }
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public Snippet snippet { get; set; }
        [DataMember]
        public ContentDetails contentDetails { get; set; }
    }

    [DataContract]
    public class RootObject
    {
        [DataMember]
        public string kind { get; set; }
        [DataMember]
        public string etag { get; set; }
        [DataMember]
        public PageInfo pageInfo { get; set; }
        [DataMember]
        public List<Item> items { get; set; }
    }
}
