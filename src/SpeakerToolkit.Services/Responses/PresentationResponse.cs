namespace TaleLearnCode.SpeakerToolkit.Responses;

public class PresentationResponse
{
	public int Id { get; set; }
	public string PresentationType { get; set; } = null!;
	public string? RepoLink { get; set; }
	public bool IsArchived { get; set; }
	public bool IncludeInPublicProfile { get; set; }
	public string DefaultLanguageCode { get; set; } = null!;
	public string Title { get; set; } = null!;
	public List<PresentationTextResponse>? PresentationTexts { get; set; }
	public List<TagResponse>? Tags { get; set; }
	public List<RelatedPresentationsResponse>? RelatedPresentations { get; set; }
}