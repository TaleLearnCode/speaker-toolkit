namespace TaleLearnCode.SpeakerToolkit.Functions;

public class Engagement(ILoggerFactory loggerFactory, ConfigServices configServices, JsonSerializerOptions jsonSerializerOptions)
{

	private readonly ILogger _logger = loggerFactory.CreateLogger<Speaker>();
	private readonly EngagementServices _services = new(configServices);
	private readonly JsonSerializerOptions _jsonSerializerOptions = jsonSerializerOptions;

	[Function("GetEngagement")]
	public async Task<HttpResponseData> GetEngagementAsync(
		[HttpTrigger(AuthorizationLevel.Function, "get", Route = "engagements/{engagementId:int}")] HttpRequestData request,
		int engagementId)
	{
		try
		{
			return await request.CreateResponseAsync(await _services.GetEngagementAsync(engagementId, request.GetQueryStringValue("languageCode")), _jsonSerializerOptions);
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

	[Function("GetEngagements")]
	public async Task<HttpResponseData> GetEngagementsAsync(
		[HttpTrigger(AuthorizationLevel.Function, "get", Route = "engagements")] HttpRequestData request)
	{
		try
		{
			return await request.CreateResponseAsync(await _services.GetEngagementsAsync(request.GetQueryStringValue("languageCode")), _jsonSerializerOptions);
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

	[Function("CreateEngagement")]
	public async Task<HttpResponseData> CreateEngagementAsync(
		[HttpTrigger(AuthorizationLevel.Function, "post", Route = "engagements")] HttpRequestData request)
	{
		try
		{
			EngagementRequest engagementRequest = await request.GetRequestParameters2Async<EngagementRequest>(_jsonSerializerOptions) ?? throw new ArgumentException("Unable to read request body or the request body is invalid.");
			int engagementId = await _services.CreateEngagementAsync(engagementRequest);
			return request.CreateCreatedResponse($"engagements/{engagementId}");
			//return request.CreateResponse(HttpStatusCode.Created);
		}
		catch (Exception ex) when (ex is ArgumentException)
		{
			//return request.CreateBadRequestResponse(ex);
			return request.CreateResponse(HttpStatusCode.BadRequest);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Unhandled Exception: {errorMessage}", ex.Message);
			return request.CreateErrorResponse(ex);
		}
	}

	[Function("UpdateEngagement")]
	public async Task<HttpResponseData> UpdateEngagementAsync(
				[HttpTrigger(AuthorizationLevel.Function, "put", Route = "engagements/{engagementId:int}")] HttpRequestData request,
						int engagementId)
	{
		try
		{
			EngagementRequest engagementRequest = await request.GetRequestParameters2Async<EngagementRequest>(_jsonSerializerOptions) ?? throw new ArgumentException("Unable to read request body or the request body is invalid.");
			await _services.UpdateEngagementAsync(engagementId, engagementRequest);
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

	[Function("DeleteEngagement")]
	public async Task<HttpResponseData> DeleteEngagementAsync(
		[HttpTrigger(AuthorizationLevel.Function, "delete", Route = "engagements/{engagementId:int}")] HttpRequestData request,
		int engagementId)
	{
		try
		{
			await _services.DeleteEngagementAsync(engagementId);
			return request.CreateOkResponse();
		}
		catch (Exception ex) when (ex is ArgumentOutOfRangeException)
		{
			return request.CreateNotFoundResponse(ex);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Unhandled Exception: {errorMessage}", ex.Message);
			return request.CreateErrorResponse(ex);
		}
	}

}
