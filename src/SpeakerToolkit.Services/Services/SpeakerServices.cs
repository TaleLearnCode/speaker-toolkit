namespace TaleLearnCode.SpeakerToolkit.Services;

public class SpeakerServices(ConfigServices configServices) : ServicesBase(configServices)
{

	public async Task<SpeakerResponse?> GetSpeakerAsync(int speakerId, GetSpeakerOptions? options = null)
	{
		options ??= new();
		return (await GetDataAsync(speakerId, options)).ToResponse(options);
	}

	public async Task<SpeakerResponse?> CreateSpeakerAsync(SpeakerRequest request)
	{
		using SpeakerToolkitContext context = new(_configServices);
		Speaker? speaker = new()
		{
			FirstName = request.FirstName,
			LastName = request.LastName,
			EnablePublicProfile = request.EnablePublicProfile,
			PublicProfileUrl = request.PublicProfileUrl,
			CountryCode = request.CountryCode,
			CountryDivisionCode = request.CountryDivisionCode,
			DefaultLanguageCode = request.DefaultLanguageCode
		};
		await context.Speakers.AddAsync(speaker);
		await context.SaveChangesAsync();
		return (await GetDataAsync(speaker.SpeakerId, new())).ToResponse(new());
	}

	public async Task<SpeakerResponse?> UpdateSpeakerAsync(int speakerId, SpeakerRequest request)
	{
		GetSpeakerOptions options = new();
		using SpeakerToolkitContext context = new(_configServices);
		Speaker? speaker = await GetDataAsync(context, speakerId, options);
		if (speaker is null)
		{
			throw new ArgumentOutOfRangeException(nameof(speakerId), "Speaker not found.");
		}
		else
		{
			speaker.FirstName = request.FirstName;
			speaker.LastName = request.LastName;
			speaker.EnablePublicProfile = request.EnablePublicProfile;
			speaker.PublicProfileUrl = request.PublicProfileUrl;
			speaker.CountryCode = request.CountryCode;
			speaker.CountryDivisionCode = request.CountryDivisionCode;
			await context.SaveChangesAsync();
		}
		return (await GetDataAsync(speakerId, new())).ToResponse(new());
	}

	private async Task<Speaker?> GetDataAsync(int speakerId, GetSpeakerOptions options)
	{
		using SpeakerToolkitContext context = new(_configServices);
		IQueryable<Speaker> query = context.Speakers
			.Include(x => x.Country)
			.Include(x => x.CountryDivision)
			.Include(x => x.DefaultLanguage);
		if (options.GetBiographies)
			query = query.Include(x => x.SpeakerBiographies).ThenInclude(x => x.Language);
		if (options.GetLinks)
			query = query.Include(x => x.SpeakerLinks);
		if (options.GetAwards)
			query = query.Include(x => x.SpeakerAwards).ThenInclude(x => x.SpeakerAwardType);
		query = query.Where(x => x.SpeakerId == speakerId);
		return await query.FirstOrDefaultAsync();
	}

	private static async Task<Speaker?> GetDataAsync(
		SpeakerToolkitContext context, int speakerId, GetSpeakerOptions options)
	{
		IQueryable<Speaker> query = context.Speakers
			.Include(x => x.Country)
			.Include(x => x.CountryDivision)
			.Include(x => x.DefaultLanguage);
		if (options.GetBiographies)
			query = query.Include(x => x.SpeakerBiographies).ThenInclude(x => x.Language);
		if (options.GetLinks)
			query = query.Include(x => x.SpeakerLinks);
		if (options.GetAwards)
			query = query.Include(x => x.SpeakerAwards).ThenInclude(x => x.SpeakerAwardType);
		query = query.Where(x => x.SpeakerId == speakerId);
		return await query.FirstOrDefaultAsync();
	}

}