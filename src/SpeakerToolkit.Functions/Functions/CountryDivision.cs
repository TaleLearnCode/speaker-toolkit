namespace TaleLearnCode.SpeakerToolkit.Functions;

public class CountryDivision(ILoggerFactory loggerFactory, ConfigServices configServices, JsonSerializerOptions jsonSerializerOptions)
{

	private readonly ILogger _logger = loggerFactory.CreateLogger<Country>();
	private readonly CountryDivisionServices _countryDivisionServices = new(configServices);
	private readonly JsonSerializerOptions _jsonSerializerOptions = jsonSerializerOptions;

	[Function("GetCountryDivisions")]
	public async Task<HttpResponseData> GetCountryDivisionsAsync(
		[HttpTrigger(AuthorizationLevel.Function, "get", Route = "country-divisions/{countryCode}")] HttpRequestData request,
		string countryCode)
	{
		try
		{
			return await request.CreateResponseAsync(
				await _countryDivisionServices.GetCountryDivisionsAsync(
					countryCode,
					await request.GetRequestParameters2Async<GetCountryDivisionOptions?>()),
				_jsonSerializerOptions);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Unhandled Exception: {errorMessage}", ex.Message);
			return request.CreateErrorResponse(ex);
		}
	}

	[Function("GetCountryDivision")]
	public async Task<HttpResponseData> GetCountryDivisionAsync(
		[HttpTrigger(AuthorizationLevel.Function, "get", Route = "country-divisions/{countryCode}/{countryDivisionCode}")] HttpRequestData request,
		string countryCode,
		string countryDivisionCode)
	{
		try
		{
			return await request.CreateResponseAsync(
				await _countryDivisionServices.GetCountryDivisionAsync(
					countryCode,
					countryDivisionCode,
					await request.GetRequestParameters2Async<GetCountryDivisionOptions?>()),
				_jsonSerializerOptions);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Unhandled Exception: {errorMessage}", ex.Message);
			return request.CreateErrorResponse(ex);
		}
	}

}