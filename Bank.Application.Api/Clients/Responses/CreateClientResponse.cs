namespace Bank.Application.Api.Clients.Responses;

public record CreateClientResponse
{
    public long Id { get; set; }
    public string Token { get; set; }
}