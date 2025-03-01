using DrinksInfo.Models;
using Spectre.Console;
using System.Net.Http.Headers;
using System.Text.Json;


namespace DrinksInfo.Controllers
{
    public class DrinksController
    {
        private HttpClient _client;
        public DrinksController()
        {
            _client = new();
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<ICollection<T>> GetResponseList<T, K>(string url)
        {
           await using Stream stream =
           await _client.GetStreamAsync(url);

            var response =
                await JsonSerializer.DeserializeAsync<K>(stream);
            List<T> list = response.GetType()
                .GetProperties().
                Select(x => x.GetValue(response))
                .FirstOrDefault() as List<T> ?? new();
            return list;
        }
        public async Task<Drink> GetDrinkDetails(string drink)
        {
            await using Stream stream =
                await _client.GetStreamAsync($"https://www.thecocktaildb.com/api/json/v1/1/search.php?s={drink}");
            var response =
                await JsonSerializer.DeserializeAsync<Drink>(stream);
            return response ?? new();
        }

    }

}
