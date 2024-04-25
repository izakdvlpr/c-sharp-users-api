using UserApi.Models;

namespace UserApi.Repositories;

public interface UserRepository
{
    User Create(User user);
    User[] FindMany();
    User? FindById(Guid id);
    User? FindByName(string name);
    User? FindByEmail(string email);
    User? Update(User user);
    void Delete(User user);
}