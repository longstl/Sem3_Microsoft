using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using App1_Demo_Api_Youtube.entity;

namespace App1_Demo_Api_Youtube.model
{
    class ApiHandle
    {
        private static readonly string ApiKey = "AIzaSyBVpqPYZvJUhdo8S4Xg_2GYB2wB2noxNe0";
        private static readonly string UserId = "UCgfMLQUWI7xmsRkIrfefNbw";

        private static readonly string PlayListUrl = string.Format("");
        private static readonly string PlayListItemUrl = string.Format("");

        private static readonly string VideoCategoriesUrl = string.Format("");
        private static readonly string VideoUrl = string.Format("");

        public static async Task<Account.RootObject> GetAccount()
        {
            var channelUrl = string.Format("https://www.googleapis.com/youtube/v3/channels?part=snippet%2CcontentDetails%2Cstatistics&id={0}&key={1}", UserId, ApiKey);
            var http = new HttpClient();
            var response = await http.GetAsync(channelUrl);
            var result = await response.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(Account.RootObject));
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
            var data = (Account.RootObject)serializer.ReadObject(ms);
            return data;
        }

        public static async Task<SearchByLocation.RootObject> SearchByLocation(double lat, double lon)
        {
            var searchByLocationUrl = string.Format("https://www.googleapis.com/youtube/v3/search?part=snippet&location={0}%2C{1}&locationRadius=10mi&q=surfing&type=video&key={2}", lat, lon, ApiKey);
            var http = new HttpClient();
            var response = await http.GetAsync(searchByLocationUrl);
            var result = await response.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(SearchByLocation.RootObject));
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
            var data = (SearchByLocation.RootObject)serializer.ReadObject(ms);
            return data;
        }

        public static async Task<SearchByKeyword.RootObject> SearchByKeyWord(string param)
        {
            var SearchUrl = string.Format("https://www.googleapis.com/youtube/v3/search?part=snippet&maxResults=25&q={0}&key={1}", param, ApiKey);
            var http = new HttpClient();
            var response = await http.GetAsync(SearchUrl);
            var result = await response.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(SearchByKeyword.RootObject));
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
            var data = (SearchByKeyword.RootObject)serializer.ReadObject(ms);
            return data;
        }

        public static async Task<SearchByKeyword.RootObject> GetContentHome()
        {
            var SearchUrl = string.Format("https://www.googleapis.com/youtube/v3/search?part=snippet&maxResults=25&q=&key={0}", ApiKey);
            var http = new HttpClient();
            var response = await http.GetAsync(SearchUrl);
            var result = await response.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(SearchByKeyword.RootObject));
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
            var data = (SearchByKeyword.RootObject)serializer.ReadObject(ms);
            return data;
        }
    }
}
