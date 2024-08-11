using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WanderVibe.Services // or your preferred namespace
{
    public class ImageService
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;
        private readonly string _accessKey;

        public ImageService(IConfiguration configuration, IMemoryCache cache)
        {
            _httpClient = new HttpClient();
            _accessKey = configuration["Unsplash:AccessKey"];
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Client-ID {_accessKey}");
            _cache = cache;
        }

        public async Task<string> GetImageForService(string serviceName)
        {
            if (_cache.TryGetValue(serviceName, out string cachedImageUrl))
            {
                return cachedImageUrl;
            }

            try
            {
                var response = await _httpClient.GetAsync($"https://api.unsplash.com/search/photos?page=1&query={serviceName}&per_page=1");

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(jsonResponse);
                    string imageUrl = data.results[0]?.urls?.regular;

                    // Cache the result for future use
                    _cache.Set(serviceName, imageUrl, TimeSpan.FromDays(1));

                    return imageUrl;
                }
            }
            catch (Exception ex)
            {
                // Log the error (implement your logging mechanism)
                Console.WriteLine($"Error fetching image for {serviceName}: {ex.Message}");
            }

            // Return a default image URL if API fails
            return "https://images.unsplash.com/photo-1605152276897-4f618f831968?q=80&w=1770&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D";
        }
    }
}