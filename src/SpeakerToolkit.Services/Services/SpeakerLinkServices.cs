namespace TaleLearnCode.SpeakerToolkit.Services;

public class SpeakerLinkServices(ConfigServices configServices) : ServicesBase(configServices)
{

	public async Task<List<SpeakerLinkResponse>> GetSpeakerLinksAsync(int speakerId)
		=> (await GetDataAsync(speakerId)).ToResponse().ToList();

	public async Task<SpeakerLinkResponse?> GetSpeakerLinkAsync(int speakerId, int speakerLinkId)
		=> (await GetDataAsync(speakerId, speakerLinkId)).FirstOrDefault().ToResponse();

	public async Task<SpeakerLinkResponse?> CreateSpeakerLinkAsync(
	int speakerId,
	SpeakerLinkRequest request)
	{
		using SpeakerToolkitContext context = new(_configServices);
		List<SpeakerLink>? speakerLinks = [.. (await GetDataAsync(speakerId))];
		SpeakerLink? speakerLink = speakerLinks.FirstOrDefault(x => x.LinkType == request.LinkType && x.LinkUrl == request.LinkUrl);
		if (speakerLink is null)
		{
			speakerLink = new()
			{
				SpeakerId = speakerId,
				LinkType = request.LinkType,
				LinkUrl = request.LinkUrl
			};
			await context.SpeakerLinks.AddAsync(speakerLink);
			await context.SaveChangesAsync();
			return (await GetDataAsync(speakerId, speakerLink.SpeakerLinkId)).First().ToResponse();
		}
		else
		{
			throw new ObjectAlreadyExistsException($"Speaker Link already exists.");
		}
	}

	public async Task<SpeakerLinkResponse?> UpdateSpeakerLinkAsync(
		int speakerId,
		int speakerLinkId,
		SpeakerLinkRequest request)
	{
		using SpeakerToolkitContext context = new(_configServices);
		SpeakerLink? speakerLink = (await GetDataAsync(context, speakerId, speakerLinkId)).FirstOrDefault();
		if (speakerLink is null)
		{
			throw new ArgumentOutOfRangeException(nameof(speakerLinkId), "Speaker Link not found.");
		}
		else
		{
			if (speakerLink.HasChanges(request))
			{
				speakerLink.LinkType = request.LinkType;
				speakerLink.LinkUrl = request.LinkUrl;
				await context.SaveChangesAsync();
			}
		}
		return speakerLink.ToResponse();
	}

	private static async Task<List<SpeakerLink>> GetDataAsync(
		SpeakerToolkitContext context,
		int speakerId,
		int? speakerLinkId = null)
	{
		IQueryable<SpeakerLink> query = context.SpeakerLinks
			.Include(x => x.Speaker)
			.Where(x => x.SpeakerId == speakerId);
		if (speakerLinkId is not null)
			query = query.Where(x => x.SpeakerLinkId == speakerLinkId);
		query.OrderBy(x => x.LinkType);
		return await query.ToListAsync();
	}

	private async Task<List<SpeakerLink>> GetDataAsync(int speakerId, int? speakerLinkId = null)
	{
		using SpeakerToolkitContext context = new(_configServices);
		return await GetDataAsync(context, speakerId, speakerLinkId);
	}

}