using UserApi.Models;

namespace UserApi.Responses;

public class GetAllUsersResponse
{
    public List<User> Users { get; set; }
}