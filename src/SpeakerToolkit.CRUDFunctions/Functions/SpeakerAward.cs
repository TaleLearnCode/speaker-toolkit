namespace TaleLearnCode.SpeakerToolkit.Functions;

public class SpeakerAward(ILoggerFactory loggerFactory, ConfigServices configServices, JsonSerializerOptions jsonSerializerOptions)
{

	private readonly ILogger _logger = loggerFactory.CreateLogger<SpeakerAward>();
	private readonly SpeakerAwardServices _services = new(configServices);
	private readonly JsonSerializerOptions _jsonSerializerOptions = jsonSerializerOptions;

	[Function("GetSpeakerAwards")]
	public async Task<HttpResponseData> GetSpeakerAwardsAsync(
				[HttpTrigger(AuthorizationLevel.Function, "get", Route = "speaker-awards/{speakerId}")] HttpRequestData request,
				int speakerId)
	{
		try
		{
			return await request.CreateResponseAsync(
				await _services.GetSpeakerAwardsAsync(speakerId),
				_jsonSerializerOptions);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Unhandled Exception: {errorMessage}", ex.Message);
			return request.CreateErrorResponse(ex);
		}
	}

	[Function("GetSpeakerAward")]
	public async Task<HttpResponseData> GetSpeakerAwardAsync(
		[HttpTrigger(AuthorizationLevel.Function, "get", Route = "speaker-awards/{speakerId}/{id}")] HttpRequestData request,
		int speakerId,
		int id)
	{
		try
		{
			return await request.CreateResponseAsync(
				await _services.GetSpeakerAwardAsync(speakerId, id),
				_jsonSerializerOptions);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Unhandled Exception: {errorMessage}", ex.Message);
			return request.CreateErrorResponse(ex);
		}
	}

}