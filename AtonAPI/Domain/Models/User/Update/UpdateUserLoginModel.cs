using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class UpdateUserLoginModel
{
    public UpdateUserLoginModel()
    {
        
    }
    
    [Required]
    [StringLength(15,MinimumLength = 5)]
    public string NewLogin { get; set; }
}