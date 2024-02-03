namespace TaleLearnCode.SpeakerToolkit.Functions;

public class PresentationTag(ILoggerFactory loggerFactory, ConfigServices configServices, JsonSerializerOptions jsonSerializerOptions)
{

	private readonly ILogger _logger = loggerFactory.CreateLogger<SpeakerLink>();
	private readonly PresentationTagServices _services = new(configServices);
	private readonly JsonSerializerOptions _jsonSerializerOptions = jsonSerializerOptions;

	[Function("GetPresentationTags")]
	public async Task<HttpResponseData> GetPresentationTags(
		[HttpTrigger(AuthorizationLevel.Function, "get", Route = "presentations/{presentationId:int}/tags")] HttpRequestData request,
		int presentationId)
	{
		try
		{
			return await request.CreateResponseAsync(await _services.GetTagsAsync(presentationId), _jsonSerializerOptions);
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

	[Function("AddTagToPresentation")]
	public async Task<HttpResponseData> AddTagToPresentationAsync(
		[HttpTrigger(AuthorizationLevel.Function, "post", Route = "presentations/{presentationId}/tags/{tag}")] HttpRequestData request,
		int presentationId,
		string tag)
	{
		try
		{
			await _services.AddTagToPresentation(presentationId, tag);
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

	[Function("AddTagsToPresentation")]
	public async Task<HttpResponseData> AddTagsToPresentationAsync(
	[HttpTrigger(AuthorizationLevel.Function, "post", Route = "presentations/{presentationId}/tags")] HttpRequestData request,
	int presentationId)
	{
		try
		{
			await _services.AddTagsToPresentation(
				presentationId,
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

	[Function("RemoveTagFromPresentation")]
	public async Task<HttpResponseData> RemoveTagFromPresentationAsync(
		[HttpTrigger(AuthorizationLevel.Function, "delete", Route = "presentations/{presentationId}/tags/{tag}")] HttpRequestData request,
		int presentationId,
		string tag)
	{
		try
		{
			await _services.RemoveTagFromPresentation(presentationId, tag);
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

	[Function("RemoveTagsFromPresentation")]
	public async Task<HttpResponseData> RemoveTagsFromPresentationAsync(
		[HttpTrigger(AuthorizationLevel.Function, "delete", Route = "presentations/{presentationId}/tags")] HttpRequestData request,
		int presentationId)
	{
		try
		{
			await _services.RemoveTagsFromPresentation(presentationId);
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