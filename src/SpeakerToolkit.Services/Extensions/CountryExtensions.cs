namespace TaleLearnCode.SpeakerToolkit.Extensions;

internal static class CountryExtensions
{

	internal static CountryResponse? ToResponse(this Country? country, GetCountryOptions? options = null)
	{
		options ??= new GetCountryOptions();
		return country is null ? null : new()
		{
			Code = country.CountryCode,
			Name = country.CountryName,
			WorldRegionCode = country.WorldRegionCode,
			WorldRegionName = country.WorldRegion?.WorldRegionName ?? string.Empty,
			WorldRegion = (options.IncludeWorldRegionDetails) ? country.WorldRegion.ToResponseDelete() : null,
			WorldSubregionCode = country.WorldSubregionCode,
			WorldSubregionName = country.WorldSubregion?.WorldRegionName ?? string.Empty,
			WorldSubregion = (options.IncludeWorldSubregionDetails) ? country.WorldSubregion.ToResponseDelete() : null,
			HasDivisions = country.HasDivisions,
			Divisions = (options.IncludeDivisions) ? country.CountryDivisions.Select(cd => cd.ToResponse(false)).ToList() : null
		};
	}

	internal static List<CountryResponse?>? ToResponse(this IEnumerable<Country>? countries, GetCountryOptions options)
		=> countries?.Select(c => c.ToResponse(options)).ToList();

}