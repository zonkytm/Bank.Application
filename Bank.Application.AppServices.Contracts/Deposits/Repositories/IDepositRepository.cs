using Bank.Application.Api.Contracts.Deposits;

namespace Bank.Application.AppServices.Contracts.Deposits.Repositories;

public interface IDepositRepository
{
    public Task CreateDeposit(Deposit deposit);

    public Task<Deposit[]> GetDepositsByClientId(long clientId);
}