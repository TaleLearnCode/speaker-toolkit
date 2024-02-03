namespace TaleLearnCode.SpeakerToolkit.Services;

public class PresentationTagServices(ConfigServices configServices) : ServicesBase(configServices)
{

	public async Task<TagsResponse> GetTagsAsync(int presentationId)
		=> (await GetPresentationAsync(presentationId)).PresentationTags.ToTagsResponse();

	public async Task AddTagToPresentation(int presentationId, string tagName)
	{
		using SpeakerToolkitContext context = new(_configServices);
		Presentation presentation = await GetPresentationAsync(presentationId, context);
		PresentationTag? presentationTag = GetExistingPresentationTag(presentation, tagName);
		if (presentationTag is null)
		{
			Tag tag = await TagServices.GetOrCreateAsync(context, tagName);
			presentationTag = new()
			{
				Presentation = presentation,
				Tag = tag
			};
			await context.PresentationTags.AddAsync(presentationTag);
			await context.SaveChangesAsync();

		}
	}

	public async Task AddTagsToPresentation(int presentationId, AddTagsRequest addTagsRequest, bool append = true)
	{
		using SpeakerToolkitContext context = new(_configServices);
		Presentation presentation = await GetPresentationAsync(presentationId, context);
		if (!append) await RemoveTagsFromPresentation(context, presentation);
		foreach (string tagName in addTagsRequest.Tags)
		{
			PresentationTag? presentationTag = GetExistingPresentationTag(presentation, tagName);
			if (presentationTag is null)
			{
				Tag tag = await TagServices.GetOrCreateAsync(context, tagName);
				presentationTag = new()
				{
					Presentation = presentation,
					Tag = tag
				};
				await context.PresentationTags.AddAsync(presentationTag);
			}
		}
		await context.SaveChangesAsync();
	}

	public async Task RemoveTagFromPresentation(int presentationId, string tagName)
	{
		using SpeakerToolkitContext context = new(_configServices);
		Presentation? presentation = await context.Presentations.FirstOrDefaultAsync(x => x.PresentationId == presentationId) ?? throw new ArgumentOutOfRangeException(nameof(presentationId), "Presentation not found.");
		Tag tag = await TagServices.GetTagAsync(context, tagName) ?? throw new ArgumentOutOfRangeException(nameof(tagName), "Tag not found.");
		PresentationTag? presentationTag = await GetPresentationTagAsync(context, presentationId, tag.TagId);
		if (presentationTag is not null)
		{
			context.PresentationTags.Remove(presentationTag);
			await context.SaveChangesAsync();
		}
	}

	public async Task RemoveTagsFromPresentation(int presentationId)
	{
		using SpeakerToolkitContext context = new(_configServices);
		Presentation? presentation = await GetPresentationAsync(presentationId, context);
		foreach (PresentationTag presentationTag in presentation.PresentationTags)
			context.PresentationTags.Remove(presentationTag);
		await context.SaveChangesAsync();
	}

	private static async Task RemoveTagsFromPresentation(SpeakerToolkitContext context, Presentation presentation)
	{
		if (presentation.PresentationTags is not null)
		{
			foreach (PresentationTag presentationTag in presentation.PresentationTags)
				context.PresentationTags.Remove(presentationTag);
			await context.SaveChangesAsync();
		}
	}

	private static async Task<PresentationTag?> GetPresentationTagAsync(SpeakerToolkitContext context, int presentationId, int tagId)
		=> await context.PresentationTags
			.Include(x => x.Tag)
			.FirstOrDefaultAsync(x => x.PresentationId == presentationId && x.TagId == tagId);

	private async Task<Presentation> GetPresentationAsync(int presentationId)
	{
		using SpeakerToolkitContext context = new(_configServices);
		return await GetPresentationAsync(presentationId, context);
	}

	private static async Task<Presentation> GetPresentationAsync(int presentationId, SpeakerToolkitContext context)
	{
		return await context.Presentations
			.Include(x => x.PresentationTags)
				.ThenInclude(x => x.Tag)
			.FirstOrDefaultAsync(x => x.PresentationId == presentationId)
			?? throw new ArgumentOutOfRangeException(nameof(presentationId), "Presentation not found.");
	}

	private static PresentationTag? GetExistingPresentationTag(Presentation presentation, string tagName)
		=> presentation.PresentationTags.FirstOrDefault(x => x.Tag.TagName == tagName);

}