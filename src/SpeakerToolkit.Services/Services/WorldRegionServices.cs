namespace TaleLearnCode.SpeakerToolkit.Services;

public class WorldRegionServices(ConfigServices configServices) : ServicesBase(configServices)
{

	public async Task<List<WorldRegionResponse>> GetWorldRegionsAsync(GetWorldRegionOptions? options = null)
		=> (await GetDataAsync(options)).ToResponse(options);

	public async Task<WorldRegionResponse?> GetWorldRegionAsync(string worldRegionCode, GetWorldRegionOptions? options = null)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(worldRegionCode);
		return (await GetDataAsync(options, worldRegionCode)).FirstOrDefault().ToResponse(options);
	}

	public async Task<WorldRegionResponse?> GetWorldRegionCountriesAsync(string worldRegionCode, GetWorldRegionCountriesOptions? options = null)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(worldRegionCode);
		return (await GetDataAsync(options, worldRegionCode)).FirstOrDefault().ToResponse(options?.IncludeCountryDivisions ?? false);
	}

	private async Task<List<WorldRegion>> GetDataAsync(
		GetWorldRegionOptions? options,
		string? worldRegionCode = null)
	{
		options ??= new();
		using SpeakerToolkitContext speakerToolkitContext = new(_configServices);
		IQueryable<WorldRegion> query = speakerToolkitContext.WorldRegions.OrderBy(x => x.WorldRegionName);
		if (!string.IsNullOrEmpty(worldRegionCode))
			query = query.Where(x => x.WorldRegionCode == worldRegionCode);
		List<WorldRegion> worldRegions = await query.ToListAsync();
		await AddCountryDataAsync(options, speakerToolkitContext, worldRegions);
		await AddSubregionDataAsync(options, speakerToolkitContext, worldRegions);
		return worldRegions;
	}

	private async Task<List<WorldRegion>> GetDataAsync(
		GetWorldRegionCountriesOptions? options,
		string worldRegionCode)
	{
		options ??= new();
		using SpeakerToolkitContext speakerToolkitContext = new(_configServices);
		IQueryable<WorldRegion> query = speakerToolkitContext.WorldRegions.OrderBy(x => x.WorldRegionName);
		query = query.Where(x => x.WorldRegionCode == worldRegionCode);
		List<WorldRegion> worldRegions = await query.ToListAsync();
		await AddCountryDataAsync(options, speakerToolkitContext, worldRegions);
		return worldRegions;
	}

	private static async Task AddCountryDataAsync(
		GetWorldRegionOptions options,
		SpeakerToolkitContext speakerToolkitContext,
		List<WorldRegion> worldRegions)
		=> await AddCountryDataAsync(options.IncludeCountries, options.IncludeCountryDivisions, speakerToolkitContext, worldRegions);

	private static async Task AddCountryDataAsync(
		GetWorldRegionCountriesOptions options,
		SpeakerToolkitContext speakerToolkitContext,
		List<WorldRegion> worldRegions)
		=> await AddCountryDataAsync(true, options.IncludeCountryDivisions, speakerToolkitContext, worldRegions);

	private static async Task AddCountryDataAsync(
		bool includeCountries,
		bool includeCountryDivisions,
		SpeakerToolkitContext speakerToolkitContext,
		List<WorldRegion> worldRegions)
	{
		if (includeCountries)
		{
			if (includeCountryDivisions)
			{
				foreach (WorldRegion worldRegion in worldRegions)
				{
					worldRegion.WorldRegionCountries = await speakerToolkitContext.Countries
						.Include(x => x.CountryDivisions)
						.Where(x => x.WorldRegionCode == worldRegion.WorldRegionCode)
						.OrderBy(x => x.CountryName)
						.ToListAsync();
				}
			}
			else
			{
				foreach (WorldRegion worldRegion in worldRegions)
				{
					worldRegion.WorldRegionCountries = await speakerToolkitContext.Countries
						.Where(x => x.WorldRegionCode == worldRegion.WorldRegionCode)
						.OrderBy(x => x.CountryName)
						.ToListAsync();
				}
			}
		}
	}

	private static async Task AddSubregionDataAsync(
		GetWorldRegionOptions options,
		SpeakerToolkitContext speakerToolkitContext,
		List<WorldRegion> worldRegions)
	{
		if (options.IncludeSubregions)
		{
			if (options.IncludeSubregionCountries)
			{
				if (options.IncludeSubregionCountryDivisions)
				{
					foreach (WorldRegion worldRegion in worldRegions)
					{
						worldRegion.Subregions = await speakerToolkitContext.WorldRegions
							.Include(x => x.WorldSubregionCountries)
								.ThenInclude(x => x.CountryDivisions)
							.Where(x => x.ParentId == worldRegion.WorldRegionCode)
							.OrderBy(x => x.WorldRegionName)
							.ToListAsync();
					}
				}
				else
				{
					foreach (WorldRegion worldRegion in worldRegions)
					{
						worldRegion.Subregions = await speakerToolkitContext.WorldRegions
							.Include(x => x.WorldSubregionCountries)
							.Where(x => x.ParentId == worldRegion.WorldRegionCode)
							.OrderBy(x => x.WorldRegionName)
							.ToListAsync();
					}
				}
			}
			else
			{
				foreach (WorldRegion worldRegion in worldRegions)
				{
					worldRegion.Subregions = await speakerToolkitContext.WorldRegions
						.Where(x => x.ParentId == worldRegion.WorldRegionCode)
						.OrderBy(x => x.WorldRegionName)
						.ToListAsync();
				}
			}
		}

	}

}