namespace Bank.Application.Api.Contracts.Clients.Responses;

public class LoginResponse
{
    public string ClientToken { get; set; }
    public string Login { get; set; }
    public long Id { get; set; }

}