namespace TaleLearnCode.SpeakerToolkit.Requests;

public class LearningObjectiveRequest
{
	public string LearningObjective { get; set; } = null!;
	public int? SortOrder { get; set; }
}