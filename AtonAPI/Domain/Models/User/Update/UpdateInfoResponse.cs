using Domain.Entites;

namespace Domain.Models;

public class UpdateInfoResponse : BaseResponse
{
    public UpdateInfoResponse()
    {
        
    }
    public User? UpdatedUser { get; set; }
}