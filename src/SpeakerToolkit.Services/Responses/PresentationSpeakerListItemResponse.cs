namespace TaleLearnCode.SpeakerToolkit.Responses;

public class PresentationSpeakerListItemResponse
{
	public int Id { get; set; }
	public bool IsPrimary { get; set; }
	public string Name { get; set; } = null!;
}