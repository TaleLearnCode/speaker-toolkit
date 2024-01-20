namespace TaleLearnCode.SpeakerToolkit.Services;

public class SpeakerAwardTypeServices(ConfigServices configServices) : ServicesBase(configServices)
{

	public async Task<List<SpeakerAwardTypeResponse>> GetSpeakerAwardTypesAsync()
		=> (await GetDataAsync(0)).ToResponse();

	public async Task<SpeakerAwardTypeResponse?> GetSpeakerAwardTypeAsync(int speakerAwardTypeId)
		=> (await GetDataAsync(speakerAwardTypeId)).FirstOrDefault().ToResponse();

	private async Task<List<SpeakerAwardType>> GetDataAsync(int speakerAwardTypeId)
	{
		using SpeakerToolkitContext speakerToolkitContext = new(_configServices);
		IQueryable<SpeakerAwardType> query = speakerToolkitContext.SpeakerAwardTypes.OrderBy(x => x.SpeakerAwardTypeName);
		if (speakerAwardTypeId > 0)
			query = query.Where(x => x.SpeakerAwardTypeId == speakerAwardTypeId);
		return await query.ToListAsync();
	}

}