using Bank.Application.Api.Contracts.Deposits;
using Bank.Application.Api.Contracts.Deposits.Requests;
using Bank.Application.Api.Contracts.Deposits.Responses;
using Bank.Application.AppServices.ApiClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bank.Application.Host.Pages;

[Authorize]
public class Deposits : PageModel
{
    private readonly ApiClient _apiClient;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public Deposit[] DepositsList { get; set; }

    public Deposits(ApiClient apiClient, IHttpContextAccessor httpContextAccessor)
    {
        _apiClient = apiClient;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task OnGet()
    {
        var login = _httpContextAccessor.HttpContext.Session.GetString("Login");
        var urlQuery = $"?login={login}";
        var response  = await _apiClient.GetAsync<GetClientDepositsResponse>("/api/deposit/getClientDeposits"+urlQuery);
        DepositsList = response?.ClientDeposits ?? Array.Empty<Deposit>();

        if (DepositsList.Length == 0)
        {
            TempData["ErrorMessage"] = "Депозиты не найдены";
        }
    }

    public async Task<IActionResult> OnPostDeposits()
    {
        var request = new CreateDepositRequest
        {
            Login = _httpContextAccessor.HttpContext.Session.GetString("Login"),
            DepositAmount = Decimal.Parse(Request.Form["amount"].ToString()),
            Period = int.Parse(Request.Form["period"].ToString())
        };
        
        var response = await _apiClient.PostAsync("/api/deposit/create", request);

        if (response.IsSuccessStatusCode)
        {
            TempData["SuccessMessage"] = "Депозит успешно оформлен.";
        }
        else
        {
            TempData["ErrorMessage"] = "Не удалось оформить депозит. Пожалуйста, попробуйте снова.";
        }
        
        return RedirectToPage("Deposits");
    }
}