
using System.ComponentModel.DataAnnotations;

public class UserLoginDto
{
    [Required]
    public required string Username { get; set; }

    [Required]
    [StringLength(255, MinimumLength = 6)]
    public required string Password { get; set; }
}