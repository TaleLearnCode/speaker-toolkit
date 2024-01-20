namespace TaleLearnCode.SpeakerToolkit.Responses;

public class SpeakerAwardResponse
{
	public int Id { get; set; }
	public int SpeakerId { get; set; }
	public string SpeakerName { get; set; } = null!;
	public SpeakerAwardTypeResponse? SpeakerAwardType { get; set; } = null!;
	public string? AwardCategory { get; set; }
	public int? AwardYear { get; set; }
	public string AwardProfileUrl { get; set; } = null!;
}