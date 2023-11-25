using Bank.Application.Api.Client.Requests;
using Bank.Application.Api.Clients.Requests;
using Bank.Application.AppServices.Abstractions.Client;
using Bank.Application.AppServices.Abstractions.Client.Infos;
using Bank.Application.DataAccess.Clients;
using Bank.Application.DataAccess.Clients.Repository;

namespace Bank.Application.AppServices.Clients;

public class CreateClientHandler : ICreateClientHandler
{
    private readonly IClientRepository _repository;

    public CreateClientHandler(IClientRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> Handle(CreateClientInfo createClientRequest)
    {
        if (createClientRequest == null)
        {
            throw new NullReferenceException();
        }

        var client = new ClientEntity
        {
            Login = createClientRequest.Login,
            Password = createClientRequest.HashedPassword,
            FirstName = createClientRequest.FirstName,
            LastName = createClientRequest.LastName,
            MiddleName = createClientRequest.MiddleName,
            Salary = createClientRequest.Salary,
            BirthDate = createClientRequest.BirthDate.Date
        };
        var id = await _repository.CreateClient(client);
        return id;
    }
}