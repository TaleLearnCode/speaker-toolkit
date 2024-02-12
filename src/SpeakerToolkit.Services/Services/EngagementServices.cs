namespace TaleLearnCode.SpeakerToolkit.Services;

public class EngagementServices(ConfigServices configServices) : ServicesBase(configServices)
{

	#region Engagement

	private async Task<List<Engagement>> GetEngagementList(int engagementId)
	{
		using SpeakerToolkitContext context = new(_configServices);
		return await GetEngagementList(context, engagementId);
	}

	private static async Task<List<Engagement>> GetEngagementList(SpeakerToolkitContext context, int engagementId)
	{
		return await context.Engagements
			.Include(e => e.EngagementPresentations)
				.ThenInclude(ep => ep.Presentation)
					.ThenInclude(p => p.PresentationTexts)
			.Where(e => e.EngagementId == engagementId)
			.ToListAsync();
	}

	private async Task<Engagement> GetEngagementDataAsync(SpeakerToolkitContext context, int engagementId, string presentationIdParamName = "engagementId")
	{
		return (await GetEngagementList(context, engagementId)).FirstOrDefault() ?? throw new ArgumentOutOfRangeException(nameof(engagementId), "Engagement not found.");
	}

	#endregion

	#region Engagement Presentation

	public async Task<List<EngagementPresentationResponse>> GetEngagementPresentationsAsync(int engagementId, string? languageCode = null)
		=> (await GetEngagementPresentationDataAsync(engagementId)).ToResponse(languageCode);

	public async Task<EngagementPresentationResponse?> GetEngagementPresentationAsync(int engagementId, int presentationId, string? languageCode = null)
		=> (await GetEngagementPresentationDataAsync(engagementId, presentationId)).FirstOrDefault()?.ToResponse(languageCode);

	public async Task AddPresentationToEngagementAsync(int engagementId, int presentationId, EngagementPresentationRequest request)
	{

		if (request.PresentationId != presentationId) throw new ArgumentException("Presentation ID does not match request.", nameof(presentationId));
		if (request.EngagementId != engagementId) throw new ArgumentException("Engagement ID does not match request.", nameof(engagementId));
		SpeakerToolkitContext context = new(_configServices);
		Engagement engagement = await GetEngagementDataAsync(context, engagementId);
		Presentation presentation = (await PresentationServices.GetDataAsync(new() { PresentationId = presentationId }, context));
		EngagementStatus engagementStatus = await context.EngagementStatuses.FirstOrDefaultAsync(es => es.EngagementStatusId == request.StatusId) ?? throw new ArgumentOutOfRangeException(nameof(request), "Status not found.");
		if (engagement.EngagementPresentations.Any(ep => ep.PresentationId == presentationId))
			throw new ObjectAlreadyExistsException("Presentation already added to engagement.");

		await ValidateEngagementPresentationSpeakersRequestAsync(request, context);

		EngagementPresentation engagementPresentation = new()
		{
			PresentationId = presentationId,
			StatusId = request.StatusId,
			StartDateTime = request.StartDateTime,
			EndDateTime = request.EndDateTime,
			TimeZone = request.TimeZone,
			Room = request.Room
		};

		foreach (EngagementPresentationSpeakerRequest speaker in request.Speakers)
		{
			engagementPresentation.EngagementPresentationSpeakers.Add(new()
			{
				SpeakerId = speaker.SpeakerId,
				IsPrimarySpeaker = speaker.IsPrimary
			});
		}

		engagement.EngagementPresentations.Add(engagementPresentation);

		if (request.Downloads != null)
		{
			foreach (EngagementPresentationDownloadRequest download in request.Downloads)
			{
				engagementPresentation.EngagementPresentationDownloads.Add(new()
				{
					DownloadName = download.DownloadName,
					DownloadUrl = download.DownloadUrl
				});
			}
		}

		await context.SaveChangesAsync();
	}

