using Microsoft.AspNetCore.Mvc;
using UserApi.Models;
using UserApi.Repositories;
using UserApi.Repositories.Implementations;
using UserApi.Requests.Users;
using UserApi.Responses;
using UserApi.Responses.Common;

namespace UserApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserRepository _userRepository = new InMemoryUserRepository();
    
    /// <summary>
    /// Create a new user.
    /// </summary>
    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    public IActionResult CreateUser([FromBody] CreateUserRequest request)
    {
        var user = new User(request.Name, request.Email, request.Password);
        
        if (_userRepository.FindByEmail(user.Email) != null)
        {
            var error = new Error
            {
                Message = "User already exists",
                Code = "UserAlreadyExists",
                Status = StatusCodes.Status400BadRequest
            };
            
            Response.StatusCode = error.Status;

            return new JsonResult(new { error });
        }
        
        _userRepository.Create(user);

        return Created(
            "",
            new CreateUserResponse
            {
                User = new UserResponse
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    CreatedAt = user.CreatedAt
                }
            }
        );
    }
    
    /// <summary>
    /// Get all users.
    /// </summary>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(GetAllUsersResponses), StatusCodes.Status200OK)]
    public IActionResult GetAllUsers()
    {
        var users = _userRepository.FindMany();

        return Ok(
            new GetAllUsersResponses
            {
                Users = users.Select(
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
    
    /// <summary>
    /// Get a user by id.
    /// </summary>
    [HttpGet("{userId}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    public IActionResult GetUserById([FromRoute] Guid userId)
    {
        var user = _userRepository.FindById(userId);

        if (user == null)
        {
            var error = new Error
            {
                Message = "User not found",
                Code = "UserNotFound",
                Status = StatusCodes.Status404NotFound
            };
            
            Response.StatusCode = error.Status;

            return new JsonResult(new { error });
        }

        return Ok(
            new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                CreatedAt = user.CreatedAt
            }
        );
    }
    
    /// <summary>
    /// Update a user by id.
    /// </summary>
    [HttpPut("{userId}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    public IActionResult UpdateUserById([FromRoute] Guid userId, [FromBody] UpdateUserRequest request)
    {
        var user = _userRepository.FindById(userId);

        if (user == null)
        {
            var error = new Error
            {
                Message = "User not found",
                Code = "UserNotFound",
                Status = StatusCodes.Status404NotFound
            };
            
            Response.StatusCode = error.Status;

            return new JsonResult(new { error });
        }
        
        if (request.Name != null) user.Name = request.Name;
        if (request.Email != null) user.Email = request.Email;
        if (request.Password != null) user.Password = request.Password;

        _userRepository.Update(user);

        return Ok(
            new UpdateUserByIdResponse
            {
                User = new UserResponse
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    CreatedAt = user.CreatedAt
                }
            }
        );
    }
    
    /// <summary>
    /// Delete a user by id.
    /// </summary>
    [HttpDelete("{userId}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    public IActionResult DeleteUserById([FromRoute] Guid userId)
    {
        var user = _userRepository.FindById(userId);

        if (user == null)
        {
            var error = new Error
            {
                Message = "User not found",
                Code = "UserNotFound",
                Status = StatusCodes.Status404NotFound
            };
            
            Response.StatusCode = error.Status;

            return new JsonResult(new { error });
        }

        _userRepository.Delete(userId);

        return Ok(
            new DeleteUserByIdResponse
            {
                User = new UserResponse
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    CreatedAt = user.CreatedAt
                }
            }
        );
    }
}