using Cometa.Api.Responses;
using Cometa.Application.UseCases.Users;
using Cometa.Infrastructure.Database.Repositories.InMemory;
using Microsoft.AspNetCore.Mvc;

namespace Cometa.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    /// <summary>
    /// Get all users.
    /// </summary>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(GetAllUsersResponses), StatusCodes.Status200OK)]
    public IActionResult GetAllUsers()
    {
        var userRepository = new InMemoryUserRepository();

        var getAllUsersUseCase = new GetAllUsersUseCase(userRepository);

        var users = getAllUsersUseCase.Execute();

        return Ok(
            new GetAllUsersResponses
            {
                users = users.Select(
                    user => new UserResponse
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Email = user.Email,
                        CreatedAt = user.CreatedAt
                    }
                ).ToList()
            }
        );
    }
}