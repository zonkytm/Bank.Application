namespace Bank.Application.Api.Contracts.Clients.Responses;

public class GetClientByLoginResponse
{
    public string Login { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public decimal Salary { get; set; }
}