namespace TaleLearnCode.SpeakerToolkit.Requests;

public class PresentationRequest
{
	public int PresentationTypeId { get; set; }
	public string? RepoLink { get; set; }
	public string Permalink { get; set; } = null!;
	public bool IsArchived { get; set; }
	public bool? IncludeInPublicProfile { get; set; } = false;
	public string DefaultLanguageCode { get; set; } = null!;
	public IEnumerable<int>? RelatedPresentations { get; set; }
	public IEnumerable<PresentationSpeakerRequest>? Speakers { get; set; }
	public IEnumerable<string> Tags { get; set; } = null!;
	public IEnumerable<PresentationTextRequest> PresentationTexts { get; set; } = null!;
}