	private static async Task ValidateEngagementPresentationSpeakersRequestAsync(EngagementPresentationRequest request, SpeakerToolkitContext context)
	{
		if (request.Speakers.Count == 0)
			throw new ArgumentException("At least one speaker must be assigned to the presentation.", nameof(request));
		else if (request.Speakers.Count == 1)
			request.Speakers[0].IsPrimary = true;
		else if (request.Speakers.Where(x => x.IsPrimary).Count() > 1)
			throw new ArgumentException("Only one primary speaker can be assigned to the presentation.", nameof(request));

		List<Speaker> speakers = await context.Speakers.Where(s => request.Speakers.Select(x => x.SpeakerId).Contains(s.SpeakerId)).ToListAsync();
		if (speakers.Count != request.Speakers.Count)
			throw new ArgumentOutOfRangeException(nameof(request), "One or more speakers not found.");
	}

	public async Task UpdateEngagementPresentation(int engagementId, int presentationId, EngagementPresentationRequest request)
	{

		if (request.PresentationId != presentationId) throw new ArgumentException("Presentation ID does not match request.", nameof(presentationId));
		if (request.EngagementId != engagementId) throw new ArgumentException("Engagement ID does not match request.", nameof(engagementId));
		SpeakerToolkitContext context = new(_configServices);

		Engagement engagement = await context.Engagements
			.Include(e => e.EngagementPresentations)
				.ThenInclude(ep => ep.Presentation)
			.Include(x => x.EngagementPresentations)
				.ThenInclude(x => x.EngagementPresentationSpeakers)
			.FirstOrDefaultAsync(x => x.EngagementId == engagementId)
			?? throw new ArgumentOutOfRangeException(nameof(engagementId), "Engagement not found.");

		EngagementPresentation engagementPresentation = engagement.EngagementPresentations.FirstOrDefault(ep => ep.PresentationId == presentationId)
			?? throw new ArgumentOutOfRangeException(nameof(request), "Presentation not found in engagement.");

		await ValidateEngagementPresentationSpeakersRequestAsync(request, context);

		EngagementStatus engagementStatus = await context.EngagementStatuses.FirstOrDefaultAsync(es => es.EngagementStatusId == request.StatusId) ?? throw new ArgumentOutOfRangeException(nameof(request), "Status not found.");

		await UpdatePresentationSpeakersAsync(request, context, engagementPresentation);

		await UpdatePresentationDownloadsAsync(request, context, engagementPresentation);

		engagementPresentation.StatusId = request.StatusId;
		engagementPresentation.StartDateTime = request.StartDateTime;
		engagementPresentation.EndDateTime = request.EndDateTime;
		engagementPresentation.TimeZone = request.TimeZone;
		engagementPresentation.Room = request.Room;
		await context.SaveChangesAsync();

	}

	private async Task UpdatePresentationDownloadsAsync(EngagementPresentationRequest request, SpeakerToolkitContext context, EngagementPresentation engagementPresentation)
	{
		if (request.Downloads is not null)
		{
			IEnumerable<EngagementPresentationDownload> removedDownloads = engagementPresentation.EngagementPresentationDownloads.Where(ed => !request.Downloads.Any(d => d.DownloadName == ed.DownloadName && d.DownloadUrl == ed.DownloadUrl));
			if (removedDownloads.Any())
			{
				foreach (EngagementPresentationDownload removedDownload in removedDownloads)
					context.EngagementPresentationDownloads.Remove(removedDownload);
				await context.SaveChangesAsync();
			}

			IEnumerable<EngagementPresentationDownloadRequest> addedDownloads = request.Downloads
				.Where(d => !(engagementPresentation.EngagementPresentationDownloads.Select(x => x.DownloadName).ToList()).Contains(d.DownloadName));
			if (addedDownloads.Any())
			{
				foreach (EngagementPresentationDownloadRequest addedDownload in addedDownloads)
					engagementPresentation.EngagementPresentationDownloads.Add(new()
					{
						DownloadName = addedDownload.DownloadName,
						DownloadUrl = addedDownload.DownloadUrl
					});
				await context.SaveChangesAsync();
			}
		}
		else
		{
			if (engagementPresentation.EngagementPresentationDownloads.Count != 0)
			{
				foreach (EngagementPresentationDownload download in engagementPresentation.EngagementPresentationDownloads)
					context.EngagementPresentationDownloads.Remove(download);
				await context.SaveChangesAsync();
			}
		}
	}

