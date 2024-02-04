namespace TaleLearnCode.SpeakerToolkit.Responses;

public class RelatedPresentationsResponse
{
	public int PresentationId { get; set; }
	public string PresentationTitle { get; set; } = null!;
	public List<PresentationListItemResponse> RelatedPresentations { get; set; } = [];
}