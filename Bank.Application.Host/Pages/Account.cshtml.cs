using Bank.Application.AppServices.ApiClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bank.Application.Host.Pages;

[Authorize]
public class Account : PageModel
{
    private readonly ApiClient _apiClient;
    private readonly IHttpContextAccessor  _httpContextAccessor;
    
    public Account(ApiClient apiClient, IHttpContextAccessor httpContextAccessor)
    {
        _apiClient = apiClient;
        _httpContextAccessor = httpContextAccessor;
    }

    public void OnGet()
    {
    }
    
    
}