namespace TaleLearnCode.SpeakerToolkit.Extensions;

internal static class PresentationTextExtensions
{
	internal static LearningObjectivesResponse ToLearningObjectivesResponse(
		this PresentationText presentationText,
		bool includeHeader = false)
		=> new()
		{
			PresentationId = (includeHeader) ? presentationText.PresentationId : null,
			PresentationTitle = (includeHeader) ? presentationText.PresentationTitle : null,
			LanguageCode = (includeHeader) ? presentationText.LanguageCode : null,
			LearningObjectives = presentationText.LearningObjectives.Select((x, i) => x.ToResponse()).ToList()
		};
}