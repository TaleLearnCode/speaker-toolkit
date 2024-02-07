namespace TaleLearnCode.SpeakerToolkit.Options;

public class GetPresentationOptions
{
	public int? PresentationId { get; set; } = null;
	public bool IncludePresentationTexts { get; set; } = true;
	public bool IncludeRelatedPresentations { get; set; } = false;
	public bool IncludeSpeakers { get; set; } = false;
}