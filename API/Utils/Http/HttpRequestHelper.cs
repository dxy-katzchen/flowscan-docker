

using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Authentication;

namespace API.Utils.Http
{
    public class HttpRequestHelper
    {
        private readonly HttpClient _httpClient;
        private readonly string BaseURLDev = "http://3.106.127.111:5001";
        private readonly string BaseURLProd = "http://10.0.142.33:5001";

        public HttpRequestHelper(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> SendPostRequestAsync<T>(string apiUrl, T requestBody = default)
        {
            var jsonContent = requestBody != null
                ? new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json")
                : new StringContent(string.Empty, Encoding.UTF8, "application/json");

            var BaseURL = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development" ? BaseURLDev : BaseURLProd;

            var request = new HttpRequestMessage(HttpMethod.Post, $"{BaseURL}{apiUrl}")
            {
                Content = jsonContent
            };

            request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            if (requestBody != null)
            { request.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"); }

            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
        public static JsonObject ProcessStringToJsonObject(string jsonString)
        {
            try
            {
                JsonObject? jsonObject = JsonNode.Parse(jsonString) as JsonObject;
                if (jsonObject == null)
                {
                    throw new InvalidOperationException("Invalid JSON format.");
                }
                return jsonObject;
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException("Failed to parse JSON string.", ex);
            }
        }
    }
}