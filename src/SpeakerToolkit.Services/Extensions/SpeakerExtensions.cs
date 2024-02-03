namespace TaleLearnCode.SpeakerToolkit.Extensions;

internal static class SpeakerExtensions
{

	internal static SpeakerResponse? ToResponse(this Speaker? speaker, GetSpeakerOptions options)
		=> speaker is null ? null : new()
		{
			Id = speaker.SpeakerId,
			FirstName = speaker.FirstName,
			LastName = speaker.LastName,
			EnablePublicProfile = speaker.EnablePublicProfile,
			PublicProfileUrl = speaker.PublicProfileUrl,
			CountryCode = speaker.CountryCode,
			CountryName = speaker.Country?.CountryName ?? string.Empty,
			CountryDivisionCode = speaker.CountryDivisionCode,
			CountryDivisionName = speaker.CountryDivision?.CountryDivisionName ?? string.Empty,
			DefaultLanguageCode = speaker.DefaultLanguageCode,
			DefaultLanguage = speaker.DefaultLanguage?.LanguageName ?? string.Empty,
			Biographies = (options.GetBiographies && speaker.SpeakerBiographies.Count != 0) ? speaker.SpeakerBiographies.ToResponse() : null,
			Links = (options.GetLinks && speaker.SpeakerLinks.Count != 0) ? speaker.SpeakerLinks.ToResponse().ToList() : null,
			Awards = (options.GetAwards && speaker.SpeakerAwards.Count != 0) ? speaker.SpeakerAwards.ToResponse().ToList() : null
		};

}