namespace TaleLearnCode.SpeakerToolkit.Services;

public class PresentationTypeServices(ConfigServices configServices) : ServicesBase(configServices)
{

	public async Task<PresentationTypeResponse?> GetPresentationTypeAsync(int presentationTypeId)
		=> (await GetDataAsync(presentationTypeId)).FirstOrDefault().ToResponse();

	public async Task<List<PresentationTypeResponse>> GetPresentationTypesAsync()
		=> (await GetDataAsync()).ToResponse();

	private async Task<List<PresentationType>> GetDataAsync(int? presentationTypeId = null)
	{
		using SpeakerToolkitContext context = new(_configServices);
		return await GetDataAsync(context, presentationTypeId);
	}

	private async Task<List<PresentationType>> GetDataAsync(SpeakerToolkitContext context, int? presentationTypeId = null)
	{
		IQueryable<PresentationType> query = context.PresentationTypes;
		if (presentationTypeId.HasValue)
			query = query.Where(x => x.PresentationTypeId == presentationTypeId.Value);
		return await query.ToListAsync();
	}

}