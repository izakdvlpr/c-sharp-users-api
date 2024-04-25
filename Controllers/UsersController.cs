using Microsoft.AspNetCore.Mvc;
using UserApi.Bases;
using UserApi.Models;
using UserApi.Repositories;
using UserApi.Repositories.Implementations;
using UserApi.Requests.Users;
using UserApi.Responses;
using UserApi.Responses.Common;
using UserApi.Utils;

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
    [ProducesResponseType(typeof(CreateUserResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public IActionResult CreateUser([FromBody] CreateUserRequest request)
    {
        var user = new User(
            new UserProps(
                request.Name,
                request.Email,
                new Password().HashPassword(request.Password)
            )
        );

        if (_userRepository.FindByEmail(user.Email) != null)
        {
            var error = new Error
            {
                Message = "User already exists",
                Code = "UserAlreadyExists",
                Status = StatusCodes.Status400BadRequest
            };

            Response.StatusCode = error.Status;

            return new JsonResult(error);
        }

        _userRepository.Create(user);

        return Created(
            "",
            new CreateUserResponse
            {
                User = new User(
                    new UserProps(
                        user.Name,
                        user.Email,
                        user.Password,
                        user.CreatedAt,
                        user.UpdatedAt
                    ),
                    user.Id
                )
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
                    user => new User(
                        new UserProps(
                            user.Name,
                            user.Email,
                            user.Password,
                            user.CreatedAt,
                            user.UpdatedAt
                        ),
                        user.Id
                    )
                ).ToList()
            }
        );
    }

    /// <summary>
    /// Get a user by id.
    /// </summary>
    [HttpGet("{userId}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(GetUserByIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public IActionResult GetUserById([FromRoute] Guid userId)
    {
        var user = _userRepository.FindById(userId);

        if (user == null)
        {
            var error = new ErrorResponse
            {
                Error = new Error
                {
                    Message = "User not found",
                    Code = "UserNotFound",
                    Status = StatusCodes.Status404NotFound
                }
            };

            Response.StatusCode = error.Error.Status;

            return new JsonResult(error);
        }

        return Ok(
            new GetUserByIdResponse
            {
                User = new User(
                    new UserProps(
                        user.Name,
                        user.Email,
                        user.Password,
                        user.CreatedAt,
                        user.UpdatedAt
                    ),
                    user.Id
                )
            }
        );
    }

    /// <summary>
    /// Update a user by id.
    /// </summary>
    [HttpPatch("{userId}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(UpdateUserByIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public IActionResult UpdateUserById([FromRoute] Guid userId, [FromBody] UpdateUserRequest? request)
    {
        var user = _userRepository.FindById(userId);

        if (user == null)
        {
            var error = new ErrorResponse
            {
                Error = new Error
                {
                    Message = "User not found",
                    Code = "UserNotFound",
                    Status = StatusCodes.Status404NotFound
                }
            };

            Response.StatusCode = error.Error. Status;

            return new JsonResult(error);
        }

        user.Name = request?.Name ?? user.Name;
        user.Email = request?.Email ?? user.Email;
        user.Password = request?.Password ?? user.Password;
        user.UpdatedAt = DateTime.Now;

        _userRepository.Update(user);

        return Ok(
            new UpdateUserByIdResponse
            {
                User = new User(
                    new UserProps(
                        user.Name,
                        user.Email,
                        user.Password,
                        user.CreatedAt,
                        user.UpdatedAt
                    ),
                    user.Id
                )
            }
        );
    }

    /// <summary>
    /// Delete a user by id.
    /// </summary>
    [HttpDelete("{userId}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(DeleteUserByIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public IActionResult DeleteUserById([FromRoute] Guid userId)
    {
        var user = _userRepository.FindById(userId);

        if (user == null)
        {
            var error = new ErrorResponse
            {
                Error = new Error
                {
                    Message = "User not found",
                    Code = "UserNotFound",
                    Status = StatusCodes.Status404NotFound
                }
            };

            Response.StatusCode = error.Error.Status;

            return new JsonResult(error);
        }

        _userRepository.Delete(user);

        return Ok(
            new DeleteUserByIdResponse
            {
                UserId = user.Id
            }
        );
    }
}