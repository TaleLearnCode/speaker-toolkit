namespace TaleLearnCode.SpeakerToolkit.Functions;

public class RelatedPresentation(ILoggerFactory loggerFactory, ConfigServices configServices, JsonSerializerOptions jsonSerializerOptions)
{

	private readonly ILogger _logger = loggerFactory.CreateLogger<SpeakerLink>();
	private readonly PresentationServices _services = new(configServices);
	private readonly JsonSerializerOptions _jsonSerializerOptions = jsonSerializerOptions;

	[Function("GetRelatedPresentations")]
	public async Task<HttpResponseData> GetRelatedPresentationsAsync(
		[HttpTrigger(AuthorizationLevel.Function, "get", Route = "presentations/{presentationId:int}/related")] HttpRequestData request,
		int presentationId)
	{
		try
		{
			return await request.CreateResponseAsync(await _services.GetRelatedPresentationsAsync(presentationId, request.GetQueryStringValue("languageCode")), _jsonSerializerOptions);
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

	[Function("AddRelatedPresentation")]
	public async Task<HttpResponseData> AddRelatedPresentationAsync(
		[HttpTrigger(AuthorizationLevel.Function, "post", Route = "presentations/{presentationId:int}/related/{relatedPresentationId:int}")] HttpRequestData request,
		int presentationId,
		int relatedPresentationId)
	{
		try
		{
			int? sortOrder = request.GetNullableInt32QueryStringValue("sortOrder");
			return await request.CreateResponseAsync(await _services.AddRelatedPresentation(presentationId, relatedPresentationId, sortOrder), _jsonSerializerOptions);
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

	[Function("RemoveRelatedPresentation")]
	public async Task<HttpResponseData> RemoveRelatedPresentationAsync(
		[HttpTrigger(AuthorizationLevel.Function, "delete", Route = "presentations/{presentationId:int}/related/{relatedPresentationId:int}")] HttpRequestData request,
		int presentationId,
		int relatedPresentationId)
	{
		try
		{
			return await request.CreateResponseAsync(await _services.RemoveRelatedPresentation(presentationId, relatedPresentationId), _jsonSerializerOptions);
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