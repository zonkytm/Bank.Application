using Bank.Application.Api.Contracts.Clients;
using Bank.Application.AppServices.Contracts.Client.Repositories;

namespace Bank.Application.AppServices.Clients;

public class GetClientByLoginHandler : IGetClientByLoginHandler
{
    private readonly IClientRepository _repository;

    public GetClientByLoginHandler(IClientRepository repository)
    {
        _repository = repository;
    }

    public async Task<Client> Handle(string login)
    {
        var client =  await _repository.GetClientByLogin(login);
        return client;
    }
}