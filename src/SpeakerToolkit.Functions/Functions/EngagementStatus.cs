//namespace TaleLearnCode.SpeakerToolkit.Functions;

//public class EngagementStatus(ILoggerFactory loggerFactory, ConfigServices configServices, JsonSerializerOptions jsonSerializerOptions)
//{

//	private readonly ILogger _logger = loggerFactory.CreateLogger<Country>();
//	private readonly EngagementStatusServices _services = new(configServices);
//	private readonly JsonSerializerOptions _jsonSerializerOptions = jsonSerializerOptions;

//	[Function("GetEngagementStatuses")]
//	public async Task<HttpResponseData> GetEngagementStatusesAsync(
//		[HttpTrigger(AuthorizationLevel.Function, "get", Route = "engagement-statuses")] HttpRequestData request)
//	{
//		try
//		{
//			return await request.CreateResponseAsync(await _services.GetEngagementStatusesAsync(), _jsonSerializerOptions);
//		}
//		catch (Exception ex) when (ex is ArgumentOutOfRangeException)
//		{
//			return request.CreateNotFoundResponse(ex);
//		}
//		catch (Exception ex) when (ex is ArgumentException)
//		{
//			return request.CreateBadRequestResponse(ex);
//		}
//		catch (Exception ex)
//		{
//			_logger.LogError(ex, "Unhandled Exception: {errorMessage}", ex.Message);
//			return request.CreateErrorResponse(ex);
//		}
//	}

//	[Function("GetEngagementStatus")]
//	public async Task<HttpResponseData> GetEngagementStatusAsync(
//		[HttpTrigger(AuthorizationLevel.Function, "get", Route = "engagement-statuses/{engagementStatusId:int}")] HttpRequestData request,
//		int engagementStatusId)
//	{
//		try
//		{
//			return await request.CreateResponseAsync(await _services.GetEngagementStatusAsync(engagementStatusId), _jsonSerializerOptions);
//		}
//		catch (Exception ex) when (ex is ArgumentOutOfRangeException)
//		{
//			return request.CreateNotFoundResponse(ex);
//		}
//		catch (Exception ex) when (ex is ArgumentException)
//		{
//			return request.CreateBadRequestResponse(ex);
//		}
//		catch (Exception ex)
//		{
//			_logger.LogError(ex, "Unhandled Exception: {errorMessage}", ex.Message);
//			return request.CreateErrorResponse(ex);
//		}
//	}

//}