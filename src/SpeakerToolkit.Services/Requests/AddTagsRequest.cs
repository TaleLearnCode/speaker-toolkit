namespace TaleLearnCode.SpeakerToolkit.Requests;

public class AddTagsRequest
{
	public IEnumerable<string> Tags { get; set; } = null!;
}