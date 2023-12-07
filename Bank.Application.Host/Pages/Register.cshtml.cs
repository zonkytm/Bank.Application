using Bank.Application.Api.Contracts.Clients.Requests;
using Bank.Application.AppServices.ApiClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Bank.Application.Host.Pages;

[AllowAnonymous]
public class Register : PageModel
{

    private readonly ApiClient _apiClient;


    public Register(ApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public void OnGet()
    {
        
    }

    public async Task<IActionResult> OnPostRegister()
    {
        var request = new RegisterClientRequest
        {
            Login = Request.Form["userName"],
            Password = Request.Form["password"],
            FirstName = Request.Form["firstName"],
            LastName = Request.Form["lastName"],
            MiddleName = Request.Form["middleName"],
            Salary = Convert.ToDecimal(Request.Form["salary"]),
            BirthDate = DateTime.SpecifyKind(Convert.ToDateTime(Request.Form["birthDate"]), DateTimeKind.Utc)
        };

        var response = await _apiClient.PostAsync("/api/client/register", request);

        if (response.IsSuccessStatusCode)
        {
            // Действия при успешном вызове API
            return RedirectToPage("/login"); // Перенаправление на другую страницу
        }
        else
        {
            // Обработка ошибки
            return Page();
        }
    }


}