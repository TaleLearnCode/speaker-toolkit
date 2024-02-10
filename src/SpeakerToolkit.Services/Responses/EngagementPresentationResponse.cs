namespace TaleLearnCode.SpeakerToolkit.Responses;

public class EngagementPresentationResponse
{
	public int Id { get; set; }
	public int EngagementId { get; set; }
	public string? EngagementName { get; set; }
	public int PresentationId { get; set; }
	public string? PresentationTitle { get; set; }
	public int StatusId { get; set; }
	public string? StatusName { get; set; }
	public DateTime? StartDateTime { get; set; }
	public DateTime? EndDateTime { get; set; }
	public string TimeZone { get; set; } = null!;
	public string? Room { get; set; }
}