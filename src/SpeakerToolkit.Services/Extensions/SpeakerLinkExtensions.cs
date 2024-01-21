namespace TaleLearnCode.SpeakerToolkit.Extensions;

internal static class SpeakerLinkExtensions
{

	internal static SpeakerLinkResponse? ToResponse(this SpeakerLink? speakerLink) =>
		speakerLink is null
			? null
			: new SpeakerLinkResponse
			{
				Id = speakerLink.SpeakerLinkId,
				SpeakerId = speakerLink.SpeakerId,
				SpeakerName = speakerLink.Speaker?.FullName ?? string.Empty,
				LinkType = speakerLink.LinkType,
				LinkUrl = speakerLink.LinkUrl
			};

	internal static IEnumerable<SpeakerLinkResponse> ToResponse(this IEnumerable<SpeakerLink>? speakerLinks)
	{
		IEnumerable<SpeakerLinkResponse?> rawResponse = speakerLinks?.Select(speakerLink => speakerLink.ToResponse()) ?? [];
		return rawResponse.Where(response => response is not null).Select(response => response!);
	}

	internal static bool HasChanges(this SpeakerLink speakerLink, SpeakerLinkRequest request) =>
		speakerLink.LinkType != request.LinkType ||
		speakerLink.LinkUrl != request.LinkUrl;

}