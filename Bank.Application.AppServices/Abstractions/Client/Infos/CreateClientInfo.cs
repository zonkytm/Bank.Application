namespace Bank.Application.AppServices.Abstractions.Client.Infos;

public class CreateClientInfo
{
    public string Login { get; init; }

    public string HashedPassword { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string? MiddleName { get; init; }
    public decimal Salary { get; init; }
    public DateTime BirthDate { get; init; }
}