using AtonAPI.Services.UserService;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace AtonAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    public AuthController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpPost("Authenticate")]
    public IActionResult Authenticate(LoginModel model)
    {
        var response = _userService.Authenticate(model);

        if (response == null)
            return BadRequest(new { message = "Username or password is incorrect" });

        return Ok(response);
    }
}