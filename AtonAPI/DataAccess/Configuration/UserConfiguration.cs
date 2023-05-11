using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        var adminUser = new User
        {
            Id = Guid.NewGuid(),
            Login = "admin",
            Password = BCrypt.Net.BCrypt.HashPassword("admin123"),
            Name = "admin",
            Gender = 2,
            Admin = true,
            CreatedOn = DateTime.Now,
            CreatedBy = "-",
            ModifiedOn = DateTime.Now,
            ModifiedBy = "-",
        };

        builder.HasData(adminUser);
    }
}