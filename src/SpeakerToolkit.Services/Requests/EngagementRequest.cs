namespace TaleLearnCode.SpeakerToolkit.Requests;

public class EngagementRequest
{
	public int EngagementTypeId { get; set; }
	public int EngagementStatusId { get; set; }
	public string Name { get; set; } = null!;
	public string? OverviewLocation { get; set; }
	public string ListingLocation { get; set; } = null!;
	public string StartDate { get; set; } = null!;
	public string EndDate { get; set; } = null!;
	public string? StartingCost { get; set; }
	public string? EndingCost { get; set; }
	public string? Description { get; set; }
	public string? Summary { get; set; }
	public string? Url { get; set; }
	public string? Permalink { get; set; }
	public List<string>? Tags { get; set; }
}