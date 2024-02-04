namespace TaleLearnCode.SpeakerToolkit.Responses;

public class LearningObjectiveResponse
{
	public int? PresentationId { get; set; }
	public string? PresentationTitle { get; set; }
	public string? LanguageCode { get; set; }
	public string LearningObjective { get; set; } = null!;
	public int SortOrder { get; set; }
}