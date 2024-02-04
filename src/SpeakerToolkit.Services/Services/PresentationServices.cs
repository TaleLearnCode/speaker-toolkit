namespace TaleLearnCode.SpeakerToolkit.Services;

public class PresentationServices(ConfigServices configServices) : ServicesBase(configServices)
{

	public async Task<RelatedPresentationsResponse> GetRelatedPresentationsAsync(int presentationId, string? languageCode = null)
		=> (await GetPresentationAsync(
			new()
			{
				PresentationId = presentationId,
				IncludeRelatedPresentations = true
			}))
		.ToRelatedPresentationsResponse(languageCode);

	public async Task<RelatedPresentationsResponse> AddRelatedPresentation(int presentationId, int relatedPresentationId, int? sortOrder)
	{
		using SpeakerToolkitContext context = new(_configServices);
		Presentation presentation = await GetPresentationAsync(new() { PresentationId = presentationId, IncludeRelatedPresentations = true }, context, nameof(presentationId));
		Presentation relatedPresentation = await GetPresentationAsync(new() { PresentationId = relatedPresentationId, IncludePresentationTexts = false }, context, nameof(relatedPresentationId));
		PresentationRelated? presentationRelated = presentation.RelatedPresentations.FirstOrDefault(x => x.PrimaryPresentationId == presentationId && x.RelatedPresentationId == relatedPresentationId);
		if (presentationRelated is not null)
		{
			if (sortOrder.HasValue && presentationRelated.SortOrder != sortOrder)
			{
				presentationRelated.SortOrder = sortOrder.Value;
				await context.SaveChangesAsync();
			}
		}
		else
		{
			if (sortOrder.HasValue)
				foreach (PresentationRelated existingRelatedPresentation in presentation.RelatedPresentations.Where(x => x.SortOrder >= sortOrder.Value))
					existingRelatedPresentation.SortOrder++;
			context.RelatedPresentations.Add(new()
			{
				PrimaryPresentationId = presentationId,
				RelatedPresentationId = relatedPresentationId,
				SortOrder = sortOrder ?? presentation.RelatedPresentations.Max(x => x.SortOrder) + 1
			});
			await context.SaveChangesAsync();
		}
		return presentation.ToRelatedPresentationsResponse();
	}

	public async Task<RelatedPresentationsResponse> RemoveRelatedPresentation(int presentationId, int relatedPresentationId)
	{
		using SpeakerToolkitContext context = new(_configServices);
		Presentation presentation = await GetPresentationAsync(new() { PresentationId = presentationId, IncludeRelatedPresentations = true }, context, nameof(presentationId));
		Presentation relatedPresentation = await GetPresentationAsync(new() { PresentationId = relatedPresentationId, IncludePresentationTexts = false }, context, nameof(relatedPresentationId));
		PresentationRelated? presentationRelated = presentation.RelatedPresentations.FirstOrDefault(x => x.PrimaryPresentationId == presentationId && x.RelatedPresentationId == relatedPresentationId);
		if (presentationRelated is not null)
		{
			context.RelatedPresentations.Remove(presentationRelated);
			await context.SaveChangesAsync();
		}
		return presentation.ToRelatedPresentationsResponse();
	}

	private async Task<Presentation> GetPresentationWithRelatedPresentationsAsync(int presentationId)
	{
		using SpeakerToolkitContext context = new(_configServices);
		return await GetPresentationWithRelatedPresentationsAsync(presentationId, context);
	}

	private static async Task<Presentation> GetPresentationWithRelatedPresentationsAsync(int presentationId, SpeakerToolkitContext context)
		=> await context.Presentations
		.Include(x => x.PresentationTexts)
		.Include(x => x.RelatedPresentations)
			.ThenInclude(x => x.RelatedPresentation)
				.ThenInclude(x => x.PresentationTexts)
		.FirstOrDefaultAsync(x => x.PresentationId == presentationId)
		?? throw new ArgumentOutOfRangeException(nameof(presentationId), "Presentation not found.");

	private async Task<Presentation> GetPresentationAsync(
		GetPresentationOptions options,
		string presentationIdParamName = "presentationId")
	{
		using SpeakerToolkitContext speakerToolkitContext = new(_configServices);
		return await GetPresentationAsync(options, speakerToolkitContext, presentationIdParamName);
	}

	private static async Task<Presentation> GetPresentationAsync(
		GetPresentationOptions options,
		SpeakerToolkitContext context,
		string presentationIdParamName = "presentationId")
	{
		return (await GetPresentationsAsync(options, context)).FirstOrDefault()
			?? throw new ArgumentOutOfRangeException(presentationIdParamName, "Presentation not found.");
	}

	private async Task<List<Presentation>> GetPresentationsAsync(GetPresentationOptions options)
	{
		using SpeakerToolkitContext context = new(_configServices);
		return await GetPresentationsAsync(options, context);
	}

	private static async Task<List<Presentation>> GetPresentationsAsync(
		GetPresentationOptions options, SpeakerToolkitContext context)
	{
		IQueryable<Presentation> query = context.Presentations;
		if (options.IncludePresentationTexts)
			query = query.Include(x => x.PresentationTexts);
		if (options.IncludeRelatedPresentations)
			query = query.Include(x => x.RelatedPresentations)
				.ThenInclude(x => x.RelatedPresentation)
					.ThenInclude(x => x.PresentationTexts);
		if (options.PresentationId is not null)
			query = query.Where(x => x.PresentationId == options.PresentationId);
		return await query.ToListAsync();
	}

}