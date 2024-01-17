namespace TaleLearnCode.SpeakerToolkit.Extensions;

internal static class WorldRegionExtensions
{

	internal static WorldRegionResponse? ToResponse(this WorldRegion? worldRegion, GetWorldRegionOptions? options = null)
	{
		options ??= new();
		return worldRegion is null ? null : new()
		{
			Code = worldRegion.WorldRegionCode,
			Name = worldRegion.WorldRegionName,
			ParentCode = worldRegion.ParentId,
			ParentName = worldRegion.Parent?.WorldRegionName,
			Subregions = (options.IncludeSubregions && worldRegion.Subregions.Count > 0) ? worldRegion.Subregions.Select(ws => ws.ToSubregionResponse(options)).ToList() : null,
			Countries = BuildCountryList(worldRegion, options.IncludeCountries, options.IncludeCountryDivisions)
		};
	}

	internal static List<WorldRegionResponse> ToResponse(this IEnumerable<WorldRegion> worldRegions, GetWorldRegionOptions? options)
	{
		List<WorldRegionResponse?> rawResponse = worldRegions.Select(wr => wr.ToResponse(options)).ToList();
		List<WorldRegionResponse> response = [];
		foreach (WorldRegionResponse? worldRegionResponse in rawResponse)
			if (worldRegionResponse is not null)
				response.Add(worldRegionResponse);
		return response;
	}

	internal static WorldRegionResponse? ToResponse(this WorldRegion? worldRegion, bool includeCountryDivisions)
	{
		return worldRegion is null ? null : new()
		{
			Code = worldRegion.WorldRegionCode,
			Name = worldRegion.WorldRegionName,
			ParentCode = worldRegion.ParentId,
			ParentName = worldRegion.Parent?.WorldRegionName,
			Countries = BuildCountryList(worldRegion, true, includeCountryDivisions)
		};
	}

	private static WorldRegionResponse? ToSubregionResponse(this WorldRegion? worldSubregion, GetWorldRegionOptions options)
	{
		return worldSubregion is null ? null : new()
		{
			Code = worldSubregion.WorldRegionCode,
			Name = worldSubregion.WorldRegionName,
			ParentCode = worldSubregion.ParentId,
			ParentName = worldSubregion.Parent?.WorldRegionName,
			Subregions = (options.IncludeSubregions && worldSubregion.Subregions.Count > 0) ? worldSubregion.Subregions.Select(ws => ws.ToResponse(options)).ToList() : null,
			Countries = BuildCountryList(worldSubregion, options.IncludeSubregionCountries, options.IncludeSubregionCountryDivisions)
		};
	}

	private static List<CountryResponse?>? BuildCountryList(WorldRegion worldRegion, bool includeCountries, bool includeCountryDivisions)
	{
		List<Country>? countries = null;
		if (includeCountries)
		{
			countries = [];
			foreach (Country? country in worldRegion.WorldRegionCountries)
				countries.AddOnlyOnce(country);
			foreach (Country? country in worldRegion.WorldSubregionCountries)
				countries.AddOnlyOnce(country);
		}
		return countries?.ToResponse(
				new GetCountryOptions
				{
					IncludeDivisions = includeCountryDivisions,
					IncludeWorldRegionDetails = false,
					IncludeWorldSubregionDetails = false
				});
	}

}