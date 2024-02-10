namespace TaleLearnCode.SpeakerToolkit.Requests;

public class EngagementPresentationRequest
{
	public int EngagementId { get; set; }
	public int PresentationId { get; set; }
	public int StatusId { get; set; }
	public DateTime? StartDateTime { get; set; }
	public DateTime? EndDateTime { get; set; }
	public string TimeZone { get; set; } = null!;
	public string? Room { get; set; }
}