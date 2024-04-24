using Cometa.Application.Dtos;
using Cometa.Application.Repositories;
using Cometa.Domain.Entities;

namespace Cometa.Infrastructure.Database.Repositories.InMemory;

public class InMemoryUserRepository : UserRepository
{
    private static readonly List<User> _users = [];
    
    public User Create(CreateUserDto dto)
    {
        var user = new User(dto.Name, dto.Email, dto.Password);
        
        _users.Add(user);
        
        return user;
    }
    
    public User[] FindMany()
    {
        return _users.ToArray();
    }
    
    public User? FindById(Guid id)
    {
        return _users.Find(user => user.Id == id);
    }
    
    public User? FindByName(string name)
    {
        return _users.Find(user => user.Name == name);
    }
    
    public User? FindByEmail(string email)
    {
        return _users.Find(user => user.Email == email);
    }
    
    public bool Update(Guid id, UpdateUserDto dto)
    {
        var user = _users.Find(user => user.Id == id);
        
        if (user == null)
        {
            return false;
        }
        
        user.Name = dto.Name;
        user.Email = dto.Email;
        user.Password = dto.Password;
        
        return true;
    }
    
    public bool Delete(Guid id)
    {
        var user = _users.Find(user => user.Id == id);
        
        if (user == null)
        {
            return false;
        }
        
        _users.Remove(user);
        
        return true;
    }
}