namespace TaleLearnCode.SpeakerToolkit.Options;

public class UpsertOptions
{
	public bool CreateIfNotExists { get; set; } = true;
	public bool UpdateIfExists { get; set; } = true;
}