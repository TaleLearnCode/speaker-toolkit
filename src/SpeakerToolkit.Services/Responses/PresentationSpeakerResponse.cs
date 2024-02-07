namespace TaleLearnCode.SpeakerToolkit.Responses;

public class PresentationSpeakerResponse
{
	public int? PresentationId { get; set; }
	public string? PresentationTitle { get; set; }
	public int SpeakerId { get; set; }
	public string SpeakerName { get; set; } = null!;
}