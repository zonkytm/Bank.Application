using Bank.Application.Api.Contracts.Deposits;

namespace Bank.Application.AppServices.Contracts.Deposits.Handlers;

public interface IGetClientDepositsHandler
{
    public Task<Deposit[]> Handle(string login);
}