using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class UpdateUserInfoModel
{
    public UpdateUserInfoModel()
    {
        
    }
    
    [StringLength(15,MinimumLength = 2)]
    public string? Name { get; set; }
    public int? Gender { get; set; }
    public DateTime? Birthday { get; set; }
}