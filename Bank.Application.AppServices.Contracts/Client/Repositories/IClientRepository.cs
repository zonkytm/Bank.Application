using Bank.Application.Api.Contracts.Clients.Requests;

namespace Bank.Application.AppServices.Contracts.Client.Repositories;

public interface IClientRepository
{
    Task<int> CreateClient(Api.Contracts.Clients.Client client);

    Task<Api.Contracts.Clients.Client> GetClientByLogin(string login);

    Task<string> UpdateClient(UpdateClientRequest request);
}