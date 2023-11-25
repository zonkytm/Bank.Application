using Bank.Application.AppServices.ApiClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bank.Application.Host.Pages;

public class Account : PageModel
{
    private readonly ApiClient _apiClient;
    private readonly IHttpContextAccessor  _httpContextAccessor;
    
    public Account(ApiClient apiClient, IHttpContextAccessor httpContextAccessor)
    {
        _apiClient = apiClient;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<IActionResult> OnGet()
    {
        var accessToken = HttpContext.Session.GetString("AccessToken");

        // Проверьте, что токен не пустой
        if (string.IsNullOrEmpty(accessToken))
        {
            // Редирект на страницу аутентификации или другое действие в случае отсутствия токена
            return RedirectToPage("/Login");
        }
        return Page();
    }
}