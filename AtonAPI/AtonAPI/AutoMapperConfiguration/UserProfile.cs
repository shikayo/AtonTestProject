using AutoMapper;
using Domain.Entites;
using Domain.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AtonAPI.AutoMapperConfiguration;

public class UserProfile: Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserModel, User>()
            .ForMember(x => x.CreatedOn, opt => opt.MapFrom(x => DateTime.Now))
            .ForMember(x => x.ModifiedOn, opt => opt.MapFrom(x => DateTime.Now))
            .ForMember(x => x.Id, opt => opt.MapFrom(x => Guid.NewGuid()))
            .ForMember(x => x.Password, opt => opt.MapFrom(x => BCrypt.Net.BCrypt.HashPassword(x.Password)));
    }
}