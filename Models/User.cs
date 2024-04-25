using System.Text.Json.Serialization;

namespace UserApi.Models;

public class UserProps
{
    public string Name { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public UserProps(string name, string email, string password, DateTime? createdAt = null, DateTime? updatedAt = null)
    {
        Name = name;
        Email = email;
        Password = password;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;

    }
}

public class User
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    [JsonIgnore]
    public string Password { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
    
    public User(UserProps props, Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
        Name = props.Name;
        Email = props.Email;
        Password = props.Password;
        CreatedAt = props.CreatedAt ?? DateTime.Now;
        UpdatedAt = props.UpdatedAt ?? DateTime.Now;
    }
}