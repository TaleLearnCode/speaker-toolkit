namespace TaleLearnCode.SpeakerToolkit.Extensions;

internal static class CountryDivisionExtensions
{

	internal static CountryDivisionResponse? ToResponse(this CountryDivision? countryDivision, GetCountryDivisionOptions? options = null)
	{
		options ??= new GetCountryDivisionOptions();
		return countryDivision is null ? null : new()
		{
			Code = countryDivision.CountryDivisionCode,
			Name = countryDivision.CountryDivisionName,
			CountryCode = countryDivision.CountryCode,
			CountryName = countryDivision.Country?.CountryName ?? string.Empty,
			Country = (options.IncludeCountryDetails) ? countryDivision.Country.ToResponse() : null,
			CategoryName = countryDivision.CategoryName
		};
	}

	internal static List<CountryDivisionResponse> ToResponse(this IEnumerable<CountryDivision> countryDivisions, GetCountryDivisionOptions? options)
	{
		List<CountryDivisionResponse?> rawResponse = countryDivisions.Select(cd => cd.ToResponse(options)).ToList();
		List<CountryDivisionResponse> response = [];
		foreach (CountryDivisionResponse? countryDivisionResponse in rawResponse)
			if (countryDivisionResponse is not null)
				response.Add(countryDivisionResponse);
		return response;
	}

}