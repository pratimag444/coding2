using System.ComponentModel.DataAnnotations;

namespace CommunityEventManagementSystem.DTOs;

public class UpdateParticipantDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "First name is required.")]
    [StringLength(
        50,
        MinimumLength = 2,
        ErrorMessage = "First name must be between 2 and 50 characters.")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last name is required.")]
    [StringLength(
        50,
        MinimumLength = 2,
        ErrorMessage = "Last name must be between 2 and 50 characters.")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email address is required.")]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Phone number is required.")]
    public string PhoneNumber { get; set; } = string.Empty;
}