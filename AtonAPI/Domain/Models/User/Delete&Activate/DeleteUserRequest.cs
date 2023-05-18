using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class DeleteUserRequest : BaseRequest
{
    public DeleteUserRequest()
    {
        
    }

    public DeleteUserRequest(string userLogin, bool isSoftDelete)
    {
        UserLogin = userLogin;
        IsSoftDelete = isSoftDelete;
    }
    [Required]
    public bool IsSoftDelete { get; set; }
}