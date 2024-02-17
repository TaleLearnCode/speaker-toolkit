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

	internal static EngagementResponse ToResponse(this Engagement Engagement, string? languageCode = null)
		=> new()
		{
			Id = Engagement.EngagementId,
			EngagementTypeId = Engagement.EngagementTypeId,
			EngagementType = Engagement.EngagementType?.EngagementTypeName,
			EngagementStatusId = Engagement.EngagementStatusId,
			EngagementStatus = Engagement.EngagementStatus?.EngagementStatusName,
			Name = Engagement.EngagementName,
			OverviewLocation = Engagement.OverviewLocation,
			ListingLocation = Engagement.ListingLocation,
			StartDate = Engagement.StartDate,
			EndDate = Engagement.EndDate,
			StartingCost = Engagement.StartingCost,
			EndingCost = Engagement.EndingCost,
			Description = Engagement.EngagementDescription,
			Summary = Engagement.EngagementSummary,
			Url = Engagement.EngagementUrl,
			Permalink = Engagement.Permalink,
			Presentations = Engagement.EngagementPresentations?.Select(x => x.ToResponse(languageCode)).ToList(),
			Tags = Engagement.EngagementTags.Select(x => x.Tag.TagName).ToList()
		};

	internal static List<EngagementResponse> ToResponse(this IEnumerable<Engagement> engagements, string? languageCode = null)
		=> engagements.Select(engagement => engagement.ToResponse(languageCode)).ToList();

}