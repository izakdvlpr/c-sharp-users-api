using UserApi.Models;

namespace UserApi.Responses;

public class GetAllUsersResponses
{
    public List<User> Users { get; set; }
}