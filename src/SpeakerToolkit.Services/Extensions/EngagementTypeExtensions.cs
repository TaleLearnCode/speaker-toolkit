namespace TaleLearnCode.SpeakerToolkit.Extensions;

internal static class EngagementTypeExtensions
{

	internal static EngagementTypeResponse ToResponse(this EngagementType engagementType)
	{
		return new EngagementTypeResponse
		{
			Id = engagementType.EngagementTypeId,
			Name = engagementType.EngagementTypeName
		};
	}

	internal static IEnumerable<EngagementTypeResponse> ToResponse(this IEnumerable<EngagementType> engagementTypes)
		=> engagementTypes.Select(engagementType => engagementType.ToResponse());

}