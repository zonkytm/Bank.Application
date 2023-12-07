using Bank.Application.Api.Contracts.Loans.Requests;
using Bank.Application.Api.Contracts.Loans.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Application.Api.Domain.Loans;

[Route("/api/loans")]
public class LoanController : Controller
{
    // [HttpPost]
    // public Task<CreateLoanApplicationResponse> CreateLoanApplication([FromBody] CreateLoanApplicationRequest request)
    // {
    //     if (request == null)
    //     {
    //         throw new NullReferenceException("Заявка не может быть пустой");
    //     }
    //     
    //     
    // }
}