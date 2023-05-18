using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class BaseRequest
{
    [Required]
    public string UserLogin { get; set; }
}