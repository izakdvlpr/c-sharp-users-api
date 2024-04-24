using UserApi.Models;

namespace UserApi.Repositories;

public interface UserRepository
{
    User Create(User user);
    User[] FindMany();
    User? FindById(Guid id);
    User? FindByName(string name);
    User? FindByEmail(string email);
    bool Update(User user);
    bool Delete(Guid id);
}