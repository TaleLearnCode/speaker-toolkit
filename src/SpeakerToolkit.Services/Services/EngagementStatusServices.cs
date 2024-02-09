namespace TaleLearnCode.SpeakerToolkit.Services;

public class EngagementStatusServices(ConfigServices configServices) : ServicesBase(configServices)
{

	public async Task<List<EngagementStatusResponse>> GetEngagementStatusesAsync()
	{
		SpeakerToolkitContext context = new(_configServices);
		return (await context.EngagementStatuses
			.OrderBy(engagementStatus => engagementStatus.EngagementStatusName)
			.ToListAsync())
			.ToResponse();
	}

	public async Task<EngagementStatusResponse> GetEngagementStatusAsync(int engagementStatusId)
	{
		SpeakerToolkitContext context = new(_configServices);
		return (await context.EngagementStatuses
			.Where(engagementStatus => engagementStatus.EngagementStatusId == engagementStatusId)
			.FirstOrDefaultAsync())?
			.ToResponse()
			?? throw new ArgumentOutOfRangeException(nameof(engagementStatusId), "Engagement Status not found");
	}

}