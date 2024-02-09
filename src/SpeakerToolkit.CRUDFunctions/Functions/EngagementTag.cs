namespace TaleLearnCode.SpeakerToolkit.Functions;

public class EngagementTag(ILoggerFactory loggerFactory, ConfigServices configServices, JsonSerializerOptions jsonSerializerOptions)
{

	private readonly ILogger _logger = loggerFactory.CreateLogger<SpeakerLink>();
	private readonly EngagementTagServices _services = new(configServices);
	private readonly JsonSerializerOptions _jsonSerializerOptions = jsonSerializerOptions;

	[Function("GetEngagementTags")]
	public async Task<HttpResponseData> GetEngagementTags(
		[HttpTrigger(AuthorizationLevel.Function, "get", Route = "Engagements/{EngagementId:int}/tags")] HttpRequestData request,
		int EngagementId)
	{
		try
		{
			return await request.CreateResponseAsync(await _services.GetTagsAsync(EngagementId), _jsonSerializerOptions);
		}
		catch (Exception ex) when (ex is ArgumentOutOfRangeException)
		{
			return request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
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

	[Function("AddTagToEngagement")]
	public async Task<HttpResponseData> AddTagToEngagementAsync(
		[HttpTrigger(AuthorizationLevel.Function, "post", Route = "Engagements/{EngagementId}/tags/{tag}")] HttpRequestData request,
		int EngagementId,
		string tag)
	{
		try
		{
			await _services.AddTagToEngagement(EngagementId, tag);
			return request.CreateResponse(HttpStatusCode.NoContent);
		}
		catch (Exception ex) when (ex is ArgumentOutOfRangeException)
		{
			return request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
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

	[Function("AddTagsToEngagement")]
	public async Task<HttpResponseData> AddTagsToEngagementAsync(
	[HttpTrigger(AuthorizationLevel.Function, "post", Route = "Engagements/{EngagementId}/tags")] HttpRequestData request,
	int EngagementId)
	{
		try
		{
			await _services.AddTagsToEngagement(
				EngagementId,
				await request.GetRequestParameters2Async<AddTagsRequest>(_jsonSerializerOptions)
					?? throw new ArgumentNullException(nameof(request), "Invalid request body."));
			return request.CreateResponse(HttpStatusCode.NoContent);
		}
		catch (Exception ex) when (ex is ArgumentOutOfRangeException)
		{
			return request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
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

	[Function("RemoveTagFromEngagement")]
	public async Task<HttpResponseData> RemoveTagFromEngagementAsync(
		[HttpTrigger(AuthorizationLevel.Function, "delete", Route = "Engagements/{EngagementId}/tags/{tag}")] HttpRequestData request,
		int EngagementId,
		string tag)
	{
		try
		{
			await _services.RemoveTagFromEngagement(EngagementId, tag);
			return request.CreateResponse(HttpStatusCode.OK);
		}
		catch (Exception ex) when (ex is ArgumentOutOfRangeException)
		{
			return request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
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

	[Function("RemoveTagsFromEngagement")]
	public async Task<HttpResponseData> RemoveTagsFromEngagementAsync(
		[HttpTrigger(AuthorizationLevel.Function, "delete", Route = "Engagements/{EngagementId}/tags")] HttpRequestData request,
		int EngagementId)
	{
		try
		{
			await _services.RemoveTagsFromEngagement(EngagementId);
			return request.CreateOkResponse();
		}
		catch (Exception ex) when (ex is ArgumentOutOfRangeException)
		{
			return request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
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