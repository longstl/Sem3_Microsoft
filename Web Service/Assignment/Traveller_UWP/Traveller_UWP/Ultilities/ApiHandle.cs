using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Traveller_UWP.Entity;
using Image = Windows.UI.Xaml.Controls.Image;

namespace Traveller_UWP.Ultilities
{
    public class ApiHandle
    {
        private static readonly string BASE_URL = "http://localhost:27489/api/v1/";
        private static readonly string BASE_URL_TRAVELLER = "http://localhost:27489/api/v1/traveller/";
        private static readonly string BASE_URL_GUILD = "http://localhost:27489/api/v1/guide/";
        
        public static async Task<HttpResponseMessage> Register(Member traveller)
        {
            string url = BASE_URL_TRAVELLER + "register/";

            string jsonMember = JsonConvert.SerializeObject(traveller);
            HttpClient httpClient = new HttpClient();
            var content = new StringContent(jsonMember, Encoding.UTF8, "application/json");
            var response = httpClient.PostAsync(url, content);
            return response.Result;
        }

        public static async Task<HttpResponseMessage> Login(Member traveller)
        {
            string url = BASE_URL_TRAVELLER + "login/";

            string jsonMember = JsonConvert.SerializeObject(traveller);
            HttpClient httpClient = new HttpClient();
            var content = new StringContent(jsonMember, Encoding.UTF8, "application/json");
            var response = httpClient.PostAsync(url, content);
            return response.Result;
        }

        public static async Task<Member> GetCurUser(string email)
        {
            string url = BASE_URL + "GetCurrentUser?email=" + email;
            HttpClient httpClient = new HttpClient();
            string response = await httpClient.GetStringAsync(url);
            Member member = JsonConvert.DeserializeObject<Member>(response);
            return member;
        }

        public static async Task<HttpResponseMessage> AddPost(CreateParameters parameters)
        {
            string url = BASE_URL_GUILD + "posts/add";
            string jsonParam = JsonConvert.SerializeObject(parameters);
            HttpClient httpClient = new HttpClient();
            var content = new StringContent(jsonParam, Encoding.UTF8, "application/json");
            var response = httpClient.PostAsync(url, content);
            return response.Result;
        }

        public static async Task<List<Posts>> GetNews()
        {
            string url = BASE_URL_TRAVELLER + "posts";
            var http = new HttpClient();
            var response = await http.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            List<Posts> objects = JsonConvert.DeserializeObject<List<Posts>>(result);
            return objects;
        }
    }
}