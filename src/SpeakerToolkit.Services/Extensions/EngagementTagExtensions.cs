namespace TaleLearnCode.SpeakerToolkit.Extensions;

internal static class EngagementTagExtensions
{

	internal static TagsResponse ToTagsResponse(this IEnumerable<EngagementTag>? EngagementTags)
		=> EngagementTags switch
		{
			null => new(),
			_ => EngagementTags.Select(EngagementTag => EngagementTag.Tag.TagName).ToTagsResponse()
		};

}