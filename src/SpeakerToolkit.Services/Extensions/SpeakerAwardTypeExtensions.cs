namespace TaleLearnCode.SpeakerToolkit.Extensions;

internal static class SpeakerAwardTypeExtensions
{

	internal static SpeakerAwardTypeResponse? ToResponse(this SpeakerAwardType? speakerAwardType)
		=> speakerAwardType is null ? null : new()
		{
			Id = speakerAwardType.SpeakerAwardTypeId,
			Name = speakerAwardType.SpeakerAwardTypeName,
			HasCategories = speakerAwardType.HasCategories,
			HasAwardYears = speakerAwardType.HasAwardYears
		};

	internal static List<SpeakerAwardTypeResponse> ToResponse(this IEnumerable<SpeakerAwardType> speakerAwardTypes)
	{
		List<SpeakerAwardTypeResponse> response = [];
		foreach (SpeakerAwardTypeResponse? speakerAwardTypeResponse in speakerAwardTypes.Select(sat => sat.ToResponse()).ToList())
			if (speakerAwardTypeResponse is not null)
				response.Add(speakerAwardTypeResponse);
		return response;
	}

}