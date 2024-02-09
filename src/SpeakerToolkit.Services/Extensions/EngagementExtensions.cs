namespace TaleLearnCode.SpeakerToolkit.Extensions;

internal static class EngagementExtensions
{

	internal static EngagementTagsResponse ToEngagementTagsResponse(this Engagement Engagement)
	=> new()
	{
		EngagementId = Engagement.EngagementId,
		EngagementTitle = Engagement.EngagementName,
		EngagementTags = Engagement.EngagementTags.Select(x => x.Tag.TagName)
	};

}