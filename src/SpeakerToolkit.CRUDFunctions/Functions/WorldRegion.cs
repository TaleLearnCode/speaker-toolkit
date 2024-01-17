namespace TaleLearnCode.SpeakerToolkit.Functions;

public class WorldRegion(ILoggerFactory loggerFactory, ConfigServices configServices, JsonSerializerOptions jsonSerializerOptions)
{

	private readonly ILogger _logger = loggerFactory.CreateLogger<WorldRegion>();
	private readonly WorldRegionServices _worldRegionServices = new(configServices);
	private readonly JsonSerializerOptions _jsonSerializerOptions = jsonSerializerOptions;

	[Function("GetWorldRegions")]
	public async Task<HttpResponseData> GetWorldRegionsAsync([HttpTrigger(AuthorizationLevel.Function, "get", Route = "world-regions")] HttpRequestData request)
	{
		try
		{
			return await request.CreateResponseAsync(
				await _worldRegionServices.GetWorldRegionsAsync(
					await request.GetRequestParameters2Async<GetWorldRegionOptions?>()),
				_jsonSerializerOptions);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Unhandled Exception: {errorMessage}", ex.Message);
			return request.CreateErrorResponse(ex);
		}
	}

	[Function("GetWorldRegion")]
	public async Task<HttpResponseData> GetWorldRegionAsync(
		[HttpTrigger(AuthorizationLevel.Function, "get", Route = "world-regions/{code}")] HttpRequestData request,
		string code)
	{
		try
		{
			return await request.CreateResponseAsync(
				await _worldRegionServices.GetWorldRegionAsync(
					code,
					await request.GetRequestParameters2Async<GetWorldRegionOptions?>()),
				_jsonSerializerOptions);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Unhandled Exception: {errorMessage}", ex.Message);
			return request.CreateErrorResponse(ex);
		}
	}

	[Function("GetWorldRegionCountries")]
	public async Task<HttpResponseData> GetWorldRegionCountriesAsync(
		[HttpTrigger(AuthorizationLevel.Function, "get", Route = "world-regions/{code}/countries")] HttpRequestData request,
		string code)
	{
		try
		{
			return await request.CreateResponseAsync(
				await _worldRegionServices.GetWorldRegionCountriesAsync(
					code,
					await request.GetRequestParameters2Async<GetWorldRegionCountriesOptions?>()),
				_jsonSerializerOptions);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Unhandled Exception: {errorMessage}", ex.Message);
			return request.CreateErrorResponse(ex);
		}
	}

}