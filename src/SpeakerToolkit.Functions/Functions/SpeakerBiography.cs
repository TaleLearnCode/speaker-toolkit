using System.Net;
using TaleLearnCode.SpeakerToolkit.Exceptions;
using TaleLearnCode.SpeakerToolkit.Requests;
using TaleLearnCode.SpeakerToolkit.Responses;

namespace TaleLearnCode.SpeakerToolkit.Functions;

public class SpeakerBiography(ILoggerFactory loggerFactory, ConfigServices configServices, JsonSerializerOptions jsonSerializerOptions)
{

	private readonly ILogger _logger = loggerFactory.CreateLogger<SpeakerBiography>();
	private readonly SpeakerBiographyServices _services = new(configServices);
	private readonly JsonSerializerOptions _jsonSerializerOptions = jsonSerializerOptions;

	[Function("GetSpeakerBiographies")]
	public async Task<HttpResponseData> GetSpeakerBiographiesAsync(
		[HttpTrigger(AuthorizationLevel.Function, "get", Route = "speakers/{speakerId:int}/biographies")] HttpRequestData request,
		int speakerId)
	{
		try
		{
			return await request.CreateResponseAsync(await _services.GetSpeakerBiographiesAsync(speakerId), _jsonSerializerOptions);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Unhandled Exception: {errorMessage}", ex.Message);
			return request.CreateErrorResponse(ex);
		}
	}

	[Function("GetSpeakerBiography")]
	public async Task<HttpResponseData> GetSpeakerBiographyAsync(
				[HttpTrigger(AuthorizationLevel.Function, "get", Route = "speakers/{speakerId:int}/biographies/{languageCode}")] HttpRequestData request,
				int speakerId,
				string languageCode)
	{
		try
		{
			return await request.CreateResponseAsync(await _services.GetSpeakerBiographyAsync(speakerId, languageCode), _jsonSerializerOptions);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Unhandled Exception: {errorMessage}", ex.Message);
			return request.CreateErrorResponse(ex);
		}
	}

	[Function("CreateSpeakerBiography")]
	public async Task<HttpResponseData> CreateSpeakerBiographyAsync(
		[HttpTrigger(AuthorizationLevel.Function, "post", Route = "speakers/{speakerId}/biographies/{languageCode}")] HttpRequestData request,
		int speakerId,
		string languageCode)
		=> await UpsertSpeakerBiographyAsync(request, speakerId, languageCode, new() { UpdateIfExists = false });

	[Function("UpdateSpeakerBiography")]
	public async Task<HttpResponseData> UpdateCreateBiographyAsync(
		[HttpTrigger(AuthorizationLevel.Function, "put", Route = "speakers/{speakerId}/biographies/{languageCode}")] HttpRequestData request,
				int speakerId,
				string languageCode)
		=> await UpsertSpeakerBiographyAsync(request, speakerId, languageCode, new() { CreateIfNotExists = false });

	private async Task<HttpResponseData> UpsertSpeakerBiographyAsync(
		HttpRequestData request,
		int speakerId,
		string languageCode,
		UpsertOptions options)
	{
		try
		{
			if (speakerId <= 0)
				throw new ArgumentOutOfRangeException(nameof(speakerId), "Speaker ID must be greater than zero.");
			ArgumentException.ThrowIfNullOrWhiteSpace(languageCode, nameof(languageCode));
			SpeakerBiographyRequest? speakerBiographyRequest
				= await request.GetRequestParameters2Async<SpeakerBiographyRequest>(_jsonSerializerOptions) ?? throw new ArgumentNullException(nameof(request), "Invalid request body.");
			UpsertResponse<SpeakerBiographyResponse?> upsertResponse = await _services.UpsertSpeakerBiographyAsync(speakerId, languageCode, speakerBiographyRequest, options);
			if (upsertResponse.Action == UpsertAction.Create)
				return request.CreateCreatedResponse($"speakers/{speakerId}/biographies/{languageCode}");
			else
				return request.CreateResponse(HttpStatusCode.NoContent);
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

}