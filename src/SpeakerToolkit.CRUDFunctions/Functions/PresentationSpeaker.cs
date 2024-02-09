namespace TaleLearnCode.SpeakerToolkit.Functions;

public class PresentationSpeaker(ILoggerFactory loggerFactory, ConfigServices configServices, JsonSerializerOptions jsonSerializerOptions)
{

	private readonly ILogger _logger = loggerFactory.CreateLogger<SpeakerLink>();
	private readonly PresentationServices _services = new(configServices);
	private readonly JsonSerializerOptions _jsonSerializerOptions = jsonSerializerOptions;


	[Function("GetPresentationSpeakers")]
	public async Task<HttpResponseData> GetPresentationSpeakersAsync(
		[HttpTrigger(AuthorizationLevel.Function, "get", Route = "presentations/{presentationId:int}/speakers/")] HttpRequestData request,
		int presentationId)
	{
		try
		{
			return await request.CreateResponseAsync(await _services.GetSpeakersAsync(presentationId), _jsonSerializerOptions);
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

	[Function("AddPresentationSpeaker")]
	public async Task<HttpResponseData> AddPresentationSpeakerAsync(
		[HttpTrigger(AuthorizationLevel.Function, "post", Route = "presentations/{presentationId:int}/speakers/{speakerId:int}")] HttpRequestData request,
		int presentationId,
		int speakerId)
	{
		try
		{
			await _services.AddSpeakerAsync(presentationId, speakerId, request.GetBooleanQueryStringValue("isPrimary", false));
			return request.CreateCreatedResponse("/presentations/{presentationId}/speakers/{speakerId}");
		}
		catch (Exception ex) when (ex is ArgumentOutOfRangeException)
		{
			return request.CreateNotFoundResponse(ex);
		}
		catch (Exception ex) when (ex is ArgumentException || ex is ObjectAlreadyExistsException)
		{
			return request.CreateBadRequestResponse(ex);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Unhandled Exception: {errorMessage}", ex.Message);
			return request.CreateErrorResponse(ex);
		}
	}

	[Function("RemovePresentationSpeaker")]
	public async Task<HttpResponseData> RemovePresentationSpeakerAsync(
		[HttpTrigger(AuthorizationLevel.Function, "delete", Route = "presentations/{presentationId:int}/speakers/{speakerId:int}")] HttpRequestData request,
		int presentationId,
		int speakerId)
	{
		try
		{
			await _services.RemoveSpeakerAsync(presentationId, speakerId);
			return request.CreateOkResponse();
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