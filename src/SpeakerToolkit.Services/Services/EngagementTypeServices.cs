namespace TaleLearnCode.SpeakerToolkit.Services;

public class EngagementTypeServices(ConfigServices configServices) : ServicesBase(configServices)
{

	public async Task<IEnumerable<EngagementTypeResponse>> GetEngagementTypesAsync()
		=> (await GetDataAsync()).ToResponse();

	public async Task<EngagementTypeResponse> GetEngagementTypeAsync(int engagementTypeId)
		=> (await GetDataAsync(engagementTypeId)).FirstOrDefault()?.ToResponse() ?? throw new ArgumentOutOfRangeException("Engagement Type not found.");

	private async Task<List<EngagementType>> GetDataAsync(int? engagementTypeId = null)
	{
		SpeakerToolkitContext context = new(_configServices);
		return await GetDataAsync(context, engagementTypeId);
	}

	private static async Task<List<EngagementType>> GetDataAsync(SpeakerToolkitContext context, int? engagementTypeId = null)
	{
		IQueryable<EngagementType> engagementTypes = context.EngagementTypes;
		if (engagementTypeId.HasValue)
			engagementTypes = engagementTypes.Where(engagementType => engagementType.EngagementTypeId == engagementTypeId);
		return await engagementTypes.OrderBy(engagementType => engagementType.EngagementTypeName).ToListAsync();
	}

}