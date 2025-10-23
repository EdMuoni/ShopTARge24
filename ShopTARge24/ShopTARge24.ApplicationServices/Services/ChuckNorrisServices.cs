using System.Net.Http.Json;
using ShopTARge24.Core.Dto;
using ShopTARge24.Core.ServiceInterface;

namespace ShopTARge24.ApplicationServices.Services
{
    public class ChuckNorrisServices : IChuckNorrisServices
    {
        private readonly HttpClient _http;

        public ChuckNorrisServices(HttpClient http)
        {
            _http = http;
            _http.BaseAddress = new Uri("https://api.chucknorris.io/");
        }

        public async Task<ChuckNorrisViewModel> GetRandomAsync(string? category = null)
        {
            var endpoint = string.IsNullOrWhiteSpace(category)
                ? "jokes/random"
                : $"jokes/random?category={Uri.EscapeDataString(category)}";

            var json = await _http.GetFromJsonAsync<ApiJoke>(endpoint);
            if (json == null) throw new Exception("Failed to get joke.");

            return new ChuckNorrisViewModel
            {
                Id = json.id,
                IconUrl = json.icon_url,
                Url = json.url,
                Value = json.value,
                Categories = json.categories ?? new List<string>()
            };
        }

        public async Task<List<string>> GetCategoriesAsync()
        {
            var cats = await _http.GetFromJsonAsync<List<string>>("jokes/categories");
            return cats ?? new List<string>();
        }

        // Minimal DTO for API shape
        private class ApiJoke
        {
            public string id { get; set; } = "";
            public string url { get; set; } = "";
            public string icon_url { get; set; } = "";
            public string value { get; set; } = "";
            public List<string>? categories { get; set; }
        }
    }
}