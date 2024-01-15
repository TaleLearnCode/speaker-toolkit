namespace TaleLearnCode.SpeakerToolkit.Responses;

public class CountryDivisionResponse
{
	public string Code { get; set; } = null!;
	public string Name { get; set; } = null!;
	public string CountryCode { get; set; } = null!;
	public string CountryName { get; set; } = null!;
	public Country? Country { get; set; }
	public string CategoryName { get; set; } = null!;
}