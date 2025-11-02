using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ShopTARge24.Core.Dto;
using ShopTARge24.Core.ServiceInterface;

namespace ShopTARge24.ApplicationServices.Services
{
    public class TheCocktailServices : ITheCocktailServices
    {
        private readonly HttpClient _http;

        private class SearchResponse
        {
            [JsonProperty("drinks")]
            public List<TheCocktailDto>? Drinks { get; set; }
        }

        public TheCocktailServices(HttpClient http)
        {
            _http = http;
            _http.BaseAddress = new Uri("https://www.thecocktaildb.com/api/json/v1/1/");
        }

        public async Task<List<TheCocktailDto>> SearchByNameAsync(string name)
        {
            var url = $"search.php?s={WebUtility.UrlEncode(name)}";
            var json = await _http.GetStringAsync(url);
            var data = JsonConvert.DeserializeObject<SearchResponse>(json);
            return data?.Drinks ?? new List<TheCocktailDto>();
        }


        // Gets many drinks based on a name
        public async Task<TheCocktailDto?> LookupByIdAsync(string idDrink)
        {
            var url = $"lookup.php?i={WebUtility.UrlEncode(idDrink)}";
            var json = await _http.GetStringAsync(url);
            var data = JsonConvert.DeserializeObject<SearchResponse>(json);
            return data?.Drinks.FirstOrDefault();
        }
    }
}
