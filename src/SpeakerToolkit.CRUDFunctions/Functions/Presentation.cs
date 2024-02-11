namespace TaleLearnCode.SpeakerToolkit.Functions;

public class Presentation(ILoggerFactory loggerFactory, ConfigServices configServices, JsonSerializerOptions jsonSerializerOptions)
{

	private readonly ILogger _logger = loggerFactory.CreateLogger<Speaker>();
	private readonly PresentationServices _services = new(configServices);
	private readonly JsonSerializerOptions _jsonSerializerOptions = jsonSerializerOptions;

	[Function("GetPresentation")]
	public async Task<HttpResponseData> GetPresentationAsync(
		[HttpTrigger(AuthorizationLevel.Function, "get", Route = "presentations/{presentationId:int}")] HttpRequestData request,
		int presentationId)
	{
		try
		{
			return await request.CreateResponseAsync(await _services.GetPresentationAsync(GetPresentationOptions(request, presentationId)), _jsonSerializerOptions);
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

	[Function("GetPresentations")]
	public async Task<HttpResponseData> GetPresentationsAsync(
		[HttpTrigger(AuthorizationLevel.Function, "get", Route = "presentations")] HttpRequestData request)
	{
		try
		{
			GetPresentationOptions? options = await request.GetRequestParameters2Async<GetPresentationOptions?>();
			return await request.CreateResponseAsync(await _services.GetPresentationsAsync(GetPresentationOptions(request)), _jsonSerializerOptions);
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

	[Function("CreatePresentation")]
	public async Task<HttpResponseData> CreatePresentationAsync(
		[HttpTrigger(AuthorizationLevel.Function, "post", Route = "presentations")] HttpRequestData request)
	{
		try
		{
			PresentationRequest presentationRequest
				= await request.GetRequestParameters2Async<PresentationRequest>(_jsonSerializerOptions) ?? throw new ArgumentNullException(nameof(request), "Invalid request body.");
			int presentationId = await _services.CreatePresentationAsync(presentationRequest);
			return request.CreateCreatedResponse($"presentations/{presentationId}");
		}
		catch (Exception ex) when (ex is ArgumentException || ex is ObjectAlreadyExistsException)
		{
			_logger.LogError(ex, "Unhandled Exception: {errorMessage}", ex.Message);
			return request.CreateErrorResponse(ex);
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

	[Function("UpdatePresentation")]
	public async Task<HttpResponseData> UpdatePresentationAsync(
		[HttpTrigger(AuthorizationLevel.Function, "put", Route = "presentations/{presentationId:int}")] HttpRequestData request,
		int presentationId)
	{
		try
		{
			if (presentationId <= 0)
				throw new ArgumentOutOfRangeException(nameof(presentationId), "Presentation ID must be greater than zero.");
			PresentationRequest? presentationRequest
				= await request.GetRequestParameters2Async<PresentationRequest>(_jsonSerializerOptions) ?? throw new ArgumentNullException(nameof(request), "Invalid request body.");
			await _services.UpdatePresentationAsync(presentationId, presentationRequest);
			return request.CreateResponse(HttpStatusCode.NoContent);
		}
		catch (Exception ex) when (ex is ArgumentException || ex is ObjectAlreadyExistsException)
		{
			_logger.LogError(ex, "Unhandled Exception: {errorMessage}", ex.Message);
			return request.CreateErrorResponse(ex);
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

	[Function("DeletePresentation")]
	public async Task<HttpResponseData> DeletePresentationAsync(
		[HttpTrigger(AuthorizationLevel.Function, "delete", Route = "presentations/{presentationId:int}")] HttpRequestData request,
		int presentationId)
	{
		try
		{
			await _services.DeletePresentationAsync(presentationId);
			return request.CreateOkResponse();
			//return request.CreateResponse(HttpStatusCode.OK);
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

	private static GetPresentationOptions GetPresentationOptions(HttpRequestData request, int? presentationId = null)
		=> new()
		{
			PresentationId = presentationId,
			IncludePresentationTexts = request.GetBooleanQueryStringValue("includePresentationTexts", false),
			IncludeRelatedPresentations = request.GetBooleanQueryStringValue("includeRelatedPresentations", false),
			IncludeSpeakers = request.GetBooleanQueryStringValue("includeSpeakers", false),
			IncludeTags = request.GetBooleanQueryStringValue("includeTags", false)
		};

}