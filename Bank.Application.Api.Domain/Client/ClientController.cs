using System.Text;
using Bank.Application.Api.Clients.Requests;
using Bank.Application.Api.Clients.Responses;
using Bank.Application.AppServices.Abstractions.Client;
using Bank.Application.AppServices.Abstractions.Client.Infos;
using Bank.Application.AppServices.Abstractions.Tokens;
using Bank.Application.AppServices.Clients;
using Bank.Application.Host;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Bank.Application.Api.Domain.Client;

[Route("api/client")]
public class ClientController : Controller
{
    private readonly ICreateClientHandler _createClientHandler;
    private readonly IGetClientByLoginHandler _clientByLoginHandler;
    private readonly IOptions<JwtSettings> _jwtSettings;
    private readonly ITokenService _tokenService;

    public ClientController(ICreateClientHandler createClientHandler, ITokenService tokenService,
        IOptions<JwtSettings> jwtSettings, IGetClientByLoginHandler clientByLoginHandler)
    {
        _createClientHandler = createClientHandler;
        _tokenService = tokenService;
        _jwtSettings = jwtSettings;
        _clientByLoginHandler = clientByLoginHandler;
    }

    [HttpPost("register")]
    public async Task<CreateClientResponse> CreateClient([FromBody] RegisterClientRequest registerClientRequest)
    {
        if (registerClientRequest == null)
        {
            throw new NullReferenceException();
        }

        string hashedPassword = HashPassword(registerClientRequest.Password);

        var createClientInfo = new CreateClientInfo
        {
            Login = registerClientRequest.Login,
            HashedPassword = hashedPassword,
            FirstName = registerClientRequest.FirstName,
            LastName = registerClientRequest.LastName,
            MiddleName = registerClientRequest.MiddleName,
            Salary = registerClientRequest.Salary,
            BirthDate = registerClientRequest.BirthDate
        };


        var clientId = await _createClientHandler.Handle(createClientInfo);


        var createClientResponse = new CreateClientResponse
        {
            Id = clientId
        };

        return createClientResponse;
    }

    [HttpPost("login")]
    public async Task<LoginResponse> Login([FromBody] LoginRequest loginRequest)
    {
        var client = await _clientByLoginHandler.Handle(loginRequest.Login);
        var clientPassword = client.Password;


        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginRequest.Password, clientPassword);

        if (isPasswordValid)
        {
            var userId = client.id.ToString();
            var userName = loginRequest.Login;
            var secretKey = _jwtSettings.Value.SecretKey;
            var keyBytes = Encoding.UTF8.GetBytes(secretKey);
            var base64Key = Convert.ToBase64String(keyBytes);
            var jwtToken = _tokenService.GenerateJwtToken(userId, userName, base64Key);
            return new LoginResponse
            {
                ClientToken = jwtToken
            };
        }
        else
        {
            return new LoginResponse
            {
                ClientToken = null
            };
        }
    }

    private string HashPassword(string password)
    {
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
        return hashedPassword;
    }

    [HttpGet("getOne")]
    [Authorize]
    public long GetOne()
    {
        return 1;
    }
}