	private static async Task UpdatePresentationSpeakersAsync(EngagementPresentationRequest request, SpeakerToolkitContext context, EngagementPresentation engagementPresentation)
	{
		IEnumerable<EngagementPresentationSpeaker> removedSpeakers = engagementPresentation.EngagementPresentationSpeakers.Where(es => !request.Speakers.Any(s => s.SpeakerId == es.SpeakerId));
		if (removedSpeakers.Any())
		{
			foreach (EngagementPresentationSpeaker removedSpeaker in removedSpeakers)
				context.EngagementPresentationSpeakers.Remove(removedSpeaker);
			await context.SaveChangesAsync();
		}

		IEnumerable<EngagementPresentationSpeaker> addedSpeakers = engagementPresentation.EngagementPresentationSpeakers
			.Where(es => !(request.Speakers.Select(s => s.SpeakerId).ToList()).Contains(es.SpeakerId));
		if (addedSpeakers.Any())
		{
			foreach (EngagementPresentationSpeaker addedSpeaker in addedSpeakers)
				engagementPresentation.EngagementPresentationSpeakers.Add(new()
				{
					SpeakerId = addedSpeaker.SpeakerId,
					IsPrimarySpeaker = addedSpeaker.IsPrimarySpeaker
				});
			await context.SaveChangesAsync();
		}
	}

	public async Task RemovePresentationFromEngagementAsync(int engagementId, int presentationId)
	{
		SpeakerToolkitContext context = new(_configServices);
		EngagementPresentation engagementPresentation = await context.EngagementPresentations.FirstOrDefaultAsync(x => x.EngagementId == engagementId && x.PresentationId == presentationId)
			?? throw new ArgumentOutOfRangeException(nameof(presentationId), "Presentation not found in engagement.");
		context.EngagementPresentations.Remove(engagementPresentation);
		await context.SaveChangesAsync();
	}

	private async Task<List<EngagementPresentation>> GetEngagementPresentationDataAsync(int engagementId, int? presentationId = null)
	{
		using SpeakerToolkitContext context = new(_configServices);
		return await GetEngagementPresentationDataAsync(context, engagementId, presentationId);
	}

	private static async Task<List<EngagementPresentation>> GetEngagementPresentationDataAsync(SpeakerToolkitContext context, int engagementId, int? presentationId = null)
	{
		IQueryable<EngagementPresentation> query = context.EngagementPresentations
			.Include(ep => ep.Engagement)
			.Include(ep => ep.Presentation)
				.ThenInclude(p => p.PresentationTexts)
			.Include(x => x.EngagementPresentationSpeakers)
				.ThenInclude(x => x.Speaker)
			.Include(x => x.EngagementPresentationDownloads)
			.Include(ep => ep.Status)
			.Where(ep => ep.EngagementId == engagementId);
		if (presentationId.HasValue)
			query = query.Where(ep => ep.PresentationId == presentationId);
		return await query.ToListAsync();
	}

	#endregion

	#region Engagement Presentation Speakers

	public async Task<List<PresentationSpeakerListItemResponse>> GetEngagementPresentationSpeakers(int engagementId, int presentationId)
	{
		using SpeakerToolkitContext context = new(_configServices);
		EngagementPresentation engagementPresentation = await context.EngagementPresentations
			.Include(x => x.EngagementPresentationSpeakers)
				.ThenInclude(x => x.Speaker)
			.FirstOrDefaultAsync(x => x.EngagementId == engagementId && x.PresentationId == presentationId)
			?? throw new ArgumentOutOfRangeException(nameof(engagementId), "Unable to find the engagement or presentation.");
		return engagementPresentation.EngagementPresentationSpeakers.ToSpeakerList();
	}

