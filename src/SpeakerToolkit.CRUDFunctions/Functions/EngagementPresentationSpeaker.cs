namespace TaleLearnCode.SpeakerToolkit.Functions;

public class EngagementPresentationSpeaker(ILoggerFactory loggerFactory, ConfigServices configServices, JsonSerializerOptions jsonSerializerOptions)
{

	private readonly ILogger _logger = loggerFactory.CreateLogger<SpeakerLink>();
	private readonly EngagementServices _services = new(configServices);
	private readonly JsonSerializerOptions _jsonSerializerOptions = jsonSerializerOptions;

	[Function("GetEngagementPresentationSpeakers")]
	public async Task<HttpResponseData> GetEngagementPresentationSpeakersAsync(
		[HttpTrigger(AuthorizationLevel.Function, "get", Route = "engagements/{engagementId:int}/presentations/{presentationId:int}/speakers")] HttpRequestData request,
		int engagementId,
		int presentationId)
	{
		try
		{
			return await request.CreateResponseAsync(await _services.GetEngagementPresentationSpeakers(engagementId, presentationId), _jsonSerializerOptions);
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

	[Function("GetEngagementPresentationSpeaker")]
	public async Task<HttpResponseData> GetEngagementPresentationSpeakerAsync(
		[HttpTrigger(AuthorizationLevel.Function, "get", Route = "engagements/{engagementId:int}/presentations/{presentationId:int}/speakers/{speakerId:int}")] HttpRequestData request,
		int engagementId,
		int presentationId,
		int speakerId)
	{
		try
		{
			return await request.CreateResponseAsync(await _services.GetEngagementPresentationSpeakerAsync(engagementId, presentationId, speakerId), _jsonSerializerOptions);
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

	[Function("AddSpeakerToEngagementPresentation")]
	public async Task<HttpResponseData> AddSpeakerToEngagementPresentationAsync(
		[HttpTrigger(AuthorizationLevel.Function, "post", Route = "engagements/{engagementId:int}/presentations/{presentationId:int}/speakers/{speakerId:int}")] HttpRequestData request,
		int engagementId,
		int presentationId,
		int speakerId)
	{
		try
		{
			await _services.AddSpeakerToEngagementPresentationAsync(engagementId, presentationId, speakerId, request.GetBooleanQueryStringValue("isPrimary", false));
			return request.CreateCreatedResponse($"engagements/{engagementId}/presentations/{presentationId}/speakers/{speakerId}");
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

	[Function("RemoveSpeakerToEngagementPresentation")]
	public async Task<HttpResponseData> RemoveSpeakerFromEngagementPresentationAsync(
		[HttpTrigger(AuthorizationLevel.Function, "delete", Route = "engagements/{engagementId:int}/presentations/{presentationId:int}/speakers/{speakerId:int}")] HttpRequestData request,
		int engagementId,
		int presentationId,
		int speakerId)
	{
		try
		{
			await _services.RemoveSpeakerFromEngagementPresentationAsync(engagementId, presentationId, speakerId);
			return request.CreateResponse(HttpStatusCode.NoContent);
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
