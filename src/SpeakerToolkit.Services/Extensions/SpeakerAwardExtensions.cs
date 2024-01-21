namespace TaleLearnCode.SpeakerToolkit.Extensions;

internal static class SpeakerAwardExtensions
{

	internal static SpeakerAwardResponse? ToResponse(this SpeakerAward? speakerAward)
		=> speakerAward is null ? null : new()
		{
			Id = speakerAward.SpeakerAwardId,
			SpeakerId = speakerAward.SpeakerId,
			SpeakerName = speakerAward.Speaker.FullName,
			SpeakerAwardType = speakerAward.SpeakerAwardType.ToResponse(),
			AwardCategory = speakerAward.AwardCategory,
			AwardYear = speakerAward.AwardYear,
			AwardProfileUrl = speakerAward.AwardProfileUrl
		};

	internal static List<SpeakerAwardResponse> ToResponse(this IEnumerable<SpeakerAward> speakerAwards)
	{
		List<SpeakerAwardResponse> response = [];
		foreach (SpeakerAwardResponse? speakerAwardResponse in speakerAwards.Select(sa => sa.ToResponse()).ToList())
			if (speakerAwardResponse is not null)
				response.Add(speakerAwardResponse);
		return response;
	}

	internal static bool HasChanges(this SpeakerAward speakerAward, SpeakerAwardRequest request)
		=> speakerAward.SpeakerAwardTypeId != request.SpeakerAwardTypeId
			|| speakerAward.AwardCategory != request.AwardCategory
			|| speakerAward.AwardYear != request.AwardYear
			|| speakerAward.AwardProfileUrl != request.AwardProfileUrl;

}