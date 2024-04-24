using Cometa.Application.Repositories;
using Cometa.Domain.Entities;

namespace Cometa.Application.UseCases.Users;

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