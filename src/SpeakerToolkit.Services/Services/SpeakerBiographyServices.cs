using TaleLearnCode.SpeakerToolkit.Exceptions;
using TaleLearnCode.SpeakerToolkit.Requests;

namespace TaleLearnCode.SpeakerToolkit.Services;

public class SpeakerBiographyServices(ConfigServices configServices) : ServicesBase(configServices)
{

	public async Task<List<SpeakerBiographyResponse>> GetSpeakerBiographiesAsync(int speakerId)
		=> (await GetData(speakerId)).ToResponse();

	public async Task<SpeakerBiographyResponse?> GetSpeakerBiographyAsync(int speakerId, string languageCode)
		=> (await GetData(speakerId, languageCode)).FirstOrDefault().ToResponse();

	public async Task<UpsertResponse<SpeakerBiographyResponse?>> UpsertSpeakerBiographyAsync(
		int speakerId,
		string languageCode,
		SpeakerBiographyRequest request,
		UpsertSpeakerBiographyOptions? options = null)
	{
		options ??= new();
		UpsertAction upsertAction = UpsertAction.NoAction;
		using SpeakerToolkitContext context = new(_configServices);
		SpeakerBiography? speakerBiography = await context.SpeakerBiographies
			.Include(x => x.Speaker)
			.Include(x => x.Language)
			.FirstOrDefaultAsync(x => x.SpeakerId == speakerId && x.LanguageCode == languageCode);
		if (speakerBiography is null)
		{
			if (options.CreateIfNotExists)
			{
				upsertAction = UpsertAction.Create;
				speakerBiography = new()
				{
					SpeakerId = speakerId,
					LanguageCode = languageCode,
					Title = request.Title,
					Biography = request.Biography
				};
				await context.SpeakerBiographies.AddAsync(speakerBiography);
			}
			else
				throw new ArgumentOutOfRangeException(nameof(request), $"Speaker Biography for Speaker ID {speakerId} and Language Code {languageCode} not found.");
		}
		else if (options.UpdateIfExists)
		{
			if (speakerBiography.HasChanges(request))
			{
				upsertAction = UpsertAction.Update;
				speakerBiography.Title = request.Title;
				speakerBiography.Biography = request.Biography;
			}
		}
		else
		{
			throw new ObjectAlreadyExistsException($"Speaker Biography for Speaker ID {speakerId} and Language Code {languageCode} already exists.");
		}
		await context.SaveChangesAsync();
		return new((await GetData(speakerId, languageCode)).First().ToResponse(), upsertAction);
	}

	private async Task<List<SpeakerBiography>> GetData(int speakerId, string? languageCode = null)
	{
		SpeakerToolkitContext context = new(_configServices);
		IQueryable<SpeakerBiography> query = context.SpeakerBiographies
			.Include(x => x.Speaker)
			.Include(x => x.Language);
		if (!string.IsNullOrWhiteSpace(languageCode))
			query = query.Where(x => x.SpeakerId == speakerId && x.LanguageCode == languageCode);
		else
			query = query.Where(x => x.SpeakerId == speakerId);
		query = query.OrderBy(x => x.LanguageCode);
		return await query.ToListAsync();
	}

}