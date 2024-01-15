namespace TaleLearnCode.SpeakerToolkit.Options;

public class GetWorldRegionOptions
{
	public bool IncludeCountries { get; set; } = false;
	public bool IncludeCountryDivisions { get; set; } = false;
	public bool IncludeSubregions { get; set; } = false;
	public bool IncludeSubregionCountries { get; set; } = false;
	public bool IncludeSubregionCountryDivisions { get; set; } = false;
}