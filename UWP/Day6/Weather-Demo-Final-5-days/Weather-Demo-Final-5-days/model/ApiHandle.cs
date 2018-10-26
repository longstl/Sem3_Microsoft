using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Weather_Demo_Final_5_days.entity;

namespace Weather_Demo_Final_5_days.model
{
    class ApiHandle
    {
        private static readonly string ApiKey = "98bb0e6881d8f402f21bf1a3eeb97a17";
        private static readonly string UnitParam = "metric";

        public static async Task<OpenWeatherMap.RootObject> GetWeather(double lat, double lon)
        {
            var getWeatherUrl = string.Format("https://api.openweathermap.org/data/2.5/forecast?lat={0}&lon={1}&appid={2}&units={3}", lat, lon, ApiKey, UnitParam);
            var http = new HttpClient();
            var response = await http.GetAsync(getWeatherUrl);
            var result = await response.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(OpenWeatherMap.RootObject));
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
            var data = (OpenWeatherMap.RootObject)serializer.ReadObject(ms);
            return data;
        }

        public static async Task<Geoposition> GetPosition()
        {
            var accessStatus = await Geolocator.RequestAccessAsync();
            if (accessStatus != GeolocationAccessStatus.Allowed) throw new Exception();
            var geolocator = new Geolocator { DesiredAccuracyInMeters = 0 };
            var postion = await geolocator.GetGeopositionAsync();
            return postion;
        }
    }
}
