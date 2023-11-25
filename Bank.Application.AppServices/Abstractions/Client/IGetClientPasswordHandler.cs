namespace Bank.Application.AppServices.Abstractions.Client;

public interface IGetClientByLoginHandler
{
    Task<Api.Clients.Client> Handle(string login);
}