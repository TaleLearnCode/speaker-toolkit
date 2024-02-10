namespace TaleLearnCode.SpeakerToolkit.Functions;

public class EngagementPresentation(ILoggerFactory loggerFactory, ConfigServices configServices, JsonSerializerOptions jsonSerializerOptions)
{

	private readonly ILogger _logger = loggerFactory.CreateLogger<Country>();
	private readonly EngagementServices _services = new(configServices);
	private readonly JsonSerializerOptions _jsonSerializerOptions = jsonSerializerOptions;

	[Function("GetEngagementPresentations")]
	public async Task<HttpResponseData> GetEngagementPresentationsAsync(
		[HttpTrigger(AuthorizationLevel.Function, "get", Route = "engagements/{engagementId:int}/presentations")] HttpRequestData request,
		int engagementId)
	{
		try
		{
			return await request.CreateResponseAsync(await _services.GetEngagementPresentationsAsync(engagementId, request.GetQueryStringValue("languageCode")), _jsonSerializerOptions);
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

	[Function("GetEngagementPresentation")]
	public async Task<HttpResponseData> GetEngagementPresentationAsync(
		[HttpTrigger(AuthorizationLevel.Function, "get", Route = "engagements/{engagementId:int}/presentations/{presentationId:int}")] HttpRequestData request,
		int engagementId,
		int presentationId)
	{
		try
		{
			return await request.CreateResponseAsync(await _services.GetEngagementPresentationAsync(engagementId, presentationId, request.GetQueryStringValue("languageCode")), _jsonSerializerOptions);
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

	[Function("AddPresentationToEngagement")]
	public async Task<HttpResponseData> AddPresentationToEngagementAsync(
		[HttpTrigger(AuthorizationLevel.Function, "post", Route = "engagements/{engagementId:int}/presentations/{presentationId:int}")] HttpRequestData request,
		int engagementId,
		int presentationId)
	{
		try
		{
			EngagementPresentationRequest? engagementPresentationRequest
				= await request.GetRequestParameters2Async<EngagementPresentationRequest>(_jsonSerializerOptions) ?? throw new ArgumentNullException(nameof(request), "Invalid request body.");
			await _services.AddPresentationToEngagementAsync(engagementId, presentationId, engagementPresentationRequest);
			return request.CreateCreatedResponse($"engagements/{engagementId}/presentations/{presentationId}");
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

	[Function("UpdateEngagementPresentation")]
	public async Task<HttpResponseData> UpdatePresentationInEngagementAsync(
		[HttpTrigger(AuthorizationLevel.Function, "put", Route = "engagements/{engagementId:int}/presentations/{presentationId:int}")] HttpRequestData request,
		int engagementId,
		int presentationId)
	{
		try
		{
			EngagementPresentationRequest? engagementPresentationRequest
				= await request.GetRequestParameters2Async<EngagementPresentationRequest>(_jsonSerializerOptions) ?? throw new ArgumentNullException(nameof(request), "Invalid request body.");
			await _services.UpdateEngagementPresentation(engagementId, presentationId, engagementPresentationRequest);
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

	[Function("RemovePresentationFromEngagement")]
	public async Task<HttpResponseData> RemovePresentationFromEngagementAsync(
		[HttpTrigger(AuthorizationLevel.Function, "delete", Route = "engagements/{engagementId:int}/presentations/{presentationId:int}")] HttpRequestData request,
		int engagementId,
		int presentationId)
	{
		try
		{
			await _services.RemovePresentationFromEngagementAsync(engagementId, presentationId);
			return request.CreateResponse(HttpStatusCode.OK);
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