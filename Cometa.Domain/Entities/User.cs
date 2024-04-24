namespace Cometa.Domain.Entities;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    public User(string name, string email, string password)
    {
        Name = name;
        Email = email;
        Password = password;
    }
}