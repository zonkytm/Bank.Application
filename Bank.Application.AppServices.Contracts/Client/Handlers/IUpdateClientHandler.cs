using Bank.Application.Api.Contracts.Clients.Requests;
using Bank.Application.Api.Contracts.Clients.Responses;

namespace Bank.Application.AppServices.Contracts.Client.Handlers;

public interface IUpdateClientHandler
{
    Task<UpdateClientResponse> Handle(UpdateClientRequest request);
}