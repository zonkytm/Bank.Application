using Bank.Application.Api.Contracts.Deposits;
using Bank.Application.Api.Contracts.Deposits.Requests;
using Bank.Application.AppServices.Contracts.Client.Repositories;
using Bank.Application.AppServices.Contracts.Deposits.Handlers;
using Bank.Application.AppServices.Contracts.Deposits.Repositories;

namespace Bank.Application.AppServices.Deposits.Handlers;

public class CreateCardHandler : ICreateDepositHandler
{

    private readonly IDepositRepository _depositRepository;
    private readonly IClientRepository _clientRepository;

    public CreateCardHandler(IDepositRepository repository, IClientRepository clientRepository)
    {
        _depositRepository = repository;
        _clientRepository = clientRepository;
    }

    public async Task Handle(CreateDepositRequest request)
    {
        //todo валидацию депозита минимальная и максимальная сумма.

        var client = await _clientRepository.GetClientByLogin(request.Login);
        
        var deposit = new Deposit
        {
            ClientId = client.Id,
            DepositAmount = request.DepositAmount,
            Period = request.Period
        };
        await _depositRepository.CreateDeposit(deposit);

        
    }
}