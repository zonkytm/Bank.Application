namespace Bank.Application.AppServices.ApiClient;

public interface IApiClient
{
    void SetBearerToken(string token);
    Task<T> GetAsync<T>(string endpoint);
    Task<HttpResponseMessage> PostAsync<T>(string endpoint, T data);
}