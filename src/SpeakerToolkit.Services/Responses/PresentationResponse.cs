namespace TaleLearnCode.SpeakerToolkit.Responses;

public class PresentationResponse
{
	public int Id { get; set; }
	public int PresentationTypeId { get; set; }
	public string PresentationType { get; set; } = null!;
	public string? RepoLink { get; set; }
	public string Permalink { get; set; } = null!;
	public bool IsArchived { get; set; }
	public bool IncludeInPublicProfile { get; set; }
	public string DefaultLanguageCode { get; set; } = null!;
	public string Title { get; set; } = null!;
	public List<PresentationSpeakerListItemResponse>? Speakers { get; set; }
	public List<PresentationTextResponse>? PresentationTexts { get; set; }
	public List<string>? Tags { get; set; }
	public List<PresentationListItemResponse>? RelatedPresentations { get; set; }
}