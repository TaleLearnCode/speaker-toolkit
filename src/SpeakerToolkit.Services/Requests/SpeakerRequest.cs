namespace TaleLearnCode.SpeakerToolkit.Requests;

public class SpeakerRequest
{
	public string FirstName { get; set; } = null!;
	public string LastName { get; set; } = null!;
	public bool EnablePublicProfile { get; set; }
	public string PublicProfileUrl { get; set; } = null!;
	public string CountryCode { get; set; } = null!;
	public string? CountryDivisionCode { get; set; }
	public List<SpeakerBiographyRequest>? Biographies { get; set; }
	public List<SpeakerLinkRequest>? Links { get; set; }
	public List<SpeakerAwardRequest>? Awards { get; set; }
}
