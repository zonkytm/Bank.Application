using Bank.Application.Api.Contracts.Clients;
using Bank.Application.Api.Contracts.Clients.Requests;
using Bank.Application.Api.Contracts.Clients.Responses;
using Bank.Application.AppServices.Contracts.Client.Handlers;
using Bank.Application.AppServices.Contracts.Client.Repositories;
using Bank.Application.DataAccess;

namespace Bank.Application.AppServices.Clients;

public class UpdateClientHandler : IUpdateClientHandler
{
    private readonly IClientRepository _repository;

    public UpdateClientHandler(IClientRepository repository)
    {
        _repository = repository;
    }

    public async Task<UpdateClientResponse> Handle(UpdateClientRequest request)
    {
        //todo валидация данных
        var login = await _repository.UpdateClient(request);
        var response = new UpdateClientResponse
        {
            Login = login
        };
        
        return response;
    }
}