namespace Domain.Models;

public class ActivateUserRequest : BaseRequest
{
    public ActivateUserRequest()
    {
        
    }

    public ActivateUserRequest(string userLogin)
    {
        UserLogin = userLogin;
    }
}