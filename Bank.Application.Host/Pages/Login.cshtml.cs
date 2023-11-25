using Bank.Application.Api.Clients.Requests;
using Bank.Application.Api.Clients.Responses;
using Bank.Application.AppServices.ApiClient;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bank.Application.Host.Pages;

public class Login : PageModel
{

    private readonly ApiClient _apiClient;
    private readonly IHttpContextAccessor  _httpContextAccessor;

    public Login(ApiClient apiClient, IHttpContextAccessor httpContextAccessor)
    {
        _apiClient = apiClient;
        _httpContextAccessor = httpContextAccessor;
    }

    public void OnGet()
    {
        
    }

    public async Task<IActionResult> OnPostLogin()
    {
        var request = new LoginRequest
        {
            Login = Request.Form["login"],
            Password = Request.Form["password"]
        };
        var response = (await _apiClient.PostAsync("/api/client/login", request));

        var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
        
        if (response.IsSuccessStatusCode)
        {
            
            _httpContextAccessor.HttpContext.Session.SetString("AccessToken", loginResponse.ClientToken);
            return RedirectToPage("/Account"); // Перенаправление на другую страницу
        }
        else
        {
            return Page();
        }
    }
}