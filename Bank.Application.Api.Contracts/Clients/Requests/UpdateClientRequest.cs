namespace Bank.Application.Api.Contracts.Clients.Requests;

public class UpdateClientRequest
{
    public long Id { get; set; }
    public string? Login { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? MiddleName { get; set; }
    public decimal Salary { get; set; }
    
}