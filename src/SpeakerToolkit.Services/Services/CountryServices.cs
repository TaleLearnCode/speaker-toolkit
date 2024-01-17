namespace TaleLearnCode.SpeakerToolkit.Services;

public class CountryServices(ConfigServices configServices) : ServicesBase(configServices)
{

	public async Task<List<CountryResponse>> GetCountriesAsync(GetCountryOptions? options = null)
		=> (await GetDataAsync(options)).ToResponse(options);

	public async Task<CountryResponse?> GetCountryAsync(string countryCode, GetCountryOptions? options = null)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(countryCode);
		options ??= new();
		return (await GetDataAsync(options, countryCode)).FirstOrDefault().ToResponse(options);
	}

	public async Task<CountryResponse?> GetCountryDivisionsAsync(string countryCode)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(countryCode);
		GetCountryOptions options = new() { IncludeDivisions = true };
		return (await GetDataAsync(options, countryCode)).FirstOrDefault().ToResponse();
	}

	private async Task<List<Country>> GetDataAsync(GetCountryOptions? options, string? countryCode = null)
	{
		options ??= new();
		using SpeakerToolkitContext speakerToolkitContext = new(_configServices);
		IQueryable<Country> query = speakerToolkitContext.Countries
			.Include(x => x.WorldRegion)
			.Include(x => x.WorldSubregion);
		if (!string.IsNullOrEmpty(countryCode))
			query = query.Where(x => x.CountryCode == countryCode);
		if (options.IncludeDivisions)
			query = query.Include(x => x.CountryDivisions);
		query = query.OrderBy(x => x.CountryName);
		return await query.ToListAsync();
	}

}