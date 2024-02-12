namespace TaleLearnCode.SpeakerToolkit.Requests;

public class EngagementPresentationSpeakerRequest
{
	public int SpeakerId { get; set; }
	public bool IsPrimary { get; set; } = false;
}