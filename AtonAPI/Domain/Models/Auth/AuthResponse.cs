using Domain.Entites;

namespace Domain.Models;

public class AuthResponse
{
    public Guid Id { get; set; }
    public string Login { get; set; }
    public string Name { get; set; }
    public int Gender { get; set; }
    public DateTime? Birthday { get; set; }
    public bool Admin { get; set; }
    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; }
    public DateTime ModifiedOn { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime? RevokedOn { get; set; }
    public string? RevokedBy { get; set; }
    public string Token { get; set; }
    
    public AuthResponse(User user, string token)
    {
        Id = user.Id;
        Login = user.Login;
        Name = user.Name;
        Gender = user.Gender;
        Birthday = user.Birthday;
        Admin = user.Admin;
        CreatedOn = user.CreatedOn;
        CreatedBy = user.CreatedBy;
        ModifiedOn = user.ModifiedOn;
        ModifiedBy = user.ModifiedBy;
        RevokedOn = user.RevokedOn;
        Token = token;
    }
}