	public async Task<PresentationSpeakerListItemResponse> GetEngagementPresentationSpeakerAsync(int engagementId, int presentationId, int speakerId)
	{
		using SpeakerToolkitContext context = new(_configServices);
		EngagementPresentation engagementPresentation = await context.EngagementPresentations
			.Include(x => x.EngagementPresentationSpeakers)
				.ThenInclude(x => x.Speaker)
			.FirstOrDefaultAsync(x => x.EngagementId == engagementId && x.PresentationId == presentationId)
			?? throw new ArgumentOutOfRangeException(nameof(engagementId), "Unable to find the engagement or presentation.");
		EngagementPresentationSpeaker engagementPresentationSpeaker = engagementPresentation.EngagementPresentationSpeakers.FirstOrDefault(x => x.SpeakerId == speakerId)
			?? throw new ArgumentOutOfRangeException(nameof(speakerId), "Speaker not found in presentation.");
		return engagementPresentationSpeaker.ToSpeakerItem();
	}

	public async Task AddSpeakerToEngagementPresentationAsync(int engagementId, int presentationId, int speakerId, bool isPrimary)
	{

		SpeakerToolkitContext context = new(_configServices);

		Engagement engagement = await context.Engagements
			.Include(x => x.EngagementPresentations)
				.ThenInclude(x => x.EngagementPresentationSpeakers)
			.FirstOrDefaultAsync(x => x.EngagementId == engagementId)
			?? throw new ArgumentOutOfRangeException(nameof(engagementId), "Engagement not found.");

		EngagementPresentation engagementPresentation = engagement.EngagementPresentations.FirstOrDefault(ep => ep.PresentationId == presentationId)
			?? throw new ArgumentOutOfRangeException(nameof(presentationId), "Presentation not found in engagement.");

		Speaker speaker = await context.Speakers.FirstOrDefaultAsync(x => x.SpeakerId == speakerId)
			?? throw new ArgumentOutOfRangeException(nameof(speakerId), "Speaker not found.");

		if (engagementPresentation.EngagementPresentationSpeakers.Any(x => x.SpeakerId == speakerId))
			throw new ObjectAlreadyExistsException("Speaker already added to presentation.");

		if (isPrimary)
		{
			IEnumerable<EngagementPresentationSpeaker> currentPrimarySpeakers = engagementPresentation.EngagementPresentationSpeakers.Where(x => x.IsPrimarySpeaker == true);
			if (currentPrimarySpeakers.Any())
			{
				foreach (EngagementPresentationSpeaker currentPrimarySpeaker in currentPrimarySpeakers)
					currentPrimarySpeaker.IsPrimarySpeaker = false;
				await context.SaveChangesAsync();
			}
		}

		engagementPresentation.EngagementPresentationSpeakers.Add(new()
		{
			SpeakerId = speakerId,
			IsPrimarySpeaker = isPrimary
		});
		await context.SaveChangesAsync();

	}

	public async Task RemoveSpeakerFromEngagementPresentationAsync(int engagementId, int presentationId, int speakerId)
	{

		SpeakerToolkitContext context = new(_configServices);

		Engagement engagement = await context.Engagements
			.Include(x => x.EngagementPresentations)
				.ThenInclude(x => x.EngagementPresentationSpeakers)
			.FirstOrDefaultAsync(x => x.EngagementId == engagementId)
			?? throw new ArgumentOutOfRangeException(nameof(engagementId), "Engagement not found.");

		EngagementPresentation engagementPresentation = engagement.EngagementPresentations.FirstOrDefault(ep => ep.PresentationId == presentationId)
			?? throw new ArgumentOutOfRangeException(nameof(presentationId), "Presentation not found in engagement.");

		Speaker speaker = await context.Speakers.FirstOrDefaultAsync(x => x.SpeakerId == speakerId)
			?? throw new ArgumentOutOfRangeException(nameof(speakerId), "Speaker not found.");

		if (engagementPresentation.EngagementPresentationSpeakers.Any(x => x.SpeakerId == speakerId))
			throw new ArgumentOutOfRangeException(nameof(speakerId), "Speaker is not associated with the engagement presentation.");

		EngagementPresentationSpeaker engagementPresentationSpeaker = engagementPresentation.EngagementPresentationSpeakers.First(x => x.SpeakerId == speakerId);
		engagementPresentation.EngagementPresentationSpeakers.Remove(engagementPresentationSpeaker);
		await context.SaveChangesAsync();

	}

	#endregion

	#region Engagement Presentation Downloads

