using Bank.Application.Api.Contracts.Loans;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bank.Application.Host.Pages;

public class Loans : PageModel
{
    public List<Loan> LoansList { get; set; } = new List<Loan>();
    public void OnGet()
    {
        
    }
}