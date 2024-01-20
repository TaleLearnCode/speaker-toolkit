namespace TaleLearnCode.SpeakerToolkit.Services;

public class SpeakerAwardServices(ConfigServices configServices) : ServicesBase(configServices)
{

	public async Task<List<SpeakerAwardResponse>> GetSpeakerAwardsAsync(int speakerId)
	{
		using SpeakerToolkitContext speakerToolkitContext = new(_configServices);
		return (await speakerToolkitContext.SpeakerAwards
			.Include(x => x.SpeakerAwardType)
			.Include(x => x.Speaker)
			.Where(x => x.SpeakerId == speakerId)
			.OrderBy(x => x.SpeakerAwardType.SpeakerAwardTypeName)
			.ToListAsync())
			.ToResponse();
	}

	public async Task<SpeakerAwardResponse?> GetSpeakerAwardAsync(int speakerId, int speakerAwardId)
	{
		using SpeakerToolkitContext speakerToolkitContext = new(_configServices);
		SpeakerAwardResponse? response = (await speakerToolkitContext.SpeakerAwards
			.Include(x => x.SpeakerAwardType)
			.Include(x => x.Speaker)
			.Where(x => x.SpeakerAwardId == speakerAwardId)
			.OrderBy(x => x.SpeakerAwardType.SpeakerAwardTypeName)
			.FirstOrDefaultAsync())
			.ToResponse();
		if (response is not null && response.SpeakerId != speakerId) response = null;
		return response;
	}

}