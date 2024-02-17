namespace TaleLearnCode.SpeakerToolkit.Responses;

public class EngagementResponse
{
	public int Id { get; set; }
	public int EngagementTypeId { get; set; }
	public string? EngagementType { get; set; }
	public int EngagementStatusId { get; set; }
	public string? EngagementStatus { get; set; }
	public string Name { get; set; } = null!;
	public string? OverviewLocation { get; set; }
	public string ListingLocation { get; set; } = null!;
	public DateOnly StartDate { get; set; }
	public DateOnly EndDate { get; set; }
	public string? StartingCost { get; set; }
	public string? EndingCost { get; set; }
	public string? Description { get; set; }
	public string? Summary { get; set; }
	public string? Url { get; set; }
	public string? Permalink { get; set; }
	public List<EngagementPresentationResponse>? Presentations { get; set; }
	public List<string>? Tags { get; set; }
}