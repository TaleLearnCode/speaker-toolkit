namespace TaleLearnCode.SpeakerToolkit.Responses;

public class CountryResponse
{
	public string Code { get; set; } = null!;
	public string Name { get; set; } = null!;
	public string WorldRegionCode { get; set; } = null!;
	public string WorldRegionName { get; set; } = null!;
	public WorldRegionResponse? WorldRegion { get; set; }
	public string? WorldSubregionCode { get; set; }
	public string? WorldSubregionName { get; set; }
	public WorldRegionResponse? WorldSubregion { get; set; }
	public bool HasDivisions { get; set; }
	public List<CountryDivisionResponse?>? Divisions { get; set; }
}