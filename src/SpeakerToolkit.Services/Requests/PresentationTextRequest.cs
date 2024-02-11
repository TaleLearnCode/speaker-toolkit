namespace TaleLearnCode.SpeakerToolkit.Requests;

public class PresentationTextRequest
{
	public string? LanguageCode { get; set; } = null;
	public string Title { get; set; } = null!;
	public string? ShortTitle { get; set; }
	public string? Abstract { get; set; }
	public string? ShortAbstract { get; set; }
	public string? Summary { get; set; }
	public string? AdditionalDetails { get; set; }
	public IEnumerable<LearningObjectiveRequest>? LearningObjectives { get; set; } = null;
}