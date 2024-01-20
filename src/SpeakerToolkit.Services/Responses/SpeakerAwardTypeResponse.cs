namespace TaleLearnCode.SpeakerToolkit.Responses;

public class SpeakerAwardTypeResponse
{
	public int Id { get; set; }
	public string Name { get; set; } = null!;
	public bool HasCategories { get; set; }
	public bool HasAwardYears { get; set; }
}