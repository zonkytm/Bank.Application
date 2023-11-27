using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bank.Application.Host.Pages;

[Authorize]
public class About : PageModel
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public About(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void OnGet()
    {
        var accessToken = _httpContextAccessor.HttpContext.Session.GetString("AccessToken");
        HttpContext.Request.Headers.TryAdd("Authorization", "Bearer " + accessToken);
    }

 
}