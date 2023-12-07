namespace Bank.Application.Api.Contracts.Loans.Responses;

public class CreateLoanApplicationResponse
{
    public long LoanApplicationId { get; set; }
    public long ClientId { get; set; }
    public string Status { get; set; }
    public decimal LoanAmount { get; set; }
    public int LoanTermInMonth { get; set; }
}