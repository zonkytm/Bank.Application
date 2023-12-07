using Bank.Application.Api.Contracts.Clients.Requests;
using Bank.Application.Api.Contracts.Deposits.Requests;
using Bank.Application.AppServices.Clients;
using Bank.Application.AppServices.Contracts.Client.Handlers;
using Bank.Application.AppServices.Contracts.Client.Repositories;
using Bank.Application.AppServices.Contracts.Deposits.Handlers;
using Bank.Application.AppServices.Contracts.Deposits.Repositories;
using Bank.Application.AppServices.Deposits.Handlers;
using Bank.Application.DataAccess;
using Bank.Application.DataAccess.Clients.Repository;
using Bank.Application.DataAccess.Deposits.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bank.Application.AppServices;

public static class ServiceCollectionExtensions
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IDepositRepository, DepositRepository>();
        services.AddScoped<IClientRepository, ClientRepository>();
    }
    
    public static void AddContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));
    }

    public static void AddHandlers(this IServiceCollection services)
    {
        services.AddScoped<ICreateDepositHandler, CreateDepositHandler>();
        services.AddScoped<ICreateClientHandler, CreateClientHandler>();
        services.AddScoped<IGetClientDepositsHandler, GetClientDepositsHandler>();
        services.AddScoped<IGetClientByLoginHandler, GetClientByLoginHandler>();
        services.AddScoped<IUpdateClientHandler, UpdateClientHandler>();
    }
}