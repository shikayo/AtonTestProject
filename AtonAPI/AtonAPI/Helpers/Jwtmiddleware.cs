﻿using System.IdentityModel.Tokens.Jwt;
using System.Text;
using AtonAPI.Services.UserService;
using Domain.Entites;
using Microsoft.IdentityModel.Tokens;

namespace AtonAPI.Helpers;

public class Jwtmiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;
    //private readonly ILogger _logger;

    public Jwtmiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }

    public async Task Invoke(HttpContext context, IUserService userService)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token != null)
            AttachUserToContext(context, userService, token);

        await _next(context);
    }

    public void AttachUserToContext(HttpContext context, IUserService userService, string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            // min 16 characters
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = Guid.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

            var user = userService.GetById(userId).Result;
            context.Items["User"] = user;
        }
        catch
        {
            // todo: need to add logger
        }
    }
}