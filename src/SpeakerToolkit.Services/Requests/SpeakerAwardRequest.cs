namespace TaleLearnCode.SpeakerToolkit.Requests;

public class SpeakerAwardRequest
{
	public int SpeakerAwardTypeId { get; set; }
	public string? AwardCategory { get; set; }
	public int? AwardYear { get; set; }
	public string AwardProfileUrl { get; set; } = null!;
}