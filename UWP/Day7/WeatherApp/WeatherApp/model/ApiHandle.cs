using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.entity;

namespace WeatherApp.model
{
    class ApiHandle
    {
        private const string BaseUrl = "https://api.openweathermap.org/data/2.5/";
        private const string ApiKey = "98bb0e6881d8f402f21bf1a3eeb97a17";
        private const string UnitsParam = "metric";

        public static async Task<OpenWeatherMap1day.RootObject> GetWeatherOneDay(double lat, double lon)
        {
            var oneDayUrl = BaseUrl + string.Format("weather?lat={0}&lon={1}&appid={2}&units={3}", lat, lon, ApiKey, UnitsParam);
            var http = new HttpClient();
            var response = await http.GetAsync(oneDayUrl);
            var result = await response.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(OpenWeatherMap1day.RootObject));
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
            var data = (OpenWeatherMap1day.RootObject)serializer.ReadObject(ms);
            return data;
        }

        public static async Task<OpenWeatherMap5days.RootObject> GetWeatherFiveDay(double lat, double lon)
        {
            var oneDayUrl = BaseUrl + $"forecast?lat={lat}&lon={lon}&appid={ApiKey}&units={UnitsParam}";
            var http = new HttpClient();
            var response = await http.GetAsync(oneDayUrl);
            var result = await response.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(OpenWeatherMap5days.RootObject));
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
            var data = (OpenWeatherMap5days.RootObject)serializer.ReadObject(ms);
            return data;
        }
    }
}
