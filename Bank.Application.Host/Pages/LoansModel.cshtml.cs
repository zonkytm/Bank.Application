using Bank.Application.Api.Loans;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bank.Application.Host.Pages;

public class LoansModel : PageModel
{
    public List<Loan> Loans { get; set; } = new List<Loan>();
    public void OnGet([FromQuery]long clientId)
    {
        
    }
}