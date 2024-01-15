namespace TaleLearnCode.SpeakerToolkit.Extensions;

internal static class CountryDivisionExtensions
{

	internal static CountryDivisionResponse? ToResponse(this CountryDivision? countryDivision, bool includeCountryDetails = false)
		=> countryDivision is null ? null : new()
		{
			Code = countryDivision.CountryDivisionCode,
			Name = countryDivision.CountryDivisionName,
			CountryCode = countryDivision.CountryCode,
			CountryName = countryDivision.Country?.CountryName ?? string.Empty,
			Country = (includeCountryDetails) ? countryDivision.Country : null,
			CategoryName = countryDivision.CategoryName
		};

	internal static List<CountryDivisionResponse?> ToResponse(this IEnumerable<CountryDivision>? countryDivisions, bool includeCountryDetails = false)
		=> countryDivisions?.Select(cd => cd.ToResponse(includeCountryDetails)).ToList() ?? [];

}