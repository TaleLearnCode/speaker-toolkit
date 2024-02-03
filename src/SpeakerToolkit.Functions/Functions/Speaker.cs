namespace TaleLearnCode.SpeakerToolkit.Functions;

public class Speaker(ILoggerFactory loggerFactory, ConfigServices configServices, JsonSerializerOptions jsonSerializerOptions)
{

	private readonly ILogger _logger = loggerFactory.CreateLogger<Speaker>();
	private readonly SpeakerServices _services = new(configServices);
	private readonly JsonSerializerOptions _jsonSerializerOptions = jsonSerializerOptions;

	[Function("GetSpeaker")]
	public async Task<HttpResponseData> GetSpeakerAsync(
		[HttpTrigger(AuthorizationLevel.Function, "get", Route = "speakers/{speakerId:int}")] HttpRequestData request,
		int speakerId)
	{
		try
		{

			GetSpeakerOptions? options = await request.GetRequestParameters2Async<GetSpeakerOptions?>();

			return await request.CreateResponseAsync(
				await _services.GetSpeakerAsync(
					speakerId,
					await request.GetRequestParameters2Async<GetSpeakerOptions?>()),
				_jsonSerializerOptions);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Unhandled Exception: {errorMessage}", ex.Message);
			return request.CreateErrorResponse(ex);
		}
	}

	[Function("CreateSpeaker")]
	public async Task<HttpResponseData> CreateSpeakerAsync(
				[HttpTrigger(AuthorizationLevel.Function, "post", Route = "speakers")] HttpRequestData request)
	{
		try
		{
			SpeakerRequest? speakerRequest
				= await request.GetRequestParameters2Async<SpeakerRequest>(_jsonSerializerOptions) ?? throw new ArgumentNullException(nameof(request), "Invalid request body.");
			SpeakerResponse? speakerResponse = await _services.CreateSpeakerAsync(speakerRequest);
			return request.CreateCreatedResponse($"speakers/{speakerResponse?.Id}");
		}
		catch (Exception ex) when (ex is ArgumentException || ex is ObjectAlreadyExistsException)
		{
			_logger.LogError(ex, "Unhandled Exception: {errorMessage}", ex.Message);
			return request.CreateErrorResponse(ex);
		}
	}

	[Function("UpdateSpeaker")]
	public async Task<HttpResponseData> UpdateSpeakerAsync(
				[HttpTrigger(AuthorizationLevel.Function, "put", Route = "speakers/{speakerId:int}")] HttpRequestData request,
						int speakerId)
	{
		try
		{
			if (speakerId <= 0)
				throw new ArgumentOutOfRangeException(nameof(speakerId), "Speaker ID must be greater than zero.");
			SpeakerRequest? speakerRequest
				= await request.GetRequestParameters2Async<SpeakerRequest>(_jsonSerializerOptions) ?? throw new ArgumentNullException(nameof(request), "Invalid request body.");
			SpeakerResponse? speakerResponse = await _services.UpdateSpeakerAsync(speakerId, speakerRequest);
			return request.CreateResponse(HttpStatusCode.NoContent);
		}
		catch (Exception ex) when (ex is ArgumentException || ex is ObjectAlreadyExistsException)
		{
			_logger.LogError(ex, "Unhandled Exception: {errorMessage}", ex.Message);
			return request.CreateErrorResponse(ex);
		}
	}

}