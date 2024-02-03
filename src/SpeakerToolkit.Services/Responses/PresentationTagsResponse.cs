namespace TaleLearnCode.SpeakerToolkit.Responses;

public class PresentationTagsResponse
{
	public int PresentationId { get; set; }
	public string PresentationTitle { get; set; } = null!;
	public IEnumerable<string> PresentationTags { get; set; } = [];
}