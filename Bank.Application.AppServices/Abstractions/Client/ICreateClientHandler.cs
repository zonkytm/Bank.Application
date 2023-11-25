using Bank.Application.AppServices.Abstractions.Client.Infos;

namespace Bank.Application.AppServices.Abstractions.Client;

public interface ICreateClientHandler
{
    Task<int> Handle(CreateClientInfo createClientRequest);
}