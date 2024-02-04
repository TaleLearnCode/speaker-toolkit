namespace TaleLearnCode.SpeakerToolkit.Responses;

public class LearningObjectivesResponse
{
	public int? PresentationId { get; set; }
	public string? PresentationTitle { get; set; }
	public string? LanguageCode { get; set; }
	public List<LearningObjectiveResponse> LearningObjectives { get; set; } = [];
}
