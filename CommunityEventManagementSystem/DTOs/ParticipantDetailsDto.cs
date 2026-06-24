
namespace CommunityEventManagementSystem.DTOs;

public class ParticipantDetailsDto
{
  
    // Core Properties
   

    public int Id { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public int RegistrationCount { get; set; }

 
    // Activity Analytics
  

    public bool IsActiveParticipant =>
        RegistrationCount > 0;

    public bool IsHighlyActive =>
        RegistrationCount >= 10;

    public bool IsModeratelyActive =>
        RegistrationCount >= 5 &&
        RegistrationCount < 10;

    public bool IsOccasionalParticipant =>
        RegistrationCount >= 1 &&
        RegistrationCount < 5;

    public bool IsNewParticipant =>
        RegistrationCount == 0;

   
    // Participant Category
   

    public string ParticipantCategory =>
        RegistrationCount switch
        {
            >= 10 => "Highly Active",
            >= 5 => "Active",
            >= 1 => "Occasional",
            _ => "New"
        };

   
    // Dashboard Metrics
   

    public int EngagementScore =>
        RegistrationCount * 10;

    public string EngagementLevel =>
        EngagementScore switch
        {
            >= 100 => "Excellent",
            >= 50 => "Good",
            >= 20 => "Average",
            _ => "Low"
        };

    // Display Helpers
  

    public string DisplayEmail =>
        string.IsNullOrWhiteSpace(Email)
            ? "Not Provided"
            : Email;

    public string DisplayPhone =>
        string.IsNullOrWhiteSpace(PhoneNumber)
            ? "Not Provided"
            : PhoneNumber;

    public string RegistrationSummary =>
        $"{RegistrationCount} Registrations";


    // UI Helpers
 

    public string CategoryBadgeColor =>
        ParticipantCategory switch
        {
            "Highly Active" => "success",
            "Active" => "primary",
            "Occasional" => "warning",
            _ => "secondary"
        };

    public string StatusBadgeColor =>
        IsActiveParticipant
            ? "success"
            : "danger";

    public string ParticipantStatus =>
        IsActiveParticipant
            ? "Active"
            : "Inactive";

    // Dashboard Ranking
  

    public int ParticipantRank =>
        RegistrationCount switch
        {
            >= 20 => 1,
            >= 10 => 2,
            >= 5 => 3,
            >= 1 => 4,
            _ => 5
        };

    public string RankTitle =>
        ParticipantRank switch
        {
            1 => "Elite Member",
            2 => "Gold Member",
            3 => "Silver Member",
            4 => "Bronze Member",
            _ => "New Member"
        };
}

