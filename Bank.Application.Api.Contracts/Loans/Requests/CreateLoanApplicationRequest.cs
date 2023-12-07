namespace Bank.Application.Api.Contracts.Loans.Requests;

public class CreateLoanApplicationRequest
{
    public long  ClientId { get; init;}
    public decimal LoanAmount { get; init;}
    public decimal LoanTermInMonth { get; init;}
}