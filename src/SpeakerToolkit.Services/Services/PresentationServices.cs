namespace TaleLearnCode.SpeakerToolkit.Services;

public class PresentationServices(ConfigServices configServices) : ServicesBase(configServices)
{

	#region Related Presentations

	public async Task<RelatedPresentationsResponse> GetRelatedPresentationsAsync(int presentationId, string? languageCode = null)
		=> (await GetDatAsync(
			new()
			{
				PresentationId = presentationId,
				IncludeRelatedPresentations = true
			}))
		.ToRelatedPresentationsResponse(languageCode);

	public async Task<RelatedPresentationsResponse> AddRelatedPresentation(int presentationId, int relatedPresentationId, int? sortOrder)
	{
		using SpeakerToolkitContext context = new(_configServices);
		Presentation presentation = await GetDataAsync(new() { PresentationId = presentationId, IncludeRelatedPresentations = true }, context, nameof(presentationId));
		Presentation relatedPresentation = await GetDataAsync(new() { PresentationId = relatedPresentationId, IncludePresentationTexts = false }, context, nameof(relatedPresentationId));
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
		Presentation presentation = await GetDataAsync(new() { PresentationId = presentationId, IncludeRelatedPresentations = true }, context, nameof(presentationId));
		Presentation relatedPresentation = await GetDataAsync(new() { PresentationId = relatedPresentationId, IncludePresentationTexts = false }, context, nameof(relatedPresentationId));
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
		Presentation presentation = await GetDatAsync(new() { PresentationId = presentationId, IncludePresentationTexts = true });
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
		Presentation presentation = await GetDatAsync(new() { PresentationId = presentationId, IncludePresentationTexts = true });
		return presentation.PresentationTexts.ToPresentationTextResponseList();
	}

