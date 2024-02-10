namespace TaleLearnCode.SpeakerToolkit.Services;

public class PresentationServices(ConfigServices configServices) : ServicesBase(configServices)
{

	#region Related Presentations

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

	#endregion

	#region Presentation Texts

	public async Task<PresentationTextResponse> GetPresentationTextAsync(int presentationId, string? languageCode = null)
	{
		Presentation presentation = await GetPresentationAsync(new() { PresentationId = presentationId, IncludePresentationTexts = true });
		return presentation.PresentationTexts.FirstOrDefault(x => x.LanguageCode == languageCode)
			?.ToPresentationTextResponse()
			?? presentation.PresentationTexts.FirstOrDefault(x => x.LanguageCode == presentation.DefaultLanguageCode)
				?.ToPresentationTextResponse()
			?? presentation.PresentationTexts.FirstOrDefault()
				?.ToPresentationTextResponse()
			?? throw new ArgumentOutOfRangeException(nameof(presentationId), "Presentation text not found.");
	}

	public async Task<List<PresentationTextResponse>> GetPresentationTextsAsync(int presentationId)
	{
		Presentation presentation = await GetPresentationAsync(new() { PresentationId = presentationId, IncludePresentationTexts = true });
		return presentation.PresentationTexts.ToPresentationTextResponseList();
	}

	public async Task AddPresentationTextAsync(int presentationId, string languageCode, PresentationTextRequest request)
	{
		using SpeakerToolkitContext context = new(_configServices);
		Presentation presentation = await GetPresentationAsync(new() { PresentationId = presentationId, IncludePresentationTexts = true }, context, nameof(presentationId));
		PresentationText? presentationText = presentation.PresentationTexts.FirstOrDefault(x => x.LanguageCode.Equals(languageCode, StringComparison.InvariantCultureIgnoreCase));
		if (presentationText is not null) throw new ObjectAlreadyExistsException($"Presentation text for language code '{languageCode}' already exists for presentation {presentationId}.");
		presentationText = new()
		{
			PresentationId = presentationId,
			LanguageCode = languageCode,
			PresentationTitle = request.Title,
			PresentationShortTitle = request.ShortTitle,
			Abstract = request.Abstract,
			ShortAbstract = request.ShortAbstract,
			Summary = request.Summary,
			AdditionalDetails = request.AdditionalDetails
		};
		context.PresentationTexts.Add(presentationText);
		await context.SaveChangesAsync();
	}

	public async Task UpdatePresentationTextAsync(int presentationId, string languageCode, PresentationTextRequest request)
	{
		using SpeakerToolkitContext context = new(_configServices);
		Presentation presentation = await GetPresentationAsync(new() { PresentationId = presentationId, IncludePresentationTexts = true }, context, nameof(presentationId));
		PresentationText? presentationText = presentation.PresentationTexts.FirstOrDefault(x => x.LanguageCode.Equals(languageCode, StringComparison.InvariantCultureIgnoreCase))
			?? throw new ArgumentOutOfRangeException(nameof(languageCode), "Presentation text not found.");
		presentationText.PresentationTitle = request.Title;
		presentationText.PresentationShortTitle = request.ShortTitle;
		presentationText.Abstract = request.Abstract;
		presentationText.ShortAbstract = request.ShortAbstract;
		presentationText.Summary = request.Summary;
		presentationText.AdditionalDetails = request.AdditionalDetails;
		await context.SaveChangesAsync();
	}

	public async Task RemovePresentationTextAsync(int presentationId, string languageCode)
	{
		using SpeakerToolkitContext context = new(_configServices);
		Presentation presentation = await GetPresentationAsync(new() { PresentationId = presentationId, IncludePresentationTexts = true }, context, nameof(presentationId));
		PresentationText? presentationText = presentation.PresentationTexts.FirstOrDefault(x => x.LanguageCode.Equals(languageCode, StringComparison.InvariantCultureIgnoreCase))
			?? throw new ArgumentOutOfRangeException(nameof(languageCode), "Presentation text not found.");
		if (presentation.PresentationTexts.Count == 1)
			throw new InvalidOperationException("Cannot remove the only presentation text for a presentation.");
		context.PresentationTexts.Remove(presentationText);
		await context.SaveChangesAsync();
	}

	#endregion

	#region Learning Objectives

