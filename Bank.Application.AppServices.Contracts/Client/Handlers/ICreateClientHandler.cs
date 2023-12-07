using Bank.Application.AppServices.Contracts.Client.Infos;

namespace Bank.Application.AppServices.Contracts.Client.Handlers;

public interface ICreateClientHandler
{
    Task<int> Handle(CreateClientInfo createClientRequest);
}