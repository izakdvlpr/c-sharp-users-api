using System.ComponentModel.DataAnnotations;

namespace UserApi.Requests.Users;

public class CreateUserRequest
{
    [StringLength(255)]
    public required string Name { get; set; }
    
    [EmailAddress]
    public required string Email { get; set; }
    
    [StringLength(255)]
    public required string Password { get; set; }
}