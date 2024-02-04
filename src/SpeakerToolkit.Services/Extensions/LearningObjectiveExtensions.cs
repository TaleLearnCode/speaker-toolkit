namespace TaleLearnCode.SpeakerToolkit.Extensions;

internal static class LearningObjectiveExtensions
{
	internal static LearningObjectiveResponse ToResponse(
		this LearningObjective learningObjective,
		PresentationText? presentationText = null)
		=> new()
		{
			PresentationId = presentationText?.PresentationId,
			PresentationTitle = presentationText?.PresentationTitle,
			LanguageCode = presentationText?.LanguageCode,
			LearningObjective = learningObjective.LearningObjectiveText,
			SortOrder = learningObjective.SortOrder
		};

	internal static LearningObjectivesResponse ToResponse(
		this ICollection<LearningObjective> learningObjectives,
		PresentationText? presentationText = null)
	{
		LearningObjectivesResponse response = new()
		{
			PresentationId = presentationText?.PresentationId,
			PresentationTitle = presentationText?.PresentationTitle,
			LanguageCode = presentationText?.LanguageCode,
			LearningObjectives = learningObjectives.Select((x, i) => x.ToResponse(presentationText)).ToList()
		};
		return response;
	}
}