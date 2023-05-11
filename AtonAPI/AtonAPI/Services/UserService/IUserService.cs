﻿using Domain.Entites;
using Domain.Models;

namespace Misc.UserService;

public interface IUserService
{
    AuthResponse Authenticate(LoginModel model);
    Task<List<User>> GetAll();
    Task<User> GetById(Guid id);
}