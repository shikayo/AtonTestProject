using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class ChangePasswordModel
{
    [Required]
    public string OldPassword { get; set; }
    [Required]
    [StringLength(20, MinimumLength =8)]
    public string NewPassword { get; set; }
}