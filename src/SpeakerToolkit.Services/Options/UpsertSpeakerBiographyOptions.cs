namespace TaleLearnCode.SpeakerToolkit.Options;

public class UpsertSpeakerBiographyOptions
{
	public bool CreateIfNotExists { get; set; } = true;
	public bool UpdateIfExists { get; set; } = true;
}