namespace DefaultNamespace;

public class CreateUserUseCase
{
    private readonly UserRepository _userRepository;
    
    public CreateUserUseCase(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }
}