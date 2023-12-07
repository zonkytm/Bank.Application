using Bank.Application.Api.Contracts.Clients;
using Bank.Application.Api.Contracts.Clients.Requests;
using Bank.Application.AppServices.Contracts.Client.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Bank.Application.DataAccess.Clients.Repository;

public class ClientRepository : IClientRepository
{
    private ApplicationDbContext _context;

    public ClientRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> CreateClient(Client client)
    {
        var clientEntity = new ClientEntity
        {
            Login = client.Login,
            Password = client.Password,
            FirstName = client.FirstName,
            LastName = client.LastName,
            MiddleName = client.MiddleName,
            Salary = client.Salary,
            BirthDate = client.BirthDate
        };
        _context.Clients.Add(clientEntity);
        await _context.SaveChangesAsync();
        return client.Id;
    }
    
    public async Task<string> UpdateClient(UpdateClientRequest request)
    {
        var existingClient =
            await _context.Clients.FirstOrDefaultAsync(client => client.Id == request.Id);
        if (existingClient != null)
        {
            if (!string.IsNullOrEmpty(request.Login))
            {
                existingClient.Login = request.Login;
            }
            
            if (!string.IsNullOrEmpty(request.FirstName))
            {
                existingClient.FirstName = request.FirstName;
            }

            if (!string.IsNullOrEmpty(request.MiddleName))
            {
                existingClient.MiddleName = request.MiddleName;
            }

            if (!string.IsNullOrEmpty(request.LastName))
            {
                existingClient.LastName = request.LastName;
            }

            if (request.Salary > 0)
            {
                existingClient.Salary = request.Salary;
            }

            await _context.SaveChangesAsync();
        }
        else
        {
            throw new NullReferenceException("Клиент не найден");
        }

        return existingClient.Login;
    }
    

    public async Task<Client> GetClientByLogin(string login)
    {
       var clientEntity = await _context.Clients.AsNoTracking().FirstOrDefaultAsync(entity => entity.Login == login);

       if (clientEntity == null)
       {
           throw new NullReferenceException("Клиент не найден");
       }

       var client = new Client
       {
           Id = clientEntity.Id,
           Login = clientEntity.Login,
           Password = clientEntity.Password,
           FirstName = clientEntity.FirstName,
           LastName = clientEntity.LastName,
           MiddleName = clientEntity.MiddleName,
           Salary = clientEntity.Salary,
           BirthDate = clientEntity.BirthDate
       };

       return client;
    }
}