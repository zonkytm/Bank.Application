using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bank.Application.Host.Pages;

[AllowAnonymous]
public class Cards : PageModel
{
    public void OnGet()
    {
        
    }
}