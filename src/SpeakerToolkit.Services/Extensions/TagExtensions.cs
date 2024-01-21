namespace TaleLearnCode.SpeakerToolkit.Extensions;

internal static class TagExtensions
{

	internal static TagResponse? ToResponse(this Tag? tag)
		=> tag is null ? null : new TagResponse
		{
			Id = tag.TagId,
			Name = tag.TagName
		};

	internal static List<TagResponse> ToResponse(this IEnumerable<Tag> tags)
	{
		IEnumerable<TagResponse?> rawResponse = tags?.Select(tag => tag.ToResponse()) ?? [];
		return rawResponse.Where(response => response is not null).Select(response => response!).ToList();
	}

}