namespace TaleLearnCode.SpeakerToolkit.Functions;

public class LearningObjective(ILoggerFactory loggerFactory, ConfigServices configServices, JsonSerializerOptions jsonSerializerOptions)
{

	private readonly ILogger _logger = loggerFactory.CreateLogger<SpeakerLink>();
	private readonly PresentationServices _services = new(configServices);
	private readonly JsonSerializerOptions _jsonSerializerOptions = jsonSerializerOptions;

	[Function("GetLearningObjective")]
	public async Task<HttpResponseData> GetLearningObjectiveAsync(
		[HttpTrigger(AuthorizationLevel.Function, "get", Route = "presentations/{presentationId:int}/learning-objectives/{languageCode}/{sortOrder:int}")] HttpRequestData request,
		int presentationId,
		string languageCode,
		int sortOrder)
	{
		try
		{
			return await request.CreateResponseAsync(await _services.GetLearningObjectiveAsync(presentationId, languageCode, sortOrder), _jsonSerializerOptions);
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

	[Function("GetLearningObjectives")]
	public async Task<HttpResponseData> GetLearningObjectivesAsync(
		[HttpTrigger(AuthorizationLevel.Function, "get", Route = "presentations/{presentationId:int}/learning-objectives/{languageCode}")] HttpRequestData request,
		int presentationId,
		string languageCode)
	{
		try
		{
			return await request.CreateResponseAsync(await _services.GetLearningObjectivesAsync(presentationId, languageCode), _jsonSerializerOptions);
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

	[Function("AddLearningObjective")]
	public async Task<HttpResponseData> AddLearningObjectiveAsync(
		[HttpTrigger(AuthorizationLevel.Function, "post", Route = "presentations/{presentationId:int}/learning-objectives/{languageCode}")] HttpRequestData request,
		int presentationId,
		string languageCode)
	{
		try
		{
			string requestBody = await new StreamReader(request.Body).ReadToEndAsync();
			int? sortOrder = request.GetNullableInt32QueryStringValue("sortOrder");
			ArgumentNullException.ThrowIfNull(requestBody, nameof(request.Body));
			LearningObjectiveResponse response = await _services.AddLearningObjectiveAsync(presentationId, languageCode, requestBody, sortOrder);
			return request.CreateCreatedResponse($"presentations/{presentationId}/learning-objectives/{languageCode}/{response.SortOrder}");
		}
		catch (Exception ex) when (ex is ArgumentOutOfRangeException)
		{
			return request.CreateNotFoundResponse(ex);
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

	[Function("UpdateLearningObjective")]
	public async Task<HttpResponseData> UpdateLearningObjectiveAsync(
		[HttpTrigger(AuthorizationLevel.Function, "put", Route = "presentations/{presentationId:int}/learning-objectives/{languageCode}/{sortOrder:int}")] HttpRequestData request,
		int presentationId,
		string languageCode,
		int sortOrder)
	{
		try
		{
			string requestBody = await new StreamReader(request.Body).ReadToEndAsync() ?? throw new ArgumentNullException(nameof(request), "Invalid request body.");
			int? newSortOrder = request.GetNullableInt32QueryStringValue("sortOrder");
			await _services.UpdateLearningObjectiveAsync(presentationId, languageCode, sortOrder, requestBody);
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

	[Function("RemoveLearningObjective")]
	public async Task<HttpResponseData> RemoveLearningObjectiveAsync(
		[HttpTrigger(AuthorizationLevel.Function, "delete", Route = "presentations/{presentationId:int}/learning-objectives/{languageCode}/{sortOrder:int}")] HttpRequestData request,
		int presentationId,
		string languageCode,
		int sortOrder)
	{
		try
		{
			await _services.RemoveLearningObjectiveAsync(presentationId, languageCode, sortOrder);
			return request.CreateResponse(HttpStatusCode.NoContent);
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