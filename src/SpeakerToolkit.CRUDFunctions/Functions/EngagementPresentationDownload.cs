namespace TaleLearnCode.SpeakerToolkit.Functions;

public class EngagementPresentationDownload(ILoggerFactory loggerFactory, ConfigServices configServices, JsonSerializerOptions jsonSerializerOptions)
{

	private readonly ILogger _logger = loggerFactory.CreateLogger<SpeakerLink>();
	private readonly EngagementServices _services = new(configServices);
	private readonly JsonSerializerOptions _jsonSerializerOptions = jsonSerializerOptions;

	[Function("GetEngagementPresentationDownloads")]
	public async Task<HttpResponseData> GetEngagementPresentationDownloadsAsync(
				[HttpTrigger(AuthorizationLevel.Function, "get", Route = "engagements/{engagementId:int}/presentations/{presentationId:int}/downloads")] HttpRequestData request,
						int engagementId,
								int presentationId)
	{
		try
		{
			return await request.CreateResponseAsync(await _services.GetEngagementPresentationDownloads(engagementId, presentationId), _jsonSerializerOptions);
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

	[Function("GetEngagementPresentationDownload")]
	public async Task<HttpResponseData> GetEngagementPresentationDownloadAsync(
		[HttpTrigger(AuthorizationLevel.Function, "get", Route = "engagements/{engagementId:int}/presentations/{presentationId:int}/downloads/{downloadId:int}")] HttpRequestData request,
		int engagementId,
		int presentationId,
		int downloadId)
	{
		try
		{
			return await request.CreateResponseAsync(await _services.GetEngagementPresentationDownloadAsync(engagementId, presentationId, downloadId), _jsonSerializerOptions);
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

	[Function("AddDownloadToEngagementPresentation")]
	public async Task<HttpResponseData> AddDownloadToEngagementPresentationAsync(
		[HttpTrigger(AuthorizationLevel.Function, "post", Route = "engagements/{engagementId:int}/presentations/{presentationId:int}/downloads")] HttpRequestData request,
		int engagementId,
		int presentationId)
	{
		try
		{
			EngagementPresentationDownloadRequest engagementPresentationDownloadRequest = await request.GetRequestParameters2Async<EngagementPresentationDownloadRequest>(_jsonSerializerOptions) ?? throw new ArgumentException("Invalid body.");
			int engagementPresentationDownloadId = await _services.AddDownloadToEngagementPresentationAsync(engagementId, presentationId, engagementPresentationDownloadRequest);
			return request.CreateCreatedResponse($"engagements/{engagementId}/presentations/{presentationId}/downloads/{engagementPresentationDownloadId}");
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

	[Function("RemoveDownloadFromEngagementPresentation")]
	public async Task<HttpResponseData> RemoveDownloadFromEngagementPresentationAsync(
		[HttpTrigger(AuthorizationLevel.Function, "delete", Route = "engagements/{engagementId:int}/presentations/{presentationId:int}/downloads/{downloadId:int}")] HttpRequestData request,
		int engagementId,
		int presentationId,
		int downloadId)
	{
		try
		{
			await _services.RemoveDownloadFromEngagementPresentationAsync(engagementId, presentationId, downloadId);
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