namespace Bank.Application.Api.Contracts.Deposits;

public class Deposit
{
    public long DepositId { get; set; }
    public long ClientId { get; set; }
    public decimal DepositAmount { get; set; }
    public int Period { get; set; }
    public decimal InterestRate { get; set; }
}