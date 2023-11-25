using Bank.Application.Api.Clients;

namespace Bank.Application.DataAccess.Clients.Repository;

public interface IClientRepository
{
    Task<int> CreateClient(ClientEntity client);

    Task<Client> GetClientByLogin(string login);
}