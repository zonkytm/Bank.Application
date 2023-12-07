using Bank.Application.Api.Contracts.Clients;
using Bank.Application.Api.Contracts.Clients.Requests;
using Bank.Application.Api.Contracts.Clients.Responses;
using Bank.Application.Api.Contracts.Deposits.Responses;
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
    public ClientInformation ClientInformation;
    
    public Account(ApiClient apiClient, IHttpContextAccessor httpContextAccessor)
    {
        _apiClient = apiClient;
        _httpContextAccessor = httpContextAccessor;
        ClientInformation = new ClientInformation();
    }

    public async Task OnGet()
    {
        var login = _httpContextAccessor.HttpContext.Session.GetString("Login");
        var urlQuery = $"?login={login}";
        var response  = await _apiClient.GetAsync<GetClientByLoginResponse>("/api/client/getClient"+urlQuery);

        if (response != null)
        {
            ClientInformation.Login = response.Login;
            ClientInformation.FirstName = response.FirstName;
            ClientInformation.LastName = response.LastName;
            ClientInformation.MiddleName = response.MiddleName;
            ClientInformation.Salary = response.Salary;
        }
        else
        {
            TempData["ErrorMessage"] = "Информация о клиенте найдена";
        }
        
    }

    public async Task<IActionResult> OnPostUpdateClient()
    {
        var updateClientRequest = new UpdateClientRequest();
        updateClientRequest.Id = long.Parse(_httpContextAccessor.HttpContext.Session.GetString("Id"));
        updateClientRequest.Login = Request.Form["login"].ToString();
        updateClientRequest.FirstName = Request.Form["firstName"].ToString();
        updateClientRequest.LastName = Request.Form["lastName"].ToString();
        updateClientRequest.MiddleName = Request.Form["middleName"].ToString();
        updateClientRequest.Salary = string.IsNullOrEmpty(Request.Form["salary"].ToString())? 0: decimal.Parse(Request.Form["salary"].ToString());


        var response = await _apiClient.UpdateAsync("/api/client/updateClient", updateClientRequest);
    
        
        if (response.IsSuccessStatusCode)
        {
            var updateResponse = await response.Content.ReadFromJsonAsync<UpdateClientResponse>();
            _httpContextAccessor.HttpContext.Session.SetString("Login",updateResponse.Login);
            return RedirectToPage("Account");
        }
        else
        {
            TempData["ErrorUpdateMessage"] = "Не удалось обновить данные клиента";
            return Page();
        }
    }
    
    
}