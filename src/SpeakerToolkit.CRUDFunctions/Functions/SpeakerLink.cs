using TaleLearnCode.SpeakerToolkit.Exceptions;

namespace TaleLearnCode.SpeakerToolkit.Functions;

public class SpeakerLink(ILoggerFactory loggerFactory, ConfigServices configServices, JsonSerializerOptions jsonSerializerOptions)
{

	private readonly ILogger _logger = loggerFactory.CreateLogger<SpeakerLink>();
	private readonly SpeakerLinkServices _services = new(configServices);
	private readonly JsonSerializerOptions _jsonSerializerOptions = jsonSerializerOptions;

	[Function("GetSpeakerLinks")]
	public async Task<HttpResponseData> GetSpeakerLinksAsync(
				[HttpTrigger(AuthorizationLevel.Function, "get", Route = "speakers/{speakerId:int}/links")] HttpRequestData request,
				int speakerId)
	{
		try
		{
			return await request.CreateResponseAsync(await _services.GetSpeakerLinksAsync(speakerId), _jsonSerializerOptions);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Unhandled Exception: {errorMessage}", ex.Message);
			return request.CreateErrorResponse(ex);
		}
	}

	[Function("GetSpeakerLink")]
	public async Task<HttpResponseData> GetSpeakerLinkAsync(
		[HttpTrigger(AuthorizationLevel.Function, "get", Route = "speakers/{speakerId:int}/links/{speakerLinkId:int}")] HttpRequestData request,
		int speakerId,
		int speakerLinkId)
	{
		try
		{
			return await request.CreateResponseAsync(await _services.GetSpeakerLinkAsync(speakerId, speakerLinkId), _jsonSerializerOptions);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Unhandled Exception: {errorMessage}", ex.Message);
			return request.CreateErrorResponse(ex);
		}
	}

	[Function("CreateSpeakerLink")]
	public async Task<HttpResponseData> CreateSpeakerLinkAsync(
		[HttpTrigger(AuthorizationLevel.Function, "post", Route = "speakers/{speakerId}/links")] HttpRequestData request,
		int speakerId)
	{
		try
		{
			if (speakerId <= 0)
				throw new ArgumentOutOfRangeException(nameof(speakerId), "Speaker ID must be greater than zero.");
			SpeakerLinkRequest? speakerLinkRequest
				= await request.GetRequestParameters2Async<SpeakerLinkRequest>(_jsonSerializerOptions) ?? throw new ArgumentNullException(nameof(request), "Invalid request body.");
			SpeakerLinkResponse? speakerLinkResponse = await _services.CreateSpeakerLinkAsync(speakerId, speakerLinkRequest);
			return request.CreateCreatedResponse($"speakers/{speakerId}/links/{speakerLinkResponse?.Id}");
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

	[Function("UpdateSpeakerLink")]
	public async Task<HttpResponseData> UpdateSpeakerLinkAsync(
		[HttpTrigger(AuthorizationLevel.Function, "put", Route = "speakers/{speakerId}/links/{speakerLinkId}")] HttpRequestData request,
		int speakerId,
		int speakerLinkId)
	{
		try
		{
			if (speakerId <= 0)
				throw new ArgumentOutOfRangeException(nameof(speakerId), "Speaker ID must be greater than zero.");
			if (speakerLinkId <= 0)
				throw new ArgumentOutOfRangeException(nameof(speakerId), "Speaker Link ID must be greater than zero.");
			SpeakerLinkRequest? speakerLinkRequest
				= await request.GetRequestParameters2Async<SpeakerLinkRequest>(_jsonSerializerOptions) ?? throw new ArgumentNullException(nameof(request), "Invalid request body.");
			SpeakerLinkResponse? speakerLinkResponse = await _services.UpdateSpeakerLinkAsync(speakerId, speakerLinkId, speakerLinkRequest);
			return request.CreateResponse(HttpStatusCode.NoContent);
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

}