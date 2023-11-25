namespace Bank.Application.AppServices.Abstractions.Tokens;

public interface ITokenService
{
    string GenerateJwtToken(string userId, string userName, string secretKey);
}