namespace TaleLearnCode.SpeakerToolkit.Responses;

public class WorldRegionResponse
{
	public string Code { get; set; } = null!;
	public string Name { get; set; } = null!;
	public string? ParentCode { get; set; }
	public string? ParentName { get; set; }
	public List<WorldRegionResponse?>? Subregions { get; set; }
	public List<CountryResponse?>? Countries { get; set; }
}