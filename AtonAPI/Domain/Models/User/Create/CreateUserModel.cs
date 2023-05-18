using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class CreateUserModel
{
    [Required]
    [StringLength(20, MinimumLength =5)]
    public string Login { get; set; }
    [Required]
    [StringLength(20, MinimumLength =8)]
    public string Password { get; set; }
    [Required]
    [StringLength(20, MinimumLength =2)]
    public string Name { get; set; }
    [Required]
    public int Gender { get; set; }
    public DateTime? Birthday { get; set; }
    [Required]
    public bool Admin { get; set; }
}