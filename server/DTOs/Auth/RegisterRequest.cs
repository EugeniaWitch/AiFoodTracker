using System.ComponentModel.DataAnnotations;

namespace server.DTOs.Auth;

public class RegisterRequest
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(20,MinimumLength = 3, ErrorMessage = "Name must be between 3 and 20 characters")]
    public string Name{get;set;}=string.Empty;

    [Required(ErrorMessage ="Email is required")]
    [EmailAddress(ErrorMessage ="Invalid email format")]
    [StringLength(100,ErrorMessage ="Email must be less than 100 characters")]
    public string Email{get;set;}= string.Empty;

    [Required(ErrorMessage ="Password is required")]
    [StringLength(100,MinimumLength = 6, ErrorMessage ="Password must be between 6 and 100 characters")]
    public string Password{get;set;}=string.Empty;
}