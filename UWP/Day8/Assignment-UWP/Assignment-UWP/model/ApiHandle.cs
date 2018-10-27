using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Assignment_UWP.entity;
using Newtonsoft.Json;

namespace Assignment_UWP.model
{
    class ApiHandle
    {
        private static readonly string Baseurl = "http://tommyprivateguide.com/wp-json/wp/v2/posts?param%2Fdata.json&fbclid=IwAR3Gdy6G4t2rGOnPWNtCmcLcjxZZjYzIn4ttNuOB3srnixSUHZYcZMc4-2Q";

        public static async Task<List<NewsEntity.RootObject>> GetNews()
        {
            var http = new HttpClient();
            var response = await http.GetAsync(Baseurl);
            var result = await response.Content.ReadAsStringAsync();
            List<NewsEntity.RootObject> objects = JsonConvert.DeserializeObject<List<NewsEntity.RootObject>>(result);
            return objects;
        }
    }
}
