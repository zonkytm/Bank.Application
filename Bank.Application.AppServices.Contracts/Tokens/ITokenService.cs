namespace Bank.Application.AppServices.Contracts.Tokens;

public interface ITokenService
{
    string GenerateJwtToken(string userId, string userName, string secretKey);
}