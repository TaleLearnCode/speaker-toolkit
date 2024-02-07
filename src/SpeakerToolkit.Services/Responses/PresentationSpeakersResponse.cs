namespace TaleLearnCode.SpeakerToolkit.Responses;

public class PresentationSpeakersResponse
{
	public int PresentationId { get; set; }
	public string PresentationTitle { get; set; } = null!;
	public PresentationSpeakerResponse PrimarySpeaker { get; set; } = null!;
	public List<PresentationSpeakerResponse> SecondarySpeakers { get; set; } = [];
}