namespace TaleLearnCode.SpeakerToolkit.Extensions;

internal static class PresentationTypeExtensions
{

	internal static PresentationTypeResponse? ToResponse(this PresentationType? presentationType)
		=> presentationType is null ? null : new PresentationTypeResponse
		{
			Id = presentationType.PresentationTypeId,
			Name = presentationType.PresentationTypeName,
			Description = presentationType.TypeDescription,
			SortOrder = presentationType.SortOrder
		};

	internal static List<PresentationTypeResponse> ToResponse(this IEnumerable<PresentationType> presentationTypes)
	{
		IEnumerable<PresentationTypeResponse?> rawResponse = presentationTypes?.Select(presentationType => presentationType.ToResponse()) ?? [];
		return rawResponse.Where(response => response is not null).Select(response => response!).ToList();
	}


}