using Microsoft.AspNetCore.Mvc;
using UsersApi.Application.Repositories;
using UsersApi.Application.UseCases.Users;
using UsersApi.UsersApi.Infrastructure.Database.Repositories.InMemory;

namespace UsersApi.Core.Http.Controllers;

[ApiController]
[Route("users")]
public class UsersControllers : ControllerBase
{
    private readonly UserRepository _userRepository = new InMemoryUserRepository();
    
    [HttpGet]
    public IActionResult GetAllUsers()
    {
        var getAllUsersUseCase = new GetAllUsersUseCase(_userRepository);
        
        var users = getAllUsersUseCase.Execute();
        
        return Ok(new { users });
    }
}