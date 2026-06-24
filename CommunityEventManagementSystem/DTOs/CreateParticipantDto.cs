using System.ComponentModel.DataAnnotations;

namespace CommunityEventManagementSystem.DTOs;

public class CreateParticipantDto
{
    [Required(ErrorMessage = "First name is required.")]
    [StringLength(
        50,
        MinimumLength = 2,
        ErrorMessage = "First name must be between 2 and 50 characters.")]
    [RegularExpression(
        @"^[A-Za-z\s\-]+$",
        ErrorMessage = "First name can only contain letters.")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last name is required.")]
    [StringLength(
        50,
        MinimumLength = 2,
        ErrorMessage = "Last name must be between 2 and 50 characters.")]
    [RegularExpression(
        @"^[A-Za-z\s\-]+$",
        ErrorMessage = "Last name can only contain letters.")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email address is required.")]
    [StringLength(
        100,
        ErrorMessage = "Email cannot exceed 100 characters.")]
    [EmailAddress(
        ErrorMessage = "Please enter a valid email address.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Phone number is required.")]
    [StringLength(
        20,
        MinimumLength = 7,
        ErrorMessage = "Phone number must be between 7 and 20 digits.")]
    [Phone(
        ErrorMessage = "Please enter a valid phone number.")]
    public string PhoneNumber { get; set; } = string.Empty;
}