namespace Domain.Models;

public class DeleteResponse
{
    public string? Revoker { get; set; }
    public string Message { get; set; }
    public bool IsSuccess { get; set; }
}