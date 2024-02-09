namespace TaleLearnCode.SpeakerToolkit.Functions;

public class Tag(ILoggerFactory loggerFactory, ConfigServices configServices, JsonSerializerOptions jsonSerializerOptions)
{

	private readonly ILogger _logger = loggerFactory.CreateLogger<SpeakerLink>();
	private readonly TagServices _services = new(configServices);
	private readonly JsonSerializerOptions _jsonSerializerOptions = jsonSerializerOptions;

	[Function("GetTags")]
	public async Task<HttpResponseData> GetTagsAsync([HttpTrigger(AuthorizationLevel.Function, "get", Route = "tags")] HttpRequestData request)
	{
		try
		{
			return await request.CreateResponseAsync(await _services.GetTagsAsync(), _jsonSerializerOptions);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Unhandled Exception: {errorMessage}", ex.Message);
			return request.CreateErrorResponse(ex);
		}
	}

	[Function("GetTag")]
	public async Task<HttpResponseData> GetTagAsync(
		[HttpTrigger(AuthorizationLevel.Function, "get", Route = "tags/{tagId:int}")] HttpRequestData request,
		int tagId)
	{
		try
		{
			return await request.CreateResponseAsync(await _services.GetTagAsync(tagId), _jsonSerializerOptions);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Unhandled Exception: {errorMessage}", ex.Message);
			return request.CreateErrorResponse(ex);
		}
	}

	[Function("CreateTag")]
	public async Task<HttpResponseData> CreateTagAsync(
				[HttpTrigger(AuthorizationLevel.Function, "post", Route = "tags")] HttpRequestData request)
	{
		try
		{
			TagRequest? tagRequest
				= await request.GetRequestParameters2Async<TagRequest>(_jsonSerializerOptions) ?? throw new ArgumentNullException(nameof(request), "Invalid request body.");
			TagResponse? tagResponse = await _services.CreateTagAsync(tagRequest);
			return request.CreateCreatedResponse($"tags/{tagResponse?.Id}");
		}
		catch (Exception ex) when (ex is ArgumentException || ex is ObjectAlreadyExistsException)
		{
			_logger.LogError(ex, "Unhandled Exception: {errorMessage}", ex.Message);
			return request.CreateErrorResponse(ex);
		}
	}

	[Function("UpdateTag")]
	public async Task<HttpResponseData> UpdateTagAsync(
		[HttpTrigger(AuthorizationLevel.Function, "put", Route = "tags/{tagId:int}")] HttpRequestData request,
		int tagId)
	{
		try
		{
			TagRequest? tagRequest
				= await request.GetRequestParameters2Async<TagRequest>(_jsonSerializerOptions) ?? throw new ArgumentNullException(nameof(request), "Invalid request body.");
			TagResponse? tagResponse = await _services.UpdateTagAsync(tagId, tagRequest);
			return request.CreateResponse(HttpStatusCode.NoContent);
		}
		catch (Exception ex) when (ex is ArgumentException || ex is ObjectAlreadyExistsException)
		{
			_logger.LogError(ex, "Unhandled Exception: {errorMessage}", ex.Message);
			return request.CreateErrorResponse(ex);
		}
	}

}