namespace TaleLearnCode.SpeakerToolkit.Responses;

public class PresentationTextResponse
{
	public int? PresentationId { get; set; }
	public string LanguageCode { get; set; } = null!;
	public string Title { get; set; } = null!;
	public string? ShortTitle { get; set; }
	public string? Abstract { get; set; }
	public string? ShortAbstract { get; set; }
	public string? Summary { get; set; }
	public string? AdditionalDetails { get; set; }
}