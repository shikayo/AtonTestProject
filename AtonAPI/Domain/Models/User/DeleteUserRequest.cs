namespace Domain.Models;

public class DeleteUserRequest
{
    public string UserLogin { get; set; }
    public bool IsSoftDelete { get; set; }
    public string Revoker { get; set; }
}