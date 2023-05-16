using System.Net;
using System.Security.Claims;
using AutoMapper;
using DataAccess.Repository;
using Domain.Entites;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Misc.UserService;

namespace AtonAPI.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IUserRepository _userRepository;
    public UserController(IUserService userService, IUserRepository userRepository)
    {
        _userService = userService;
        _userRepository = userRepository;
    }
    private User GetCurrentUser()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        var id = Guid.Parse(identity.FindFirst(x=>x.Type=="id").Value);
        return _userService.GetById(id).Result;
    }
    
    //create user
    [HttpPost("CreateUser")]
    public IActionResult CreateUser(CreateUserModel model)
    {
        var currentUser = GetCurrentUser(); 
        if (currentUser.Admin)
        {
            var user = _userRepository.GetUserByLoginAsync(model.Login).Result;
            if (user == null) {
                
                if (ModelState.IsValid)
                {
                    var identity = HttpContext.User.Identity as ClaimsIdentity;
                    var id = Guid.Parse(identity.FindFirst(x=>x.Type=="id").Value);
                    var creator = _userService.GetById(id).Result;
                    
                    _userService.CreateUser(model,creator);
                    
                    return Ok(user);
                }
                else
                {
                    return BadRequest(new { message = "fill all required fields" });
                }
            }

            return BadRequest(new { message = "user with this login already exists" });
        }

        return BadRequest(new { message = "you must be a admin to create new users" });
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var currentUser = GetCurrentUser();
        if (currentUser.Admin)
        {
            var response = await _userRepository.GetAllSortedByCreation();

            if (response.Count != 0)
            {
                return Ok(response);
            }

            return BadRequest(new { message = "no users" });
        }

        return BadRequest(new { nessage = "rejected, you must be a admin" });
    }

    [HttpGet("GetByLogin/{login}")]
    public async Task<IActionResult> GetUserByLogin([FromRoute] string login)
    {
        var currentUser = GetCurrentUser();
        if (currentUser.Admin || currentUser.Login == login)
        {
            var response = await _userRepository.GetUserByLoginAsync(login);

            if (response != null)
            {
                return Ok(response);
            }

            return BadRequest(new { message = "no user found" });
        }

        return BadRequest(new { message = "rejected" });
    }

    [HttpGet("GetByLoginAndPassword/{login}&{password}")]
    public async Task<IActionResult> GetUserByLoginAndPassword([FromRoute]string login,[FromRoute]string password)
    {
        var currentUser = GetCurrentUser();
        if (currentUser.Login == login && BCrypt.Net.BCrypt.Verify(password, currentUser.Password))
        {
            var response = await _userRepository.GetUserByLoginAsync(login);
            
            if (response != null)
            {
                return Ok(response);
            }

            return BadRequest(new { message = "no user found" });
        }
        
        return BadRequest(new { message = "rejected" });
    }

    [HttpGet("GetSortedByAge/{age}")]
    public async Task<IActionResult> GetUsersByAge([FromRoute] int age)
    {
        var currentUser = GetCurrentUser();
        if (currentUser.Admin)
        {
            var response = await _userRepository.GetUsersByAge(age);

            if (response.Count != 0)
            {
                return Ok(response);
            }

            return BadRequest(new { message = "no users found" });
        }
        
        return BadRequest(new { message = "rejected" });
    }

    [HttpDelete("DeleteUser")]
    public async Task<IActionResult> DeleteUser([FromBody] DeleteUserRequest request)
    {
        var currentUser = GetCurrentUser();
        request.Revoker = currentUser.Login;

        var result =await _userService.DeleteUser(request);

        if (result.IsSuccess)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }
}