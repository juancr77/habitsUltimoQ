using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

public class NutritionixService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;
    private readonly string _appId;
    private readonly string _apiKey;

    public NutritionixService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _baseUrl = configuration["APIs:Nutritionix:BaseUrl"];
        _appId = configuration["APIs:Nutritionix:AppId"];
        _apiKey = configuration["APIs:Nutritionix:ApiKey"];
    }

    public async Task<string> SearchFood(string query)
    {
        // Construir la URL completa
        var url = $"{_baseUrl}/search/instant?query={query}";

        // Crear solicitud HTTP
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Add("x-app-id", _appId);
        request.Headers.Add("x-app-key", _apiKey);

        // Enviar solicitud
        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Error al buscar información nutricional: {errorContent}");
        }

        // Retornar la respuesta como string
        return await response.Content.ReadAsStringAsync();
    }
}
