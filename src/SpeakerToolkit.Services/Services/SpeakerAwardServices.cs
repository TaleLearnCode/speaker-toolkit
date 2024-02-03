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

	public async Task<SpeakerAwardResponse?> CreateSpeakerAwardAsync(
		int speakerId,
		SpeakerAwardRequest request)
	{
		using SpeakerToolkitContext context = new(_configServices);
		SpeakerAward? speakerAward = new()
		{
			SpeakerId = speakerId,
			SpeakerAwardTypeId = request.SpeakerAwardTypeId,
			AwardCategory = request.AwardCategory,
			AwardYear = request.AwardYear,
			AwardProfileUrl = request.AwardProfileUrl
		};
		await context.SpeakerAwards.AddAsync(speakerAward);
		await context.SaveChangesAsync();
		return (await GetDataAsync(speakerId, speakerAward.SpeakerAwardId)).ToResponse();
	}

	public async Task<SpeakerAwardResponse?> UpdateSpeakerAwardAsync(
		int speakerId,
		int speakerAwardId,
		SpeakerAwardRequest request)
	{
		using SpeakerToolkitContext context = new(_configServices);
		SpeakerAward? speakerAward = await GetDataAsync(context, speakerId, speakerAwardId);
		if (speakerAward is null)
		{
			throw new ArgumentOutOfRangeException(nameof(speakerAwardId), "Speaker Award not found.");
		}
		else
		{
			if (speakerAward.HasChanges(request))
			{
				speakerAward.SpeakerAwardTypeId = request.SpeakerAwardTypeId;
				speakerAward.AwardCategory = request.AwardCategory;
				speakerAward.AwardYear = request.AwardYear;
				speakerAward.AwardProfileUrl = request.AwardProfileUrl;
				await context.SaveChangesAsync();
			}
		}
		return (await GetDataAsync(speakerId, speakerAwardId)).ToResponse();
	}

	private async Task<SpeakerAward?> GetDataAsync(int speakerId, int speakerAwardId)
	{
		using SpeakerToolkitContext context = new(_configServices);
		return await GetDataAsync(context, speakerId, speakerAwardId);
	}

	private static async Task<SpeakerAward?> GetDataAsync(
		SpeakerToolkitContext context,
		int speakerId,
		int speakerAwardId)
		=> (await context.SpeakerAwards
		.Include(x => x.SpeakerAwardType)
		.Include(x => x.Speaker)
		.FirstOrDefaultAsync(x => x.SpeakerAwardId == speakerAwardId && x.SpeakerId == speakerId));

}