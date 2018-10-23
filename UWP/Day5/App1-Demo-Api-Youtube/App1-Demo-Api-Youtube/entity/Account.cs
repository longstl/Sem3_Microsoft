using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace App1_Demo_Api_Youtube.entity
{
    class Account
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
        public class Localized
        {
            [DataMember]
            public string title { get; set; }
            [DataMember]
            public string description { get; set; }
        }

        [DataContract]
        public class Snippet
        {
            [DataMember]
            public string title { get; set; }
            [DataMember]
            public string description { get; set; }
            [DataMember]
            public string publishedAt { get; set; }
            [DataMember]
            public Thumbnails thumbnails { get; set; }
            [DataMember]
            public Localized localized { get; set; }
        }

        [DataContract]
        public class RelatedPlaylists
        {
            [DataMember]
            public string likes { get; set; }
            [DataMember]
            public string favorites { get; set; }
            [DataMember]
            public string uploads { get; set; }
            [DataMember]
            public string watchHistory { get; set; }
            [DataMember]
            public string watchLater { get; set; }
        }

        [DataContract]
        public class ContentDetails
        {
            [DataMember]
            public RelatedPlaylists relatedPlaylists { get; set; }
        }

        [DataContract]
        public class Statistics
        {
            [DataMember]
            public string viewCount { get; set; }
            [DataMember]
            public string commentCount { get; set; }
            [DataMember]
            public string subscriberCount { get; set; }
            [DataMember]
            public bool hiddenSubscriberCount { get; set; }
            [DataMember]
            public string videoCount { get; set; }
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
            [DataMember]
            public Statistics statistics { get; set; }
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
}
