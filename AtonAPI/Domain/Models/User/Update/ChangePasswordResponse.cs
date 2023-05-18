using Domain.Entites;

namespace Domain.Models;

public class ChangePasswordResponse : BaseResponse
{
    public ChangePasswordResponse()
    {
        
    }
    public User? User { get; set; }
}