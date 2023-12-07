namespace Bank.Application.Api.Contracts.Deposits.Requests;

public class CreateDepositRequest
{
    public string Login { get; init; }
    public decimal DepositAmount { get; init; }
    public int Period { get; init; }
}