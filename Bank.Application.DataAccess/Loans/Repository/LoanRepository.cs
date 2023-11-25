namespace Bank.Application.DataAccess.Loans.Repository;

public class LoanRepository : ILoanRepository
{
    private readonly ApplicationDbContext _context;

    public LoanRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    // public Task<Loan>

}