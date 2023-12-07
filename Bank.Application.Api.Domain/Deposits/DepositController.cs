using Bank.Application.Api.Contracts.Deposits;
using Bank.Application.Api.Contracts.Deposits.Requests;
using Bank.Application.Api.Contracts.Deposits.Responses;
using Bank.Application.AppServices.Contracts.Deposits.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Application.Api.Domain.Deposits;

[Route("api/deposit")]
public class DepositController : Controller
{
    private readonly ICreateDepositHandler _createDepositHandler;
    private readonly IGetClientDepositsHandler _clientDepositsHandler;

    public DepositController(ICreateDepositHandler createDepositHandler, IGetClientDepositsHandler clientDepositsHandler)
    {
        _createDepositHandler = createDepositHandler;
        _clientDepositsHandler = clientDepositsHandler;
    }

    [HttpPost("create")]
    public async Task CreateDeposit([FromBody]CreateDepositRequest depositRequest)
    {
        if (depositRequest == null)
        {
            throw new NullReferenceException("Заявка на депозит не может быть null");
        }

        await _createDepositHandler.Handle(depositRequest);
    }

    [HttpGet("getClientDeposits")]
    public async Task<GetClientDepositsResponse> GetClientDepositsById([FromQuery]string login)
    {
        if (login == null)
        {
            throw new NullReferenceException("Логин клиента не может быть равен null");
        }

        var deposits = await _clientDepositsHandler.Handle(login);

        var response = new GetClientDepositsResponse
        {
            ClientDeposits = deposits
        };
        
        return response;
    }
}