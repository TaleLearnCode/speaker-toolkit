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
			Room = engagementPresentation.Room,
			Speakers = engagementPresentation.EngagementPresentationSpeakers.ToSpeakerList(),
			Downloads = engagementPresentation.EngagementPresentationDownloads.ToResponse()
		};
	}

	internal static List<EngagementPresentationResponse> ToResponse(this IEnumerable<EngagementPresentation> engagementPresentations, string? languageCode = null)
		=> engagementPresentations.Select(engagementPresentation => engagementPresentation.ToResponse(languageCode)).ToList();

	internal static List<PresentationSpeakerListItemResponse> ToSpeakerList(this ICollection<EngagementPresentationSpeaker> engagementPresentationSpeakers)
	{
		List<PresentationSpeakerListItemResponse> speakerList = [];
		foreach (EngagementPresentationSpeaker speaker in engagementPresentationSpeakers)
			speakerList.Add(speaker.ToSpeakerItem());
		return speakerList;
	}

	internal static PresentationSpeakerListItemResponse ToSpeakerItem(this EngagementPresentationSpeaker engagementPresentationSpeaker)
		=> new()
		{
			Id = engagementPresentationSpeaker.SpeakerId,
			Name = engagementPresentationSpeaker.Speaker.FullName,
			IsPrimary = engagementPresentationSpeaker.IsPrimarySpeaker
		};

	internal static EngagementPresentationDownloadResponse ToResponse(this EngagementPresentationDownload engagementPresentationDownload)
		=> new()
		{
			Id = engagementPresentationDownload.EngagementPresentationDownloadId,
			DownloadName = engagementPresentationDownload.DownloadName,
			DownloadUrl = engagementPresentationDownload.DownloadUrl
		};

	internal static List<EngagementPresentationDownloadResponse> ToResponse(this IEnumerable<EngagementPresentationDownload> engagementPresentationDownloads)
		=> engagementPresentationDownloads.Select(engagementPresentationDownload => engagementPresentationDownload.ToResponse()).ToList();

}