using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace FaceRecognitionFrontEnd
{
    public class RestClient
    {
        public static string uri = "http://localhost:3200/api";
        static HttpClient httpClient = new HttpClient();
        public RestClient()
        {
            httpClient.MaxResponseContentBufferSize = 256000;
        }
        private static StringContent PrepareContent(Object data)
        {
            string json = JsonConvert.SerializeObject(data);
            return new StringContent(json.ToString(), Encoding.UTF8, "application/json");

        }
        public static async Task<HttpResponseMessage> Post(string path, Object data)
        {
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri(uri + path);
            request.Method = HttpMethod.Post;
            request.Content = PrepareContent(data);
            return await httpClient.SendAsync(request);
        }
    }
}
