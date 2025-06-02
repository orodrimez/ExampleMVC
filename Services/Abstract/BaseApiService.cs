using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace EPractico_Optim.Services.Abstract;
public class BaseApiService
{
    protected readonly HttpClient _httpClient;
    public BaseApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    protected async Task<T?> GetFromApiAsync<T>(string endpoint)
    {
        var response = await _httpClient.GetAsync(endpoint);

        if (!response.IsSuccessStatusCode) return default;

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
    }
}
