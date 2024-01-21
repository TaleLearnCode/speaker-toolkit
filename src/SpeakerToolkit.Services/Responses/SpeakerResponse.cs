namespace TaleLearnCode.SpeakerToolkit.Responses;

public class SpeakerResponse
{
	public int Id { get; set; }
	public string? FirstName { get; set; } = string.Empty;
	public string? LastName { get; set; } = string.Empty;
	public bool EnablePublicProfile { get; set; }
	public string PublicProfileUrl { get; set; } = null!;
	public string CountryCode { get; set; } = null!;
	public string CountryName { get; set; } = null!;
	public string? CountryDivisionCode { get; set; } = string.Empty;
	public string? CountryDivisionName { get; set; } = string.Empty;
	public List<SpeakerBiographyResponse>? Biographies { get; set; }
	public List<SpeakerLinkResponse>? Links { get; set; }
	public List<SpeakerAwardResponse>? Awards { get; set; }
}