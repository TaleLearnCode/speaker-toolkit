namespace TaleLearnCode.SpeakerToolkit.Extensions;

internal static class EngagementStatusExtensions
{

	internal static EngagementStatusResponse ToResponse(this EngagementStatus engagementStatus)
		=> new()
		{
			Id = engagementStatus.EngagementStatusId,
			Name = engagementStatus.EngagementStatusName
		};

	internal static List<EngagementStatusResponse> ToResponse(this ICollection<EngagementStatus> engagementStatuses)
		=> engagementStatuses.Select(engagementStatus => engagementStatus.ToResponse()).ToList();

}