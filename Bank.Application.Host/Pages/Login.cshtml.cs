using Bank.Application.Api.Contracts.Clients.Requests;
using Bank.Application.Api.Contracts.Clients.Responses;
using Bank.Application.AppServices.ApiClient;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bank.Application.Host.Pages;

[AllowAnonymous]
public class Login : PageModel
{
    private readonly ApiClient _apiClient;
    private readonly IHttpContextAccessor _httpContextAccessor;

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
        var response = await _apiClient.PostAsync("/api/client/login", request);


        if (response.IsSuccessStatusCode)
        {
            var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
            _httpContextAccessor.HttpContext.Session.SetString("AccessToken", loginResponse.ClientToken);
            _httpContextAccessor.HttpContext.Session.SetString("Login", loginResponse.Login);
            _httpContextAccessor.HttpContext.Session.SetString("Id", loginResponse.Id.ToString());
            _httpContextAccessor.HttpContext.Response.Cookies.Append("AccessToken", loginResponse.ClientToken);
            return RedirectToPage("/Account"); // Перенаправление на другую страницу
        }

        return Page();
    }
}