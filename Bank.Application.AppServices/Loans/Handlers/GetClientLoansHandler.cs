using Bank.Application.Api.Contracts.Deposits;
using Bank.Application.AppServices.Contracts.Client.Repositories;
using Bank.Application.AppServices.Contracts.Deposits.Handlers;
using Bank.Application.AppServices.Contracts.Deposits.Repositories;

namespace Bank.Application.AppServices.Deposits.Handlers;

public class GetClientLoansHandler : IGetClientDepositsHandler
{
    private readonly IDepositRepository _depositRepository;
    private readonly IClientRepository _clientRepositoryrepository;

    public GetClientLoansHandler(IDepositRepository repository, IClientRepository clientRepositoryrepository)
    {
        _depositRepository = repository;
        _clientRepositoryrepository = clientRepositoryrepository;
    }

    public async Task<Deposit[]> Handle(string login)
    {
        var client = await _clientRepositoryrepository.GetClientByLogin(login);
        if (client == null)
        {
            throw new NullReferenceException("Клиент с таким логином не найден.");
        }
        
        var deposits = await _depositRepository.GetDepositsByClientId(client.Id);
        return deposits;
    }
}