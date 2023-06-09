﻿using System.Net;
using System.Security.Claims;
using AtonAPI.Services.UserService;
using AutoMapper;
using DataAccess.Repository;
using Domain.Entites;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AtonAPI.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
public class UserController : BaseController
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    /// <summary>
    ///  Add user to system
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("CreateUser")]
    public async Task<IActionResult> CreateUser(CreateUserModel model)
    {
        var currentUser = GetCurrentUser(); 
        if (currentUser.Admin)
        {
            var user = await _userService.GetByLogin(model.Login);
            if (user == null) 
            {
                
                if (ModelState.IsValid)
                {
                    var creator = GetCurrentUser();
                    
                    var response =await _userService.CreateUser(model,creator);
                    
                    return Ok(response);
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

    /// <summary>
    /// Finds all not revoked users
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var currentUser = GetCurrentUser();
        if (currentUser.Admin)
        {
            var response = await _userService.GetAllByCreation();

            if (response.Count != 0)
            {
                return Ok(response);
            }

            return BadRequest(new { message = "no users" });
        }

        return BadRequest(new { nessage = "rejected, you must be a admin" });
    }
    

    /// <summary>
    /// enter login
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    [HttpGet("GetByLogin/{login}")]
    public async Task<IActionResult> GetUserByLogin([FromRoute] string login)
    {
        var currentUser = GetCurrentUser();
        if (currentUser.Admin || currentUser.Login == login)
        {
            var response = await _userService.GetByLogin(login);

            if (response != null)
            {
                return Ok(response);
            }

            return BadRequest(new { message = "no user found" });
        }

        return BadRequest(new { message = "rejected" });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="login"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    [HttpGet("GetByLoginAndPassword/{login}&{password}")]
    public async Task<IActionResult> GetUserByLoginAndPassword([FromRoute]string login,[FromRoute]string password)
    {
        var currentUser = GetCurrentUser();
        if (currentUser.Login == login && BCrypt.Net.BCrypt.Verify(password, currentUser.Password))
        {
            var response = await _userService.GetByLogin(login);
            
            if (response != null)
            {
                return Ok(response);
            }

            return BadRequest(new { message = "no user found" });
        }
        
        return BadRequest(new { message = "rejected" });
    }

    /// <summary>
    /// enter age and find users <age
    /// </summary>
    /// <param name="age"></param>
    /// <returns></returns>
    [HttpGet("GetSortedByAge/{age}")]
    public async Task<IActionResult> GetUsersByAge([FromRoute] int age)
    {
        var currentUser = GetCurrentUser();
        if (currentUser.Admin)
        {
            var response = await _userService.GetAllByAge(age);

            if (response.Count != 0)
            {
                return Ok(response);
            }

            return BadRequest(new { message = "no users found" });
        }
        
        return BadRequest(new { message = "rejected" });
    }

    /// <summary>
    /// updating user info(name,birthday etc)
    /// </summary>
    /// <param name="login"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPut("UpdateUserInfo/{login}")]
    public async Task<IActionResult> UpdateUserInfo([FromRoute] string login,UpdateUserInfoModel model)
    {
        var currentUser = GetCurrentUser();

        if (currentUser.Admin || currentUser.Login == login)
        {
            var response=await _userService.UpdateUserInfo(login,model,currentUser.Login);

            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response);
        }

        return BadRequest(new { message = "rejected" });
    }

    /// <summary>
    /// changing user password
    /// enter old password then new password
    /// </summary>
    /// <param name="login"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPut("ChangePassword/{login}")]
    public async Task<IActionResult> ChangePassword([FromRoute] string login, ChangePasswordModel model)
    {
        var currentUser = GetCurrentUser();
        if (currentUser.Admin || currentUser.Login == login)
        {
            var response=await _userService.ChangeUserPassword(login,model,currentUser.Login);

            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response);
        }

        return BadRequest(new { message = "rejected" });
    }

    /// <summary>
    /// change user login, new login must be unique
    /// </summary>
    /// <param name="login"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPut("ChangeUserLogin/{login}")]
    public async Task<IActionResult> ChangeLogin([FromRoute] string login,UpdateUserLoginModel model)
    {
        var currentUser = GetCurrentUser();

        if (currentUser.Admin || currentUser.Login == login)
        {
            var response=await _userService.UpdateUserLogin(login,model,currentUser.Login);

            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response);
        }

        return BadRequest(new { message = "rejected" });
    }

    /// <summary>
    /// Delete by login
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpDelete("DeleteUser")]
    public async Task<IActionResult> DeleteUser([FromBody] DeleteUserRequest request) 
    {
        var currentUser = GetCurrentUser();

        if (request.UserLogin == currentUser.Login)
        {
            return BadRequest(new { message = "you can't delete yourself xddddd" });
        }
        
        if (currentUser.Admin)
        {
            var result = await _userService.DeleteUser(request, currentUser.Login);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        return BadRequest(new { message = "you must be an admin" });
    }

    /// <summary>
    /// Activate by login
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("ActivateUser")]
    public async Task<IActionResult> ActivateUser([FromBody] ActivateUserRequest request)
    {
        var currentUser = GetCurrentUser();
        if (currentUser.Admin)
        {
            var result = await _userService.ActivateUser(request, currentUser.Login);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        return BadRequest(new { message = "you must be an admin" });
    }
    
    private User GetCurrentUser()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        var id = Guid.Parse(identity.FindFirst(x=>x.Type=="id").Value);
        return _userService.GetById(id).Result;
    }
}