	public async Task<LearningObjectiveResponse> GetLearningObjectiveAsync(int presentationId, string languageCode, int sortOrder)
	{
		Presentation presentation = await GetPresentationAsync(new() { PresentationId = presentationId, IncludePresentationTexts = true });
		PresentationText? presentationText = presentation.PresentationTexts
			.FirstOrDefault(x => x.LanguageCode.Equals(languageCode, StringComparison.InvariantCultureIgnoreCase))
			?? throw new ArgumentOutOfRangeException(nameof(languageCode), $"There are no {languageCode} learning objectives for presentation {presentationId}.");
		return presentationText
			.LearningObjectives.FirstOrDefault(x => x.SortOrder == sortOrder)?
			.ToResponse(presentationText)
			?? throw new ArgumentOutOfRangeException(nameof(sortOrder), "Learning objective not found.");
	}

	public async Task<LearningObjectivesResponse> GetLearningObjectivesAsync(int presentationId, string languageCode)
	{
		Presentation presentation = await GetPresentationAsync(new() { PresentationId = presentationId, IncludePresentationTexts = true });
		PresentationText? presentationText = presentation.PresentationTexts
			.FirstOrDefault(x => x.LanguageCode.Equals(languageCode, StringComparison.InvariantCultureIgnoreCase))
			?? throw new ArgumentOutOfRangeException(nameof(languageCode), $"There are no {languageCode} learning objectives for presentation {presentationId}.");
		return presentationText.ToLearningObjectivesResponse(true);
	}

	public async Task<LearningObjectiveResponse> AddLearningObjectiveAsync(
		int presentationId,
		string languageCode,
		string learningObjectiveText,
		int? sortOrder = null)
	{
		using SpeakerToolkitContext context = new(_configServices);
		Presentation presentation = await GetPresentationAsync(new() { PresentationId = presentationId, IncludePresentationTexts = true }, context);
		PresentationText presentationText = presentation.PresentationTexts
			.FirstOrDefault(x => x.LanguageCode.Equals(languageCode, StringComparison.InvariantCultureIgnoreCase))
			?? throw new ArgumentOutOfRangeException(nameof(languageCode), $"There are no {languageCode} learning objectives for presentation {presentationId}.");
		LearningObjective? learningObjective = presentationText.LearningObjectives.FirstOrDefault(x => x.LearningObjectiveText.Equals(learningObjectiveText, StringComparison.InvariantCultureIgnoreCase));
		if (learningObjective is not null && (learningObjective.SortOrder == sortOrder || !sortOrder.HasValue))
			throw new ObjectAlreadyExistsException($"Learning objective '{learningObjectiveText}' already exists for presentation {presentationId}.");
		UpdateLearningObjectiveSortOrders(sortOrder, presentationText);
		if (learningObjective is not null)
		{
			learningObjective.SortOrder = sortOrder ?? learningObjective.SortOrder;
		}
		else
		{
			learningObjective = new()
			{
				LearningObjectiveText = learningObjectiveText,
				SortOrder = sortOrder ?? presentationText.LearningObjectives.Max(x => x.SortOrder) + 1
			};
			presentationText.LearningObjectives.Add(learningObjective);
		}
		await context.SaveChangesAsync();
		return learningObjective.ToResponse(presentationText);
	}

	public async Task<LearningObjectiveResponse> UpdateLearningObjectiveAsync(
		int presentationId,
		string languageCode,
		int sortOrder,
		string learningObjectiveText,
		int? newSortOrder = null)
	{
		using SpeakerToolkitContext context = new(_configServices);
		Presentation presentation = await GetPresentationAsync(new() { PresentationId = presentationId, IncludePresentationTexts = true }, context);
		PresentationText presentationText = presentation.PresentationTexts
			.FirstOrDefault(x => x.LanguageCode.Equals(languageCode, StringComparison.InvariantCultureIgnoreCase))
			?? throw new ArgumentOutOfRangeException(nameof(languageCode), $"There are no {languageCode} learning objectives for presentation {presentationId}.");
		LearningObjective learningObjective = presentationText.LearningObjectives.FirstOrDefault(x => x.SortOrder == sortOrder)
			?? throw new ArgumentOutOfRangeException(nameof(sortOrder), $"The specified learning objective does not exist.");
		if (learningObjective.SortOrder != newSortOrder)
		{
			UpdateLearningObjectiveSortOrders(newSortOrder, presentationText);
			learningObjective.SortOrder = newSortOrder ?? learningObjective.SortOrder;
		}
		learningObjective.LearningObjectiveText = learningObjectiveText;
		await context.SaveChangesAsync();
		return learningObjective.ToResponse(presentationText);
	}

