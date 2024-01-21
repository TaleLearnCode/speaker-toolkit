namespace TaleLearnCode.SpeakerToolkit.Responses;

public class SpeakerBiographyResponse
{
	public int SpeakerId { get; set; }
	public string? SpeakerName { get; set; }
	public string LanguageCode { get; set; } = null!;
	public string? LanguageName { get; set; }
	public string Title { get; set; } = null!;
	public string Biography { get; set; } = null!;
}