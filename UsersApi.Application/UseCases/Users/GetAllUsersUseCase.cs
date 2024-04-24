using UsersApi.Application.Repositories;
using UsersApi.Domain.Entities;

namespace UsersApi.Application.UseCases.Users;

public class GetAllUsersUseCase
{
    private readonly UserRepository _userRepository;
    
    public GetAllUsersUseCase(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public User[] Execute()
    {
        return _userRepository.FindMany();
    }
}