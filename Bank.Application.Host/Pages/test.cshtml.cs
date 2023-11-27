using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bank.Application.Host.Pages;

public class test : PageModel
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public test(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void OnGet()
    {
        var accessToken = _httpContextAccessor.HttpContext.Session.GetString("AccessToken");
        HttpContext.Request.Headers.TryAdd("Authorization", "Bearer " + accessToken);
    }
}