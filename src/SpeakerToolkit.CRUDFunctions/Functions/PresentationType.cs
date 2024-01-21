namespace TaleLearnCode.SpeakerToolkit.Functions;

public class PresentationType(ILoggerFactory loggerFactory, ConfigServices configServices, JsonSerializerOptions jsonSerializerOptions)
{

	private readonly ILogger _logger = loggerFactory.CreateLogger<Speaker>();
	private readonly PresentationTypeServices _services = new(configServices);
	private readonly JsonSerializerOptions _jsonSerializerOptions = jsonSerializerOptions;

	[Function("GetPresentationType")]
	public async Task<HttpResponseData> GetPresentationTypeAsync(
		[HttpTrigger(AuthorizationLevel.Function, "get", Route = "presentation-types/{presentationTypeId:int}")] HttpRequestData request,
		int presentationTypeId)
	{
		try
		{
			return await request.CreateResponseAsync(await _services.GetPresentationTypeAsync(presentationTypeId), _jsonSerializerOptions);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Unhandled Exception: {errorMessage}", ex.Message);
			return request.CreateErrorResponse(ex);
		}
	}

	[Function("GetPresentationTypes")]
	public async Task<HttpResponseData> GetPresentationTypesAsync([HttpTrigger(AuthorizationLevel.Function, "get", Route = "presentation-types")] HttpRequestData request)
	{
		try
		{
			return await request.CreateResponseAsync(await _services.GetPresentationTypesAsync(), _jsonSerializerOptions);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Unhandled Exception: {errorMessage}", ex.Message);
			return request.CreateErrorResponse(ex);
		}
	}

}