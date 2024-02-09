namespace TaleLearnCode.SpeakerToolkit.Services;

public class EngagementTagServices(ConfigServices configServices) : ServicesBase(configServices)
{

	public async Task<EngagementTagsResponse> GetTagsAsync(int EngagementId)
		=> (await GetEngagementAsync(EngagementId)).ToEngagementTagsResponse();

	public async Task AddTagToEngagement(int EngagementId, string tagName)
	{
		using SpeakerToolkitContext context = new(_configServices);
		Engagement Engagement = await GetEngagementAsync(EngagementId, context);
		EngagementTag? EngagementTag = GetExistingEngagementTag(Engagement, tagName);
		if (EngagementTag is null)
		{
			Tag tag = await TagServices.GetOrCreateAsync(context, tagName);
			EngagementTag = new()
			{
				Engagement = Engagement,
				Tag = tag
			};
			await context.EngagementTags.AddAsync(EngagementTag);
			await context.SaveChangesAsync();

		}
	}

	public async Task AddTagsToEngagement(int EngagementId, AddTagsRequest addTagsRequest, bool append = true)
	{
		using SpeakerToolkitContext context = new(_configServices);
		Engagement Engagement = await GetEngagementAsync(EngagementId, context);
		if (!append) await RemoveTagsFromEngagement(context, Engagement);
		foreach (string tagName in addTagsRequest.Tags)
		{
			EngagementTag? EngagementTag = GetExistingEngagementTag(Engagement, tagName);
			if (EngagementTag is null)
			{
				Tag tag = await TagServices.GetOrCreateAsync(context, tagName);
				EngagementTag = new()
				{
					Engagement = Engagement,
					Tag = tag
				};
				await context.EngagementTags.AddAsync(EngagementTag);
			}
		}
		await context.SaveChangesAsync();
	}

	public async Task RemoveTagFromEngagement(int EngagementId, string tagName)
	{
		using SpeakerToolkitContext context = new(_configServices);
		Engagement? Engagement = await context.Engagements.FirstOrDefaultAsync(x => x.EngagementId == EngagementId) ?? throw new ArgumentOutOfRangeException(nameof(EngagementId), "Engagement not found.");
		Tag tag = await TagServices.GetTagAsync(context, tagName) ?? throw new ArgumentOutOfRangeException(nameof(tagName), "Tag not found.");
		EngagementTag? EngagementTag = await GetEngagementTagAsync(context, EngagementId, tag.TagId);
		if (EngagementTag is not null)
		{
			context.EngagementTags.Remove(EngagementTag);
			await context.SaveChangesAsync();
		}
	}

	public async Task RemoveTagsFromEngagement(int EngagementId)
	{
		using SpeakerToolkitContext context = new(_configServices);
		Engagement? Engagement = await GetEngagementAsync(EngagementId, context);
		foreach (EngagementTag EngagementTag in Engagement.EngagementTags)
			context.EngagementTags.Remove(EngagementTag);
		await context.SaveChangesAsync();
	}

	private static async Task RemoveTagsFromEngagement(SpeakerToolkitContext context, Engagement Engagement)
	{
		if (Engagement.EngagementTags is not null)
		{
			foreach (EngagementTag EngagementTag in Engagement.EngagementTags)
				context.EngagementTags.Remove(EngagementTag);
			await context.SaveChangesAsync();
		}
	}

	private static async Task<EngagementTag?> GetEngagementTagAsync(SpeakerToolkitContext context, int EngagementId, int tagId)
		=> await context.EngagementTags
			.Include(x => x.Tag)
			.FirstOrDefaultAsync(x => x.EngagementId == EngagementId && x.TagId == tagId);

	private async Task<Engagement> GetEngagementAsync(int EngagementId)
	{
		using SpeakerToolkitContext context = new(_configServices);
		return await GetEngagementAsync(EngagementId, context);
	}

	private static async Task<Engagement> GetEngagementAsync(int EngagementId, SpeakerToolkitContext context)
	{
		return await context.Engagements
			.Include(x => x.EngagementTags)
				.ThenInclude(x => x.Tag)
			.FirstOrDefaultAsync(x => x.EngagementId == EngagementId)
			?? throw new ArgumentOutOfRangeException(nameof(EngagementId), "Engagement not found.");
	}

	private static EngagementTag? GetExistingEngagementTag(Engagement Engagement, string tagName)
		=> Engagement.EngagementTags.FirstOrDefault(x => x.Tag.TagName == tagName);

}