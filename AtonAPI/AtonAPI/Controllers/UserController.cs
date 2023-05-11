using DataAccess.Repository;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Misc.UserService;

namespace AtonAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpPost("authenticate")]
    public IActionResult Authenticate(LoginModel model)
    {
        var response = _userService.Authenticate(model);

        if (response == null)
            return BadRequest(new { message = "Username or password is incorrect" });

        return Ok(response);
    }
    
    [Authorize]
    [HttpGet("GetAll")]
    public IActionResult GetAll()
    {
        var users = _userService.GetAll().Result;
        return Ok(users);
    }
}