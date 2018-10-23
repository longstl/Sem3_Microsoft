using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp_Demo_Final
{
    public class APIManager
    {
        public static async Task<RootObject> GetWeather(double lat, double lon)
        {
            var http = new HttpClient();
            var url = string.Format("https://api.openweathermap.org/data/2.5/find?lat={0}&lon={1}&appid=1b28cadc2aeb855150f2f04414e39d5e&units=metric", lat, lon);
            var response = await http.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(RootObject));
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
            var data = (RootObject) serializer.ReadObject(ms);
            return data;
        }
    }

    [DataContract]
    public class Coord
    {
        [DataMember]
        public double lat { get; set; }
        [DataMember]
        public double lon { get; set; }
    }

    [DataContract]
    public class Main
    {
        [DataMember]
        public string temp { get; set; }
        [DataMember]
        public string pressure { get; set; }
        [DataMember]
        public string humidity { get; set; }
        [DataMember]
        public string temp_min { get; set; }
        [DataMember]
        public string temp_max { get; set; }
    }

    [DataContract]
    public class Wind
    {
        [DataMember]
        public string speed { get; set; }
    }

    [DataContract]
    public class Sys
    {
        [DataMember]
        public string country { get; set; }
    }

    [DataContract]
    public class Clouds
    {
        [DataMember]
        public string all { get; set; }
    }

    [DataContract]
    public class Weather
    {
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public string main { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public string icon { get; set; }
    }

    [DataContract]
    public class List
    {
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public Coord coord { get; set; }
        [DataMember]
        public Main main { get; set; }
        [DataMember]
        public int dt { get; set; }
        [DataMember]
        public Wind wind { get; set; }
        [DataMember]
        public Sys sys { get; set; }
        [DataMember]
        public object rain { get; set; }
        [DataMember]
        public object snow { get; set; }
        [DataMember]
        public Clouds clouds { get; set; }
        [DataMember]
        public List<Weather> weather { get; set; }
    }

    [DataContract]
    public class RootObject
    {
        [DataMember]
        public string message { get; set; }
        [DataMember]
        public string cod { get; set; }
        [DataMember]
        public string count { get; set; }
        [DataMember]
        public List<List> list { get; set; }
    }
}