	public async Task RemoveLearningObjectiveAsync(int presentationId, string languageCode, int sortOrder)
	{
		using SpeakerToolkitContext context = new(_configServices);
		Presentation presentation = await GetPresentationAsync(new() { PresentationId = presentationId, IncludePresentationTexts = true }, context);
		PresentationText presentationText = presentation.PresentationTexts
			.FirstOrDefault(x => x.LanguageCode.Equals(languageCode, StringComparison.InvariantCultureIgnoreCase))
			?? throw new ArgumentOutOfRangeException(nameof(languageCode), $"There are no {languageCode} learning objectives for presentation {presentationId}.");
		LearningObjective learningObjective = presentationText.LearningObjectives.FirstOrDefault(x => x.SortOrder == sortOrder)
			?? throw new ArgumentOutOfRangeException(nameof(sortOrder), $"The specified learning objective does not exist.");
		context.LearningObjectives.Remove(learningObjective);


		await context.SaveChangesAsync();
	}

	private static void UpdateLearningObjectiveSortOrders(int? sortOrder, PresentationText presentationText)
	{
		if (sortOrder.HasValue && presentationText.LearningObjectives.Any(x => x.SortOrder == sortOrder))
			foreach (LearningObjective existingLearningObjective in presentationText.LearningObjectives.Where(x => x.SortOrder >= sortOrder.Value))
				existingLearningObjective.SortOrder++;
	}

	#endregion

	#region Speakers

	public async Task<PresentationSpeakersResponse> GetSpeakersAsync(int presentationId)
		=> (await GetPresentationAsync(new() { PresentationId = presentationId, IncludeSpeakers = true }))
		.ToSpeakerList();

	public async Task AddSpeakerAsync(int presentationId, int speakerId, bool isPrimary)
	{
		using SpeakerToolkitContext context = new(_configServices);
		Presentation presentation = await GetPresentationAsync(new()
		{ PresentationId = presentationId, IncludeSpeakers = true }, context, nameof(presentationId));
		Speaker speaker = await context.Speakers.FindAsync(speakerId)
			?? throw new ArgumentOutOfRangeException(nameof(speakerId), "Speaker not found.");
		if (presentation.PresentationSpeakers.Any(x => x.SpeakerId == speakerId))
			throw new ObjectAlreadyExistsException($"Speaker {speakerId} is already associated with presentation {presentationId}.");
		if (presentation.PresentationSpeakers is null || presentation.PresentationSpeakers.Count == 0)
		{
			presentation.PresentationSpeakers = [];
			isPrimary = true;
		}
		else if (isPrimary)
			foreach (PresentationSpeaker existingSpeaker in presentation.PresentationSpeakers)
				existingSpeaker.IsPrimary = false;
		presentation.PresentationSpeakers.Add(new()
		{
			PresentationId = presentationId,
			SpeakerId = speakerId,
			IsPrimary = isPrimary
		});
		await context.SaveChangesAsync();
	}

	public async Task RemoveSpeakerAsync(int presentationId, int speakerId)
	{
		using SpeakerToolkitContext context = new(_configServices);
		Presentation presentation = await GetPresentationAsync(new()
		{ PresentationId = presentationId, IncludeSpeakers = true }, context, nameof(presentationId));
		PresentationSpeaker presentationSpeaker = presentation.PresentationSpeakers.FirstOrDefault(x => x.SpeakerId == speakerId)
			?? throw new ArgumentOutOfRangeException(nameof(speakerId), "Speaker not associated with presentation.");
		Speaker speaker = await context.Speakers.FindAsync(speakerId)
			?? throw new ArgumentOutOfRangeException(nameof(speakerId), "Speaker not found.");
		context.PresentationSpeakers.Remove(presentationSpeaker);
		await context.SaveChangesAsync();
	}

	#endregion

	private async Task<Presentation> GetPresentationAsync(
		GetPresentationOptions options,
		string presentationIdParamName = "presentationId")
	{
		using SpeakerToolkitContext speakerToolkitContext = new(_configServices);
		return await GetPresentationAsync(options, speakerToolkitContext, presentationIdParamName);
	}

	internal static async Task<Presentation> GetPresentationAsync(
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
			query = query.Include(x => x.PresentationTexts)
				.ThenInclude(x => x.LearningObjectives);
		if (options.IncludeRelatedPresentations)
			query = query.Include(x => x.RelatedPresentations)
				.ThenInclude(x => x.RelatedPresentation)
					.ThenInclude(x => x.PresentationTexts);
		if (options.IncludeSpeakers)
			query = query.Include(x => x.PresentationSpeakers)
				.ThenInclude(x => x.Speaker);
		if (options.PresentationId is not null)
			query = query.Where(x => x.PresentationId == options.PresentationId);
		return await query.ToListAsync();
	}

}