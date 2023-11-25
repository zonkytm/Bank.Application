using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bank.Application.Host.Pages;

public class LogOut : PageModel
{
    public IActionResult OnPost()
    {

        HttpContext.Session.Remove("AccessToken");

        return RedirectToPage("/Login");
    }
}