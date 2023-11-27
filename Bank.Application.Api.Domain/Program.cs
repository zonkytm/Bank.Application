using System.Text;
using Bank.Application.Api.Domain;
using Bank.Application.Api.Domain.Client;
using Bank.Application.AppServices.Abstractions.Client;
using Bank.Application.AppServices.Abstractions.Tokens;
using Bank.Application.AppServices.ApiClient;
using Bank.Application.AppServices.Clients;
using Bank.Application.AppServices.Tokens;
using Bank.Application.DataAccess;
using Bank.Application.DataAccess.Clients.Repository;
using Bank.Application.Host;
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
builder.Services.AddScoped<IClientRepository,ClientRepository>();
builder.Services.AddScoped<ICreateClientHandler,CreateClientHandler>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IGetClientByLoginHandler, GetClientByLoginHandler>();
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

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(
       connectionString));
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.Run();