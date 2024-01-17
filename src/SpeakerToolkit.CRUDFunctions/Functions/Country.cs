namespace TaleLearnCode.SpeakerToolkit.Functions;

public class Country(ILoggerFactory loggerFactory, ConfigServices configServices, JsonSerializerOptions jsonSerializerOptions)
{

	private readonly ILogger _logger = loggerFactory.CreateLogger<Country>();
	private readonly CountryServices _countryServices = new(configServices);
	private readonly JsonSerializerOptions _jsonSerializerOptions = jsonSerializerOptions;

	[Function("GetCountries")]
	public async Task<HttpResponseData> GetCountriesAsync([HttpTrigger(AuthorizationLevel.Function, "get", Route = "countries")] HttpRequestData request)
	{
		try
		{
			return await request.CreateResponseAsync(
				await _countryServices.GetCountriesAsync(
					await request.GetRequestParameters2Async<GetCountryOptions?>()),
				_jsonSerializerOptions);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Unhandled Exception: {errorMessage}", ex.Message);
			return request.CreateErrorResponse(ex);
		}
	}

	[Function("GetCountry")]
	public async Task<HttpResponseData> GetCountryAsync(
		[HttpTrigger(AuthorizationLevel.Function, "get", Route = "countries/{code}")] HttpRequestData request,
		string code)
	{
		try
		{
			return await request.CreateResponseAsync(
				await _countryServices.GetCountryAsync(
					code,
					await request.GetRequestParameters2Async<GetCountryOptions?>()),
				_jsonSerializerOptions);
		}
		catch (ArgumentException ex)
		{
			return request.CreateBadRequestResponse(ex);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Unhandled Exception: {errorMessage}", ex.Message);
			return request.CreateErrorResponse(ex);
		}
	}

	[Function("GetCountryCountryDivisions")]
	public async Task<HttpResponseData> GetCountryCountryDivisionsAsync(
		[HttpTrigger(AuthorizationLevel.Function, "get", Route = "countries/{code}/divisions")] HttpRequestData request,
		string code)
	{
		try
		{
			return await request.CreateResponseAsync(
				await _countryServices.GetCountryDivisionsAsync(code),
				_jsonSerializerOptions);
		}
		catch (ArgumentException ex)
		{
			return request.CreateBadRequestResponse(ex);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Unhandled Exception: {errorMessage}", ex.Message);
			return request.CreateErrorResponse(ex);
		}
	}

}