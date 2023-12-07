using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bank.Application.Api.Contracts.Deposits;
using Bank.Application.AppServices.Contracts.Deposits.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Bank.Application.DataAccess.Deposits.Repository;

public class DepositRepository : IDepositRepository
{
    private readonly ApplicationDbContext _context;

    public DepositRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CreateDeposit(Deposit deposit)
    {

        var depositEntity = new DepositEntity
        {
            ClientId = deposit.ClientId,
            DepositAmount = deposit.DepositAmount,
            InterestRate = deposit.InterestRate,
            PeriodInMonth = deposit.Period
        };

        _context.Add(depositEntity);
        await _context.SaveChangesAsync();
    }

    public Task<Deposit[]> GetDepositsByClientId(long clientId)
    {
        var deposits = _context.Deposits.AsNoTracking().Where(entity => entity.ClientId == clientId).Select(entity => new Deposit
        {
            DepositId = entity.Id,
            ClientId = entity.ClientId,
            DepositAmount = entity.DepositAmount,
            Period = entity.PeriodInMonth,
            InterestRate = entity.InterestRate
        }).Where(deposit => deposit.ClientId == clientId).ToArrayAsync();

        return deposits;
    }
}