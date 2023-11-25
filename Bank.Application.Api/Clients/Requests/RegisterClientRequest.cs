namespace Bank.Application.Api.Clients.Requests;

public record RegisterClientRequest
{
    
    public string Login { get; init; }

    public string Password { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string? MiddleName { get; init; }
    public decimal Salary { get; init; }
    public DateTime BirthDate { get; init; }
}