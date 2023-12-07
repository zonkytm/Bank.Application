using System.Text;
using Bank.Application.Api.Domain;
using Bank.Application.Api.Domain.Client;
using Bank.Application.Api.Domain.CustomMiddlewares;
using Bank.Application.Api.Domain.Deposits;
using Bank.Application.AppServices;
using Bank.Application.AppServices.ApiClient;
using Bank.Application.AppServices.Clients;
using Bank.Application.AppServices.Contracts.ApiClient;
using Bank.Application.AppServices.Contracts.Client.Handlers;
using Bank.Application.AppServices.Contracts.Client.Repositories;
using Bank.Application.AppServices.Contracts.Tokens;
using Bank.Application.AppServices.Tokens;
using Bank.Application.DataAccess;
using Bank.Application.DataAccess.Clients.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .Build();
builder.Services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
builder.Services.Configure<ApiClientSettings>(configuration.GetSection("ApiClientSettings"));
builder.Services.AddScoped <ClientController>();
builder.Services.AddScoped<DepositController>();
builder.Services.AddRepositories();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddHandlers();
builder.Services.AddHttpClient();

builder.Services.AddScoped<ApiClient>();
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var issuer = jwtSettings["Issuer"];
var audience = jwtSettings["Audience"];
var secretKey = jwtSettings["SecretKey"];

builder.Services.AddAuthentication("Bearer")  // схема аутентификации - с помощью jwt-токенов
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = false,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
        };
    });    
builder.Services.AddAuthorization();
builder.Services.AddContext(configuration);
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseRouting();
app.UseMiddleware<ApiExceptionMiddleware>();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.Run();