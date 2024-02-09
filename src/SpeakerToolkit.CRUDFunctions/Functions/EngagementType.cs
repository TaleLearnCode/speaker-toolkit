namespace TaleLearnCode.SpeakerToolkit.Functions;

public class EngagementType(ILoggerFactory loggerFactory, ConfigServices configServices, JsonSerializerOptions jsonSerializerOptions)
{

	private readonly ILogger _logger = loggerFactory.CreateLogger<Country>();
	private readonly EngagementTypeServices _services = new(configServices);
	private readonly JsonSerializerOptions _jsonSerializerOptions = jsonSerializerOptions;

	[Function("GetEngagementTypes")]
	public async Task<HttpResponseData> GetEngagementTypesAsync([HttpTrigger(AuthorizationLevel.Function, "get", Route = "engagement-types")] HttpRequestData request)
	{
		try
		{
			return await request.CreateResponseAsync(await _services.GetEngagementTypesAsync(), _jsonSerializerOptions);
		}
		catch (Exception ex) when (ex is ArgumentOutOfRangeException)
		{
			return request.CreateNotFoundResponse(ex);
		}
		catch (Exception ex) when (ex is ArgumentException)
		{
			return request.CreateBadRequestResponse(ex);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Unhandled Exception: {errorMessage}", ex.Message);
			return request.CreateErrorResponse(ex);
		}
	}

	[Function("GetEngagementType")]
	public async Task<HttpResponseData> GetEngagementTypeAsync(
		[HttpTrigger(AuthorizationLevel.Function, "get", Route = "engagement-types/{engagementTypeId:int}")] HttpRequestData request,
		int engagementTypeId)
	{
		try
		{
			return await request.CreateResponseAsync(await _services.GetEngagementTypeAsync(engagementTypeId), _jsonSerializerOptions);
		}
		catch (Exception ex) when (ex is ArgumentOutOfRangeException)
		{
			return request.CreateNotFoundResponse(ex);
		}
		catch (Exception ex) when (ex is ArgumentException)
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
