using Bank.Application.Api.Contracts.Clients;

namespace Bank.Application.AppServices.Clients;

public interface IGetClientByLoginHandler
{
    Task<Client> Handle(string login);
}