namespace TaleLearnCode.SpeakerToolkit.Services;

public class CountryDivisionServices(ConfigServices configServices) : ServicesBase(configServices)
{

	public async Task<List<CountryDivisionResponse>> GetCountryDivisionsAsync(string countryCode, GetCountryDivisionOptions? options = null)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(countryCode);
		return (await GetDataAsync(options, countryCode)).ToResponse(options);
	}

	public async Task<CountryDivisionResponse?> GetCountryDivisionAsync(string countryCode, string countryDivisionCode, GetCountryDivisionOptions? options = null)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(countryDivisionCode);
		return (await GetDataAsync(options, countryCode, countryDivisionCode)).FirstOrDefault().ToResponse(options);
	}

	private async Task<List<CountryDivision>> GetDataAsync(GetCountryDivisionOptions? options, string countryCode, string? countryDivisionCode = null)
	{
		options ??= new();
		using SpeakerToolkitContext speakerToolkitContext = new(_configServices);
		IQueryable<CountryDivision> query = speakerToolkitContext.CountryDivisions;
		if (options.IncludeCountryDetails)
			query = query.Include(x => x.Country).ThenInclude(x => x.WorldRegion).Include(x => x.Country).ThenInclude(x => x.WorldSubregion);
		if (countryDivisionCode is null)
			query = query.Where(x => x.CountryCode == countryCode);
		else
			query = query.Where(x => x.CountryCode == countryCode && x.CountryDivisionCode == countryDivisionCode);
		query = query.OrderBy(x => x.CountryDivisionName);
		return await query.ToListAsync();
	}

}