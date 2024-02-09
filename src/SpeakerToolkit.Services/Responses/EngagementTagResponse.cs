namespace TaleLearnCode.SpeakerToolkit.Responses;

public class EngagementTagsResponse
{
	public int EngagementId { get; set; }
	public string EngagementTitle { get; set; } = null!;
	public IEnumerable<string> EngagementTags { get; set; } = [];
}