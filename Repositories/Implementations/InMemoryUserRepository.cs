using UserApi.Models;

namespace UserApi.Repositories.Implementations;

public class InMemoryUserRepository : UserRepository
{
    private static readonly List<User> _users = [];
    
    public User Create(User user)
    {
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
    
    public bool Update(User user)
    {
        var _user = _users.Find(__user => __user.Id == user.Id);
        
        if (_user == null)
        {
            return false;
        }
        
        _user.Name = user.Name;
        _user.Email = user.Email;
        _user.Password = user.Password;
        
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