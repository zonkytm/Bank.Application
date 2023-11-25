using Bank.Application.Api.Clients;
using Microsoft.EntityFrameworkCore;

namespace Bank.Application.DataAccess.Clients.Repository;

public class ClientRepository : IClientRepository
{
    private ApplicationDbContext _context;

    public ClientRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> CreateClient(ClientEntity client)
    { 
        _context.Clients.Add(client);
        await _context.SaveChangesAsync();
        return client.id;
    }

    public async Task<Client> GetClientByLogin(string login)
    {
       var clientEntity = await _context.Clients.FirstOrDefaultAsync(entity => entity.Login == login);

       if (clientEntity == null)
       {
           throw new NullReferenceException("Клиент не найден");
       }

       var client = new Client
       {
           id = clientEntity.id,
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