	public async Task<List<EngagementPresentationDownloadResponse>> GetEngagementPresentationDownloads(int engagementId, int presentationId)
	{
		using SpeakerToolkitContext context = new(_configServices);
		EngagementPresentation engagementPresentation = await context.EngagementPresentations
			.Include(x => x.EngagementPresentationDownloads)
			.FirstOrDefaultAsync(x => x.EngagementId == engagementId && x.PresentationId == presentationId)
			?? throw new ArgumentOutOfRangeException(nameof(engagementId), "Unable to find the engagement or presentation.");
		return engagementPresentation.EngagementPresentationDownloads.ToResponse();
	}

	public async Task<EngagementPresentationDownloadResponse> GetEngagementPresentationDownloadAsync(int engagementId, int presentationId, int downloadId)
	{
		using SpeakerToolkitContext context = new(_configServices);
		EngagementPresentation engagementPresentation = await context.EngagementPresentations
			.Include(x => x.EngagementPresentationDownloads)
			.FirstOrDefaultAsync(x => x.EngagementId == engagementId && x.PresentationId == presentationId)
			?? throw new ArgumentOutOfRangeException(nameof(engagementId), "Unable to find the engagement or presentation.");
		EngagementPresentationDownload engagementPresentationDownload = engagementPresentation.EngagementPresentationDownloads.FirstOrDefault(x => x.EngagementPresentationDownloadId == downloadId)
			?? throw new ArgumentOutOfRangeException(nameof(downloadId), "Download not found in presentation.");
		return engagementPresentationDownload.ToResponse();
	}

	public async Task<int> AddDownloadToEngagementPresentationAsync(int engagementId, int presentationId, EngagementPresentationDownloadRequest request)
	{

		SpeakerToolkitContext context = new(_configServices);

		Engagement engagement = await context.Engagements
			.Include(x => x.EngagementPresentations)
				.ThenInclude(x => x.EngagementPresentationDownloads)
			.FirstOrDefaultAsync(x => x.EngagementId == engagementId)
			?? throw new ArgumentOutOfRangeException(nameof(engagementId), "Engagement not found.");

		EngagementPresentation engagementPresentation = engagement.EngagementPresentations.FirstOrDefault(ep => ep.PresentationId == presentationId)
			?? throw new ArgumentOutOfRangeException(nameof(presentationId), "Presentation not found in engagement.");

		if (engagementPresentation.EngagementPresentationDownloads.Any(x => x.DownloadName == request.DownloadName && x.DownloadUrl == request.DownloadUrl))
			throw new ObjectAlreadyExistsException("Download already added to presentation.");

		EngagementPresentationDownload engagementPresentationDownload = new()
		{
			DownloadName = request.DownloadName,
			DownloadUrl = request.DownloadUrl
		};
		engagementPresentation.EngagementPresentationDownloads.Add(engagementPresentationDownload);
		await context.SaveChangesAsync();

		return engagementPresentationDownload.EngagementPresentationDownloadId;

	}

	public async Task RemoveDownloadFromEngagementPresentationAsync(int engagementId, int presentationId, int downloadId)
	{

		SpeakerToolkitContext context = new(_configServices);

		Engagement engagement = await context.Engagements
			.Include(x => x.EngagementPresentations)
				.ThenInclude(x => x.EngagementPresentationDownloads)
			.FirstOrDefaultAsync(x => x.EngagementId == engagementId)
			?? throw new ArgumentOutOfRangeException(nameof(engagementId), "Engagement not found.");

		EngagementPresentation engagementPresentation = engagement.EngagementPresentations.FirstOrDefault(ep => ep.PresentationId == presentationId)
			?? throw new ArgumentOutOfRangeException(nameof(presentationId), "Presentation not found in engagement.");

		EngagementPresentationDownload engagementPresentationDownload = engagementPresentation.EngagementPresentationDownloads.FirstOrDefault(x => x.EngagementPresentationDownloadId == downloadId)
			?? throw new ArgumentOutOfRangeException(nameof(downloadId), "Download not found in presentation.");

		context.EngagementPresentationDownloads.Remove(engagementPresentationDownload);
		await context.SaveChangesAsync();

	}

	#endregion

}