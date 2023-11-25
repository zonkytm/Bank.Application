using Bank.Application.DataAccess.BankAccounts;
using Bank.Application.DataAccess.Cards;
using Bank.Application.DataAccess.Clients;
using Bank.Application.DataAccess.Deposits;
using Bank.Application.DataAccess.Loans;
using Microsoft.EntityFrameworkCore;

namespace Bank.Application.DataAccess;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    

    public DbSet<ClientEntity> Clients { get; set; }
    // public DbSet<LoanEntity> Loans { get; set; }
    // public DbSet<DepositEntity> Deposits { get; set; }
    // public DbSet<Card> Cards { get; set; }
    // public DbSet<BankAccount> BankAccounts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("banking");
    }
}