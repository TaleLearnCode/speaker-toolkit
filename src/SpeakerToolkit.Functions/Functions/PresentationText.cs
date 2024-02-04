namespace TaleLearnCode.SpeakerToolkit.Functions;

public class PresentationText(ILoggerFactory loggerFactory, ConfigServices configServices, JsonSerializerOptions jsonSerializerOptions)
{

	private readonly ILogger _logger = loggerFactory.CreateLogger<SpeakerLink>();
	private readonly PresentationServices _services = new(configServices);
	private readonly JsonSerializerOptions _jsonSerializerOptions = jsonSerializerOptions;


	[Function("GetPresentationText")]
	public async Task<HttpResponseData> GetPresentationTextAsync(
		[HttpTrigger(AuthorizationLevel.Function, "get", Route = "presentations/{presentationId:int}/text/{languageCode}")] HttpRequestData request,
		int presentationId,
		string languageCode)
	{
		try
		{
			return await request.CreateResponseAsync(await _services.GetPresentationTextAsync(presentationId, languageCode), _jsonSerializerOptions);
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

	[Function("GetPresentationTexts")]
	public async Task<HttpResponseData> GetPresentationTextsAsync(
		[HttpTrigger(AuthorizationLevel.Function, "get", Route = "presentations/{presentationId:int}/texts")] HttpRequestData request,
		int presentationId)
	{
		try
		{
			return await request.CreateResponseAsync(await _services.GetPresentationTextsAsync(presentationId), _jsonSerializerOptions);
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

	[Function("AddPresentationText")]
	public async Task<HttpResponseData> AddPresentationTextAsync(
		[HttpTrigger(AuthorizationLevel.Function, "post", Route = "presentations/{presentationId:int}/text/{languageCode}")] HttpRequestData request,
		int presentationId,
		string languageCode)
	{
		try
		{
			PresentationTextRequest presentationTextRequest
				= await request.GetRequestParameters2Async<PresentationTextRequest>(_jsonSerializerOptions)
				?? throw new ArgumentNullException(nameof(request), "Invalid request body.");
			await _services.AddPresentationTextAsync(presentationId, languageCode, presentationTextRequest);
			return request.CreateCreatedResponse($"presentations/{presentationId}/text/{languageCode}");
		}
		catch (Exception ex) when (ex is ArgumentOutOfRangeException || ex is ObjectAlreadyExistsException)
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

	[Function("UpdatePresentationText")]
	public async Task<HttpResponseData> UpdatePresentationTextAsync(
		[HttpTrigger(AuthorizationLevel.Function, "put", Route = "presentations/{presentationId:int}/text/{languageCode}")] HttpRequestData request,
		int presentationId,
		string languageCode)
	{
		try
		{
			PresentationTextRequest presentationTextRequest
				= await request.GetRequestParameters2Async<PresentationTextRequest>(_jsonSerializerOptions)
				?? throw new ArgumentNullException(nameof(request), "Invalid request body.");
			await _services.UpdatePresentationTextAsync(presentationId, languageCode, presentationTextRequest);
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

	[Function("RemovePresentationText")]
	public async Task<HttpResponseData> RemovePresentationTextAsync(
		[HttpTrigger(AuthorizationLevel.Function, "delete", Route = "presentations/{presentationId:int}/text/{languageCode}")] HttpRequestData request,
		int presentationId,
		string languageCode)
	{
		try
		{
			await _services.RemovePresentationTextAsync(presentationId, languageCode);
			return request.CreateOkResponse();
		}
		catch (Exception ex) when (ex is ArgumentOutOfRangeException)
		{
			return request.CreateNotFoundResponse(ex);
		}
		catch (Exception ex) when (ex is ArgumentException || ex.Message == "Cannot remove the only presentation text for a presentation.")
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