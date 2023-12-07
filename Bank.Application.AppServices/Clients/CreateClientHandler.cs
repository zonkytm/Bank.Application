using Bank.Application.Api.Contracts.Clients;
using Bank.Application.AppServices.Contracts.Client.Handlers;
using Bank.Application.AppServices.Contracts.Client.Infos;
using Bank.Application.AppServices.Contracts.Client.Repositories;

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

        //todo валидация реквеста по параметрам (клиенту больше 18 лет, зарплата больше 0)
        var client = new Client
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