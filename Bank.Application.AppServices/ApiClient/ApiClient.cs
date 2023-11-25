using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Http.Formatting;
using Bank.Application.Host;
using Microsoft.Extensions.Options;

namespace Bank.Application.AppServices.ApiClient;

public class ApiClient
{
    private readonly HttpClient _httpClient;
    private readonly string _url;

    public ApiClient(HttpClient httpClient, IOptions<ApiClientSettings> config)
    {
        _httpClient = httpClient;
        _url = config.Value.Url;
    }

    public void SetBearerToken(string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
    
    public async Task<T> GetAsync<T>(string endpoint)
    {
        var response = await _httpClient.GetAsync(endpoint);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsAsync<T>();
        }
        else
        {
            // Обработка ошибок, например, выброс исключения или возврат значения по умолчанию
            return default(T);
        }
    }

    public async Task<HttpResponseMessage> PostAsync<T>(string endpoint, T data)
    {
        var response = await _httpClient.PostAsJsonAsync(_url+endpoint, data);
        return response;
    }
}