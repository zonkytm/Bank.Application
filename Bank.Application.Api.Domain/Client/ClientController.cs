using System.Text;
using Bank.Application.Api.Contracts.Clients.Requests;
using Bank.Application.Api.Contracts.Clients.Responses;
using Bank.Application.AppServices.Clients;
using Bank.Application.AppServices.Contracts.Client.Handlers;
using Bank.Application.AppServices.Contracts.Client.Infos;
using Bank.Application.AppServices.Contracts.Tokens;
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
    private readonly IUpdateClientHandler _updateClientHandler;

    public ClientController(ICreateClientHandler createClientHandler, ITokenService tokenService,
        IOptions<JwtSettings> jwtSettings, IGetClientByLoginHandler clientByLoginHandler, IUpdateClientHandler updateClientHandler)
    {
        _createClientHandler = createClientHandler;
        _tokenService = tokenService;
        _jwtSettings = jwtSettings;
        _clientByLoginHandler = clientByLoginHandler;
        _updateClientHandler = updateClientHandler;
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
            var userId = client.Id.ToString();
            var userName = loginRequest.Login;
            var secretKey = _jwtSettings.Value.SecretKey;
            var keyBytes = Encoding.UTF8.GetBytes(secretKey);
            var base64Key = Convert.ToBase64String(keyBytes);
            var jwtToken = _tokenService.GenerateJwtToken(userId, userName, secretKey);
            return new LoginResponse
            {
                ClientToken = jwtToken,
                Login = client.Login,
                Id = client.Id
            };
        }
        throw new Exception("Пароль не верный");
    }

    private string HashPassword(string password)
    {
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
        return hashedPassword;
    }

    [HttpGet("getClient")]
    public async Task<GetClientByLoginResponse> GetClientByLogin([FromQuery] string login)
    {
        var client = await _clientByLoginHandler.Handle(login);
        var response = new GetClientByLoginResponse
        {
            Login = client.Login,
            FirstName = client.FirstName,
            LastName = client.LastName,
            MiddleName = client.MiddleName,
            Salary = client.Salary
        };
        
        return response;
    }

    [HttpPut("updateClient")]
    public async Task<UpdateClientResponse> UpdateClient([FromBody] UpdateClientRequest request)
    {
        if (request == null)
        {
            throw new NullReferenceException("Запрос на обновление не может быть пустым");
        }

        var response = await _updateClientHandler.Handle(request);

        return response;
    }
}