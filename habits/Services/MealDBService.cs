using System.Net.Http;
using System.Threading.Tasks;

namespace habits.Services
{
    public class MealDBService
    {
        private readonly HttpClient _httpClient;

        public MealDBService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> SearchRecipeByName(string name)
        {
            var url = $"https://www.themealdb.com/api/json/v1/1/search.php?s={name}";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Error al llamar a TheMealDB API: {response.StatusCode}");

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetRecipesByCategory(string category)
        {
            var url = $"https://www.themealdb.com/api/json/v1/1/filter.php?c={category}";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Error al llamar a TheMealDB API: {response.StatusCode}");

            return await response.Content.ReadAsStringAsync();
        }
    }
}
