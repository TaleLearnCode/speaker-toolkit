namespace TaleLearnCode.SpeakerToolkit.Responses;

public class SpeakerLinkResponse
{
	public int Id { get; set; }
	public int SpeakerId { get; set; }
	public string SpeakerName { get; set; } = null!;
	public string LinkType { get; set; } = null!;
	public string LinkUrl { get; set; } = null!;
}