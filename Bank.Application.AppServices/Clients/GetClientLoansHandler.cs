using Bank.Application.DataAccess.Loans.Repository;

namespace Bank.Application.AppServices.Clients;

public class GetClientLoansHandler
{
    private readonly ILoanRepository _loanRepository;

    public GetClientLoansHandler(ILoanRepository loanRepository)
    {
        _loanRepository = loanRepository;
    }
}