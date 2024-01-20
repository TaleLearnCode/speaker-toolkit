namespace TaleLearnCode.SpeakerToolkit.Functions;

public class SpeakerAwardType(ILoggerFactory loggerFactory, ConfigServices configServices, JsonSerializerOptions jsonSerializerOptions)
{

	private readonly ILogger _logger = loggerFactory.CreateLogger<SpeakerAwardType>();
	private readonly SpeakerAwardTypeServices _services = new(configServices);
	private readonly JsonSerializerOptions _jsonSerializerOptions = jsonSerializerOptions;

	[Function("GetSpeakerAwardTypes")]
	public async Task<HttpResponseData> GetSpeakerAwardTypesAsync(
		[HttpTrigger(AuthorizationLevel.Function, "get", Route = "speaker-award-types")] HttpRequestData request)
	{
		try
		{
			return await request.CreateResponseAsync(
				await _services.GetSpeakerAwardTypesAsync(),
				_jsonSerializerOptions);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Unhandled Exception: {errorMessage}", ex.Message);
			return request.CreateErrorResponse(ex);
		}
	}

	[Function("GetSpeakerAwardType")]
	public async Task<HttpResponseData> GetSpeakerAwardTypeAsync(
				[HttpTrigger(AuthorizationLevel.Function, "get", Route = "speaker-award-types/{id}")] HttpRequestData request,
				int id)
	{
		try
		{
			return await request.CreateResponseAsync(
				await _services.GetSpeakerAwardTypeAsync(id),
				_jsonSerializerOptions);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Unhandled Exception: {errorMessage}", ex.Message);
			return request.CreateErrorResponse(ex);
		}
	}

}