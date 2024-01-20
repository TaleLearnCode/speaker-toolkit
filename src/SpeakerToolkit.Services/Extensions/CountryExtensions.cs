namespace TaleLearnCode.SpeakerToolkit.Extensions;

internal static class CountryExtensions
{

	internal static CountryResponse? ToResponse(this Country? country, GetCountryOptions options)
	{
		return country is null ? null : new()
		{
			Code = country.CountryCode,
			Name = country.CountryName,
			WorldRegionCode = country.WorldRegionCode,
			WorldRegionName = country.WorldRegion?.WorldRegionName ?? string.Empty,
			WorldRegion = (options.IncludeWorldRegionDetails) ? country.WorldRegion.ToResponse() : null,
			WorldSubregionCode = country.WorldSubregionCode,
			WorldSubregionName = country.WorldSubregion?.WorldRegionName ?? string.Empty,
			WorldSubregion = (options.IncludeWorldSubregionDetails) ? country.WorldSubregion.ToResponse() : null,
			HasDivisions = country.HasDivisions,
			Divisions = (options.IncludeDivisions) ? country.CountryDivisions.Select(cd => cd.ToResponse()).ToList() : null
		};
	}

	internal static CountryResponse? ToResponse(this Country? country)
	{
		return country is null ? null : new()
		{
			Code = country.CountryCode,
			Name = country.CountryName,
			HasDivisions = country.HasDivisions,
			Divisions = country.CountryDivisions.Select(cd => cd.ToResponse()).ToList()
		};
	}

	internal static List<CountryResponse> ToResponse(this IEnumerable<Country> countries, GetCountryOptions? options)
	{
		options ??= new();
		List<CountryResponse?> rawResponse = countries.Select(c => c.ToResponse(options)).ToList();
		List<CountryResponse> response = [];
		foreach (CountryResponse? countryResponse in rawResponse)
			if (countryResponse is not null)
				response.Add(countryResponse);
		return response;
	}

}