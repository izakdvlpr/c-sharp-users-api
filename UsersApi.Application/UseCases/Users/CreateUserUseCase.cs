using UsersApi.Application.Dtos.User;
using UsersApi.Domain.Entities;
using UsersApi.Application.Repositories;

namespace UsersApi.Application.UseCases.Users;

public class CreateUserUseCase
{
    private readonly UserRepository _userRepository;

    public CreateUserUseCase(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public User Execute(string name, string email, string password)
    {
        var user = new User(name, email, password);
        
        var dto = new CreateUserDto
        {
            Name = user.Name,
            Email = user.Email,
            Password = user.Password
        };
        
        _userRepository.Create(dto);

        return user;
    }
}