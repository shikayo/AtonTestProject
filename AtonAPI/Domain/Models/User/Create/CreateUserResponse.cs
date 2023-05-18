using Domain.Entites;

namespace Domain.Models;

public class CreateUserResponse : BaseResponse
{
    public User CreatedUser { get; set; }

    public CreateUserResponse(string message,bool isSuccess, User createdUser)
    {
        Message = message;
        IsSuccess = isSuccess;
        CreatedUser = createdUser;
    }
}