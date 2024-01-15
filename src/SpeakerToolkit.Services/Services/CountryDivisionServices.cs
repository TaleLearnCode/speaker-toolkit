namespace TaleLearnCode.SpeakerToolkit.Services;

public class CountryDivisionServices(ConfigServices configServices) : ServicesBase(configServices)
{

	public async Task<List<CountryDivisionResponse?>> GetCountryDivisionsAsync(string countryCode, GetCountryDivisionOptions? options = null)
	{
		options ??= new();
		using SpeakerToolkitContext speakerToolkitContext = new(_configServices);
		return (await speakerToolkitContext.CountryDivisions
			.Include(x => x.Country)
			.Where(c => c.CountryCode == countryCode)
			.OrderBy(c => c.CountryDivisionName)
			.ToListAsync())
			.ToResponse();
	}

	public async Task<CountryDivisionResponse?> GetCountryDivisionAsync(string countryCode, string countryDivisionCode, GetCountryDivisionOptions? options = null)
	{
		options ??= new();
		using SpeakerToolkitContext speakerToolkitContext = new(_configServices);
		return (await speakerToolkitContext.CountryDivisions
			.Include(x => x.Country)
			.FirstOrDefaultAsync(x => x.CountryCode == countryCode && x.CountryDivisionCode == countryDivisionCode))
			.ToResponse(options.IncludeCountryDetails);
	}

}