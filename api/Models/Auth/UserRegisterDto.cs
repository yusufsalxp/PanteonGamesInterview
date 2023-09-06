
using System.ComponentModel.DataAnnotations;

public class UserRegisterDto
{
    [Required]
    public required string Username { get; set; }

    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    [StringLength(255, MinimumLength = 6)]
    public required string Password { get; set; }
}