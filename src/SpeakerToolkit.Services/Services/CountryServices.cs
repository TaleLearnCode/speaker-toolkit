namespace TaleLearnCode.SpeakerToolkit.Services;

public class CountryServices(ConfigServices configServices) : ServicesBase(configServices)
{

	public async Task<List<CountryResponse?>?> GetCountriesAsync(GetCountryOptions? options = null)
	{
		options ??= new();
		using SpeakerToolkitContext speakerToolkitContext = new(_configServices);
		return (await speakerToolkitContext.Countries
			.Include(x => x.WorldRegion)
			.Include(x => x.WorldSubregion)
			.Include(x => x.CountryDivisions)
			.OrderBy(c => c.CountryName)
			.ToListAsync()).ToResponse(options);
	}

	public async Task<CountryResponse?> GetCountryAsync(string countryCode, GetCountryOptions? options = null)
	{
		options ??= new();
		using SpeakerToolkitContext speakerToolkitContext = new(_configServices);
		return (await speakerToolkitContext.Countries
			.Include(x => x.WorldRegion)
			.Include(x => x.WorldSubregion)
			.Include(x => x.CountryDivisions)
			.FirstOrDefaultAsync(c => c.CountryCode == countryCode))
			.ToResponse(options);
	}

}