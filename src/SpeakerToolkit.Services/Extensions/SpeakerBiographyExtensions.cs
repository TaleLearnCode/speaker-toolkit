using TaleLearnCode.SpeakerToolkit.Requests;

namespace TaleLearnCode.SpeakerToolkit.Extensions;

internal static class SpeakerBiographyExtensions
{
	internal static SpeakerBiographyResponse? ToResponse(this SpeakerBiography? speakerBiography)
		=> speakerBiography is null ? null : new()
		{
			SpeakerId = speakerBiography.SpeakerId,
			SpeakerName = speakerBiography.Speaker.FullName,
			LanguageCode = speakerBiography.LanguageCode,
			LanguageName = speakerBiography.Language.LanguageName,
			Title = speakerBiography.Title,
			Biography = speakerBiography.Biography
		};

	internal static List<SpeakerBiographyResponse> ToResponse(this IEnumerable<SpeakerBiography> speakerBiographies)
	{
		List<SpeakerBiographyResponse> response = [];
		foreach (SpeakerBiographyResponse? speakerBiographyResponse in speakerBiographies.Select(sb => sb.ToResponse()).ToList())
			if (speakerBiographyResponse is not null)
				response.Add(speakerBiographyResponse);
		return response;
	}

	internal static bool HasChanges(this SpeakerBiography speakerBiography, SpeakerBiographyRequest request)
		=> speakerBiography.Title != request.Title || speakerBiography.Biography != request.Biography;
}