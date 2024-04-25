using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace UserApi.Requests.Users;

public class UpdateUserRequest
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
}