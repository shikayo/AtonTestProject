using Domain.Entites;

namespace Domain.Models;

public class UpdateUserLoginResponse : BaseResponse
{
    public UpdateUserLoginResponse()
    {
        
    }
    public User? User { get; set; }
}