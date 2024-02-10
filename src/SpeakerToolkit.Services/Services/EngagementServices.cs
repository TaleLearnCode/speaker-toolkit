namespace TaleLearnCode.SpeakerToolkit.Services;

public class EngagementServices(ConfigServices configServices) : ServicesBase(configServices)
{

	#region Engagement

	private async Task<List<Engagement>> GetEngagementList(int engagementId)
	{
		using SpeakerToolkitContext context = new(_configServices);
		return await GetEngagementList(context, engagementId);
	}

	private async Task<List<Engagement>> GetEngagementList(SpeakerToolkitContext context, int engagementId)
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
		Presentation presentation = (await PresentationServices.GetPresentationAsync(new() { PresentationId = presentationId }, context));
		EngagementStatus engagementStatus = await context.EngagementStatuses.FirstOrDefaultAsync(es => es.EngagementStatusId == request.StatusId) ?? throw new ArgumentOutOfRangeException(nameof(request), "Status not found.");
		if (engagement.EngagementPresentations.Any(ep => ep.PresentationId == presentationId))
			throw new ObjectAlreadyExistsException("Presentation already added to engagement.");
		engagement.EngagementPresentations.Add(new()
		{
			PresentationId = presentationId,
			StatusId = request.StatusId,
			StartDateTime = request.StartDateTime,
			EndDateTime = request.EndDateTime,
			TimeZone = request.TimeZone,
			Room = request.Room
		});
		await context.SaveChangesAsync();
	}

	public async Task UpdateEngagementPresentation(int engagementId, int presentationId, EngagementPresentationRequest request)
	{
		if (request.PresentationId != presentationId) throw new ArgumentException("Presentation ID does not match request.", nameof(presentationId));
		if (request.EngagementId != engagementId) throw new ArgumentException("Engagement ID does not match request.", nameof(engagementId));
		SpeakerToolkitContext context = new(_configServices);
		Engagement engagement = await GetEngagementDataAsync(context, engagementId);
		Presentation presentation = await PresentationServices.GetPresentationAsync(new() { PresentationId = presentationId }, context);
		EngagementStatus engagementStatus = await context.EngagementStatuses.FirstOrDefaultAsync(es => es.EngagementStatusId == request.StatusId) ?? throw new ArgumentOutOfRangeException(nameof(request), "Status not found.");
		EngagementPresentation engagementPresentation = engagement.EngagementPresentations.FirstOrDefault(ep => ep.PresentationId == presentationId) ?? throw new ArgumentOutOfRangeException(nameof(request), "Presentation not found in engagement.");
		engagementPresentation.StatusId = request.StatusId;
		engagementPresentation.StartDateTime = request.StartDateTime;
		engagementPresentation.EndDateTime = request.EndDateTime;
		engagementPresentation.TimeZone = request.TimeZone;
		engagementPresentation.Room = request.Room;
		await context.SaveChangesAsync();
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

	private async Task<List<EngagementPresentation>> GetEngagementPresentationDataAsync(SpeakerToolkitContext context, int engagementId, int? presentationId = null)
	{
		IQueryable<EngagementPresentation> query = context.EngagementPresentations
			.Include(ep => ep.Engagement)
			.Include(ep => ep.Presentation)
				.ThenInclude(p => p.PresentationTexts)
			.Include(ep => ep.Status)
			.Where(ep => ep.EngagementId == engagementId);
		if (presentationId.HasValue)
			query = query.Where(ep => ep.PresentationId == presentationId);
		return await query.ToListAsync();
	}

	#endregion

}