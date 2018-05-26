using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace FaceRecognitionFrontEnd
{
    public class RestClient
    {
		
		public static string uri = "http://172.20.10.3:3100/api";
     
        static HttpClient httpClient = new HttpClient();
        public RestClient()
        {
            httpClient.MaxResponseContentBufferSize = 256000;
        }
      /// <summary>
		/// Prepares the content(body).
      /// </summary>
      /// <returns>The content.</returns>
      /// <param name="data">Data.</param>
        private static StringContent PrepareContent(Object data)
        {
            string json = JsonConvert.SerializeObject(data);
            return new StringContent(json.ToString(), Encoding.UTF8, "application/json");

        }
        /// <summary>
        /// Post the specified path and data.
        /// </summary>
        /// <returns>The post.</returns>
        /// <param name="path">Path.</param>
        /// <param name="data">Data.</param>
        public static async Task<HttpResponseMessage> Post(string path, Object data)
        {
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri(uri + path);
            request.Method = HttpMethod.Post;
            request.Content = PrepareContent(data);
            return await httpClient.SendAsync(request);
        }
        /// <summary>
        /// Get the specified path.
        /// </summary>
        /// <returns>The get.</returns>
        /// <param name="path">Path.</param>
        public static async Task<HttpResponseMessage> Get(string path)
        {
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri(uri + path);
            request.Method = HttpMethod.Get;
            var response = await httpClient.SendAsync(request);
            return response;
        }

    }
}
