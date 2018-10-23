using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace App1_Demo_Api_Youtube.entity
{
    class SearchByKeyword
    {
        [DataContract]
        public class PageInfo
        {
            [DataMember]
            public int totalResults { get; set; }
            [DataMember]
            public int resultsPerPage { get; set; }
        }

        [DataContract]
        public class Id
        {
            [DataMember]
            public string kind { get; set; }
            [DataMember]
            public string videoId { get; set; }
            [DataMember]
            public string channelId { get; set; }
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
        public class Thumbnails
        {
            [DataMember]
            public Default @default { get; set; }
            [DataMember]
            public Medium medium { get; set; }
            [DataMember]
            public High high { get; set; }
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
            public string liveBroadcastContent { get; set; }
        }

        [DataContract]
        public class Item
        {
            [DataMember]
            public string kind { get; set; }
            [DataMember]
            public string etag { get; set; }
            [DataMember]
            public Id id { get; set; }
            [DataMember]
            public Snippet snippet { get; set; }
        }

        [DataContract]
        public class RootObject
        {
            [DataMember]
            public string kind { get; set; }
            [DataMember]
            public string etag { get; set; }
            [DataMember]
            public string nextPageToken { get; set; }
            [DataMember]
            public string regionCode { get; set; }
            [DataMember]
            public PageInfo pageInfo { get; set; }
            [DataMember]
            public List<Item> items { get; set; }
        }
    }
}
