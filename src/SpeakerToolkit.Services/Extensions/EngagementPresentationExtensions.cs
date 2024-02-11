namespace TaleLearnCode.SpeakerToolkit.Extensions;

internal static class EngagementPresentationExtensions
{

	internal static EngagementPresentationResponse ToResponse(this EngagementPresentation engagementPresentation, string? languageCode = null)
	{
		return new EngagementPresentationResponse
		{
			Id = engagementPresentation.EngagementPresentationId,
			EngagementId = engagementPresentation.EngagementId,
			EngagementName = engagementPresentation.Engagement?.EngagementName,
			PresentationId = engagementPresentation.PresentationId,
			PresentationTitle = engagementPresentation.Presentation?
				.PresentationTexts.FirstOrDefault(pt => pt.LanguageCode == (languageCode ?? engagementPresentation.Presentation.DefaultLanguageCode))?.PresentationTitle
				?? engagementPresentation.Presentation?.PresentationTexts.FirstOrDefault(pt => pt.LanguageCode == "en")?.PresentationTitle
				?? engagementPresentation.Presentation?.PresentationTexts.FirstOrDefault()?.PresentationTitle,
			StatusId = engagementPresentation.StatusId,
			StatusName = engagementPresentation.Status?.EngagementStatusName,
			StartDateTime = engagementPresentation.StartDateTime,
			EndDateTime = engagementPresentation.EndDateTime,
			TimeZone = engagementPresentation.TimeZone,
			Room = engagementPresentation.Room
		};
	}

	internal static List<EngagementPresentationResponse> ToResponse(this IEnumerable<EngagementPresentation> engagementPresentations, string? languageCode = null)
		=> engagementPresentations.Select(engagementPresentation => engagementPresentation.ToResponse(languageCode)).ToList();

}