using Cometa.Application.Dtos;
using Cometa.Domain.Entities;

namespace Cometa.Application.Repositories;

public interface UserRepository
{
    User Create(CreateUserDto dto);
    User[] FindMany();
    User? FindById(Guid id);
    User? FindByName(string name);
    User? FindByEmail(string email);
    bool Update(Guid id, UpdateUserDto dto);
    bool Delete(Guid id);
}