	public async Task AddPresentationTextAsync(int presentationId, string languageCode, PresentationTextRequest request)
	{
		using SpeakerToolkitContext context = new(_configServices);
		Presentation presentation = await GetDataAsync(new() { PresentationId = presentationId, IncludePresentationTexts = true }, context, nameof(presentationId));
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
		Presentation presentation = await GetDataAsync(new() { PresentationId = presentationId, IncludePresentationTexts = true }, context, nameof(presentationId));
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
		Presentation presentation = await GetDataAsync(new() { PresentationId = presentationId, IncludePresentationTexts = true }, context, nameof(presentationId));
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
		Presentation presentation = await GetDatAsync(new() { PresentationId = presentationId, IncludePresentationTexts = true });
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
		Presentation presentation = await GetDatAsync(new() { PresentationId = presentationId, IncludePresentationTexts = true });
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
		Presentation presentation = await GetDataAsync(new() { PresentationId = presentationId, IncludePresentationTexts = true }, context);
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
		Presentation presentation = await GetDataAsync(new() { PresentationId = presentationId, IncludePresentationTexts = true }, context);
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
		Presentation presentation = await GetDataAsync(new() { PresentationId = presentationId, IncludePresentationTexts = true }, context);
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
		=> (await GetDatAsync(new() { PresentationId = presentationId, IncludeSpeakers = true }))
		.ToSpeakerList();

	public async Task AddSpeakerAsync(int presentationId, int speakerId, bool isPrimary)
	{
		using SpeakerToolkitContext context = new(_configServices);
		Presentation presentation = await GetDataAsync(new()
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
		Presentation presentation = await GetDataAsync(new()
		{ PresentationId = presentationId, IncludeSpeakers = true }, context, nameof(presentationId));
		PresentationSpeaker presentationSpeaker = presentation.PresentationSpeakers.FirstOrDefault(x => x.SpeakerId == speakerId)
			?? throw new ArgumentOutOfRangeException(nameof(speakerId), "Speaker not associated with presentation.");
		Speaker speaker = await context.Speakers.FindAsync(speakerId)
			?? throw new ArgumentOutOfRangeException(nameof(speakerId), "Speaker not found.");
		context.PresentationSpeakers.Remove(presentationSpeaker);
		await context.SaveChangesAsync();
	}

	#endregion

	#region Presentation

	public async Task<PresentationResponse> GetPresentationAsync(GetPresentationOptions options, string? languageCode = null)
	{
		GetPresentationOptions queryOptions = options.Clone();
		queryOptions.IncludePresentationTexts = true;

		return (await GetDataListAsync(queryOptions)).FirstOrDefault(x => x.PresentationId == options.PresentationId)?.ToPresentationResponse(options, languageCode)
			?? throw new ArgumentOutOfRangeException(nameof(options), "Presentation not found.");




		//return (await GetDataListAsync(queryOptions)).Select(x => x.ToPresentationResponse(options, languageCode)).FirstOrDefault()
		//	?? throw new ArgumentOutOfRangeException(nameof(options), "Presentation not found.");
	}

	public async Task<List<PresentationResponse>> GetPresentationsAsync(GetPresentationOptions options, string? languageCode = null)
	{
		GetPresentationOptions queryOptions = options.Clone();
		queryOptions.IncludePresentationTexts = true;
		return (await GetDataListAsync(queryOptions)).Select(x => x.ToPresentationResponse(options, languageCode)).ToList();
	}

	public async Task<int> CreatePresentationAsync(PresentationRequest request)
	{
		using SpeakerToolkitContext context = new(_configServices);
		Presentation presentation = new()
		{
			PresentationTypeId = request.PresentationTypeId,
			RepoLink = request.RepoLink,
			Permalink = request.Permalink,
			IsArchived = request.IsArchived,
			IncludeInPublicProfile = request.IncludeInPublicProfile,
			DefaultLanguageCode = request.DefaultLanguageCode,
		};

		string defaultLanguageCode = await AddSpeakersToPresentation(request, context, presentation);
		await AddTagsToPresentation(request, context, presentation);
		await AddTextsToPresentation(request, context, presentation, defaultLanguageCode);
		await AddRelatedPresentationsToPresentation(request, context, presentation);

		context.Presentations.Add(presentation);
		await context.SaveChangesAsync();

		return presentation.PresentationId;

	}

	public async Task UpdatePresentationAsync(int presentationId, PresentationRequest request)
	{

		using SpeakerToolkitContext context = new(_configServices);

		Presentation presentation = await context.Presentations
			.Include(x => x.PresentationSpeakers)
				.ThenInclude(x => x.Speaker)
			.Include(x => x.PresentationTags)
				.ThenInclude(x => x.Tag)
			.Include(x => x.PresentationTexts)
				.ThenInclude(x => x.LearningObjectives)
			.Include(x => x.RelatedPresentations)
				.ThenInclude(x => x.RelatedPresentation)
			.FirstOrDefaultAsync(x => x.PresentationId == presentationId)
			?? throw new ArgumentOutOfRangeException(nameof(presentationId), "Presentation not found.");

		presentation.PresentationTypeId = request.PresentationTypeId;
		presentation.RepoLink = request.RepoLink;
		presentation.Permalink = request.Permalink;
		presentation.IsArchived = request.IsArchived;
		presentation.IncludeInPublicProfile = request.IncludeInPublicProfile;
		presentation.DefaultLanguageCode = request.DefaultLanguageCode;

		await UpdatePresentationSpeakersAsync(request, context, presentation);
		await UpdatePresentationTagsAsync(request, context, presentation);
		UpdatePresentationTexts(request, presentation);
		await UpdatePresentationRelatedPresentationsAsync(request, context, presentation);

		await context.SaveChangesAsync();

	}

	public async Task DeletePresentationAsync(int presentationId)
	{
		SpeakerToolkitContext context = new(_configServices);
		Presentation presentation = await GetPresentationForDeletionAsync(presentationId, context);
		await RemoveSpeakersFromPresentationAsync(context, presentation);
		await RemoveTagsFromPresentationAsync(context, presentation);
		await RemoveTextFromPresentationAsync(context, presentation);
		await RemoveRelatedPresentationsFromPresentationAsync(context, presentation);
		await DeletePresentationAsync(context, presentation);
	}

	private static async Task DeletePresentationAsync(SpeakerToolkitContext context, Presentation presentation)
	{
		context.Presentations.Remove(presentation);
		await context.SaveChangesAsync();
	}

	private static async Task<Presentation> GetPresentationForDeletionAsync(int presentationId, SpeakerToolkitContext context)
	{
		return await context.Presentations
			.Include(x => x.PresentationSpeakers)
			.Include(x => x.PresentationTags)
			.Include(x => x.PresentationTexts)
				.ThenInclude(x => x.LearningObjectives)
			.Include(x => x.RelatedPresentations)
			.FirstOrDefaultAsync(x => x.PresentationId == presentationId)
			?? throw new ArgumentOutOfRangeException(nameof(presentationId), "Presentation not found.");
	}

	private static async Task RemoveRelatedPresentationsFromPresentationAsync(SpeakerToolkitContext context, Presentation presentation)
	{
		foreach (PresentationRelated relatedPresentation in presentation.RelatedPresentations)
			context.RelatedPresentations.Remove(relatedPresentation);
		await context.SaveChangesAsync();
	}

	private static async Task RemoveTextFromPresentationAsync(SpeakerToolkitContext context, Presentation presentation)
	{
		foreach (PresentationText text in presentation.PresentationTexts)
		{
			foreach (LearningObjective learningObjective in text.LearningObjectives)
				context.LearningObjectives.Remove(learningObjective);
			context.PresentationTexts.Remove(text);
		}
		await context.SaveChangesAsync();
	}

	private static async Task RemoveTagsFromPresentationAsync(SpeakerToolkitContext context, Presentation presentation)
	{
		foreach (PresentationTag tag in presentation.PresentationTags)
			context.PresentationTags.Remove(tag);
		await context.SaveChangesAsync();
	}

	private static async Task RemoveSpeakersFromPresentationAsync(SpeakerToolkitContext context, Presentation presentation)
	{
		foreach (PresentationSpeaker speaker in presentation.PresentationSpeakers)
			context.PresentationSpeakers.Remove(speaker);
		await context.SaveChangesAsync();
	}

	private static async Task UpdatePresentationRelatedPresentationsAsync(PresentationRequest request, SpeakerToolkitContext context, Presentation presentation)
	{
		IEnumerable<int> existingRelatedPresentations = presentation.RelatedPresentations.Select(x => x.RelatedPresentationId).Except(request.RelatedPresentations);
		if (request.RelatedPresentations is not null && request.RelatedPresentations.Any())
		{
			foreach (int relatedPresentationId in request.RelatedPresentations.Except(presentation.RelatedPresentations.Select(x => x.RelatedPresentationId)))
			{
				Presentation relatedPresentation = await context.Presentations.FirstOrDefaultAsync(x => x.PresentationId == relatedPresentationId)
					?? throw new ArgumentOutOfRangeException(nameof(request), $"Related presentation '{relatedPresentationId}' not found.");
				presentation.RelatedPresentations.Add(new()
				{
					RelatedPresentationId = relatedPresentationId
				});
			}

			foreach (int existingRelatedPresentationId in presentation.RelatedPresentations.Select(x => x.RelatedPresentationId).Except(request.RelatedPresentations))
			{
				PresentationRelated? presentationRelated = presentation.RelatedPresentations.FirstOrDefault(x => x.RelatedPresentationId == existingRelatedPresentationId);
				if (presentationRelated is not null)
				{
					context.RelatedPresentations.Remove(presentationRelated);
				}
			}
		}
	}

	private static void UpdatePresentationTexts(PresentationRequest request, Presentation presentation)
	{
		foreach (PresentationTextRequest presentationText in request.PresentationTexts)
		{
			PresentationText? existingPresentationText = presentation.PresentationTexts.FirstOrDefault(x => x.LanguageCode == presentationText.LanguageCode);
			if (existingPresentationText is not null)
			{
				existingPresentationText.PresentationTitle = presentationText.Title;
				existingPresentationText.PresentationShortTitle = presentationText.ShortTitle;
				existingPresentationText.Abstract = presentationText.Abstract;
				existingPresentationText.ShortAbstract = presentationText.ShortAbstract;
				existingPresentationText.Summary = presentationText.Summary;
				existingPresentationText.AdditionalDetails = presentationText.AdditionalDetails;
				if (presentationText.LearningObjectives is not null && presentationText.LearningObjectives.Any())
				{
					int maxSortOrder = presentationText.LearningObjectives.Max(x => x.SortOrder) ?? 0;
					foreach (LearningObjectiveRequest learningObjective in presentationText.LearningObjectives)
					{
						int sortOrder = learningObjective.SortOrder ?? ++maxSortOrder;
						maxSortOrder++;
						LearningObjective? existingLearningObjective = existingPresentationText.LearningObjectives.FirstOrDefault(x => x.SortOrder == sortOrder);
						if (existingLearningObjective is not null)
						{
							existingLearningObjective.LearningObjectiveText = learningObjective.LearningObjective;
						}
						else
						{
							existingPresentationText.LearningObjectives.Add(new()
							{
								LearningObjectiveText = learningObjective.LearningObjective,
								SortOrder = sortOrder
							});
						}
					}
				}
			}
			else
			{
				PresentationText newPresentationText = new()
				{
					LanguageCode = presentationText.LanguageCode,
					PresentationTitle = presentationText.Title,
					PresentationShortTitle = presentationText.ShortTitle,
					Abstract = presentationText.Abstract,
					ShortAbstract = presentationText.ShortAbstract,
					Summary = presentationText.Summary,
					AdditionalDetails = presentationText.AdditionalDetails
				};
				if (presentationText.LearningObjectives is not null && presentationText.LearningObjectives.Any())
				{
					int maxSortOrder = presentationText.LearningObjectives.Max(x => x.SortOrder) ?? 0;
					foreach (LearningObjectiveRequest learningObjective in presentationText.LearningObjectives)
					{
						int sortOrder = learningObjective.SortOrder ?? ++maxSortOrder;
					}
				}
			}
		}
	}

	private static async Task UpdatePresentationTagsAsync(PresentationRequest request, SpeakerToolkitContext context, Presentation presentation)
	{
		List<string> existingTags = presentation.PresentationTags.Select(x => x.Tag.TagName).ToList();
		foreach (string tagToAdd in (request.Tags.Except(existingTags)))
		{
			presentation.PresentationTags.Add(new()
			{
				TagId = (await TagServices.GetOrCreateAsync(context, tagToAdd)).TagId
			});
		}

		foreach (string? tagToRemove in (existingTags.Except(request.Tags)))
		{
			PresentationTag? tag = presentation.PresentationTags.FirstOrDefault(x => x.Tag.TagName == tagToRemove);
			if (tag is not null)
				context.PresentationTags.Remove(tag);
		}
	}

	private static async Task UpdatePresentationSpeakersAsync(PresentationRequest request, SpeakerToolkitContext context, Presentation presentation)
	{
		string defaultLanguageCode = "en";
		if (request.Speakers is null || !request.Speakers.Any()) throw new ArgumentException("At least one speaker must be associated with the presentation.", nameof(request));
		if (request.Speakers.Where(x => x.IsPrimarySpeaker).Count() > 1) throw new ArgumentException("Only one speaker can be the the primary speaker for the presentation.", nameof(request));
		if (request.Speakers.Count() == 1) request.Speakers.First().IsPrimarySpeaker = true;
		else if (request.Speakers.Where(x => x.IsPrimarySpeaker).Any()) throw new ArgumentException("At least one speaker must be the primary speaker for the presentation.", nameof(request));

		List<Speaker> speakers = [];
		foreach (int speakerId in request.Speakers.DistinctBy(x => x.SpeakerId).Select(x => x.SpeakerId))
		{
			Speaker speaker = await context.Speakers.FirstOrDefaultAsync(x => x.SpeakerId == speakerId)
				?? throw new ArgumentOutOfRangeException(nameof(request), $"Speaker '{speakerId}' not found.");
			if (presentation.PresentationSpeakers.Select(x => x.SpeakerId).Contains(speakerId))
			{
				if (request.Speakers.First(x => x.SpeakerId == speakerId).IsPrimarySpeaker)
				{
					foreach (PresentationSpeaker existingSpeaker in presentation.PresentationSpeakers)
						existingSpeaker.IsPrimary = false;
				}
			}
			else
			{
				presentation.PresentationSpeakers.Add(new()
				{
					SpeakerId = speakerId,
					IsPrimary = request.Speakers.First(x => x.SpeakerId == speakerId).IsPrimarySpeaker
				});
			}
			speakers.Add(speaker);
		}
		defaultLanguageCode = speakers.First(x => x.SpeakerId == request.Speakers.First(x => x.IsPrimarySpeaker).SpeakerId).DefaultLanguageCode;

		List<int> speakersNotInPresentation = request.Speakers
				.Select(s => s.SpeakerId)
				.Except(presentation.PresentationSpeakers.Select(ps => ps.SpeakerId))
				.ToList();

		IEnumerable<int> speakersToRemove = presentation.PresentationSpeakers.Select(x => x.SpeakerId).Except(request.Speakers.Select(x => x.SpeakerId));
		foreach (int speakerId in speakersToRemove)
		{
			PresentationSpeaker? speaker = presentation.PresentationSpeakers.FirstOrDefault(x => x.SpeakerId == speakerId);
			if (speaker is not null)
				context.PresentationSpeakers.Remove(speaker);
		}
	}

	private static async Task AddRelatedPresentationsToPresentation(PresentationRequest request, SpeakerToolkitContext context, Presentation presentation)
	{
		if (request.RelatedPresentations is not null && request.RelatedPresentations.Any())
		{
			foreach (int relatedPresentationId in request.RelatedPresentations)
			{
				Presentation relatedPresentation = await context.Presentations.FirstOrDefaultAsync(x => x.PresentationId == relatedPresentationId)
					?? throw new ArgumentOutOfRangeException(nameof(request), $"Related presentation '{relatedPresentationId}' not found.");
				presentation.RelatedPresentations.Add(new()
				{
					RelatedPresentationId = relatedPresentationId
				});
			}
		}
	}

	private static async Task AddTextsToPresentation(PresentationRequest request, SpeakerToolkitContext context, Presentation presentation, string defaultLanguageCode)
	{
		if (request.PresentationTexts is not null && request.PresentationTexts.Any())
		{

			foreach (PresentationTextRequest presentationTextRequest in request.PresentationTexts.Where(x => x.LanguageCode == null))
				presentationTextRequest.LanguageCode = defaultLanguageCode;

			IEnumerable<string?> something = request.PresentationTexts.Select(x => x.LanguageCode).Distinct();

			List<string> languages = await context.Languages.Select(x => x.LanguageCode).ToListAsync();

			foreach (PresentationTextRequest presentationText in request.PresentationTexts)
			{
				PresentationText newPresentationText = new()
				{
					LanguageCode = languages.FirstOrDefault(presentationText.LanguageCode) ?? throw new ArgumentOutOfRangeException(nameof(request), "Invalid language code."),
					PresentationTitle = presentationText.Title,
					PresentationShortTitle = presentationText.ShortTitle,
					Abstract = presentationText.Abstract,
					ShortAbstract = presentationText.ShortAbstract,
					Summary = presentationText.Summary,
					AdditionalDetails = presentationText.AdditionalDetails
				};

				if (presentationText.LearningObjectives is not null && presentationText.LearningObjectives.Any())
				{

					int maxSortOrder = presentationText.LearningObjectives.Max(x => x.SortOrder) ?? 0;


					foreach (LearningObjectiveRequest learningObjective in presentationText.LearningObjectives)
					{
						int sortOrder = learningObjective.SortOrder ?? ++maxSortOrder;
						maxSortOrder++;
						newPresentationText.LearningObjectives.Add(new()
						{
							LearningObjectiveText = learningObjective.LearningObjective,
							SortOrder = sortOrder
						});
					}
				}

				presentation.PresentationTexts.Add(newPresentationText);
			}
		}
	}

	private static async Task AddTagsToPresentation(PresentationRequest request, SpeakerToolkitContext context, Presentation presentation)
	{
		if (request.Tags is not null && request.Tags.Any())
		{
			foreach (string tag in request.Tags)
				presentation.PresentationTags.Add(new()
				{
					TagId = (await TagServices.GetOrCreateAsync(context, tag)).TagId
				});
		}
	}

	private static async Task<string> AddSpeakersToPresentation(PresentationRequest request, SpeakerToolkitContext context, Presentation presentation)
	{
		string defaultLanguageCode = "en";
		if (request.Speakers is not null && request.Speakers.Any())
		{

			if (request.Speakers.Where(x => x.IsPrimarySpeaker).Count() > 1)
				throw new ArgumentException("Only one speaker can be the the primary speaker for the presentation.", nameof(request));

			if (request.Speakers.Count() == 1)
				request.Speakers.First().IsPrimarySpeaker = true;
			else if (request.Speakers.Where(x => x.IsPrimarySpeaker).Any())
				throw new ArgumentException("At least one speaker must be the primary speaker for the presentation.", nameof(request));

			List<Speaker> speakers = [];
			foreach (int speakerId in request.Speakers.DistinctBy(x => x.SpeakerId).Select(x => x.SpeakerId))
			{
				Speaker speaker = await context.Speakers.FirstOrDefaultAsync(x => x.SpeakerId == speakerId)
					?? throw new ArgumentOutOfRangeException(nameof(request), $"Speaker '{speakerId}' not found.");
				speakers.Add(speaker);
			}

			defaultLanguageCode = speakers.First(x => x.SpeakerId == request.Speakers.First(x => x.IsPrimarySpeaker).SpeakerId).DefaultLanguageCode;

			foreach (PresentationSpeakerRequest speaker in request.Speakers)
			{
				presentation.PresentationSpeakers.Add(new()
				{
					SpeakerId = speaker.SpeakerId,
					IsPrimary = speaker.IsPrimarySpeaker
				});
			}
		}

		return defaultLanguageCode;
	}

	private async Task<Presentation> GetDatAsync(
		GetPresentationOptions options,
		string presentationIdParamName = "presentationId")
	{
		using SpeakerToolkitContext speakerToolkitContext = new(_configServices);
		return await GetDataAsync(options, speakerToolkitContext, presentationIdParamName);
	}

	internal static async Task<Presentation> GetDataAsync(
		GetPresentationOptions options,
		SpeakerToolkitContext context,
		string presentationIdParamName = "presentationId")
	{
		return (await GetDataListAsync(options, context)).FirstOrDefault()
			?? throw new ArgumentOutOfRangeException(presentationIdParamName, "Presentation not found.");
	}

	private async Task<List<Presentation>> GetDataListAsync(GetPresentationOptions options)
	{
		using SpeakerToolkitContext context = new(_configServices);
		return await GetDataListAsync(options, context);
	}

	private static async Task<List<Presentation>> GetDataListAsync(
		GetPresentationOptions options, SpeakerToolkitContext context)
	{
		IQueryable<Presentation> query = context.Presentations.Include(x => x.PresentationType);
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
		if (options.IncludeTags)
			query = query.Include(x => x.PresentationTags)
				.ThenInclude(x => x.Tag);
		if (options.PresentationId is not null)
			query = query.Where(x => x.PresentationId == options.PresentationId);
		try
		{
			return await query.ToListAsync();
		}
		catch (Exception ex)
		{
			throw new Exception("An error occurred while retrieving the presentation data.", ex);
		}
	}

	#endregion

}