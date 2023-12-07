using Bank.Application.Api.Contracts.Deposits.Requests;
using Bank.Application.Api.Contracts.Deposits.Responses;

namespace Bank.Application.AppServices.Contracts.Deposits.Handlers;

public interface ICreateDepositHandler
{
    public Task Handle(CreateDepositRequest request);
}