namespace TaleLearnCode.SpeakerToolkit.Extensions;

internal static class PresentationExtensions
{

	internal static string PresentationTitle(this Presentation presentation, string? languageCode = null)
		=> presentation.PresentationTexts.FirstOrDefault(x => x.LanguageCode.Equals(languageCode?.ToUpperInvariant(), StringComparison.OrdinalIgnoreCase))?.PresentationTitle
		?? presentation.PresentationTexts.FirstOrDefault(x => x.LanguageCode.Equals(presentation.DefaultLanguageCode, StringComparison.InvariantCultureIgnoreCase))?.PresentationTitle
		?? presentation.PresentationTexts.FirstOrDefault()?.PresentationTitle ?? string.Empty;

	internal static PresentationTagsResponse ToPresentationTagsResponse(this Presentation presentation, string? languageCode = null)
		=> new()
		{
			PresentationId = presentation.PresentationId,
			PresentationTitle = presentation.PresentationTitle(languageCode),
			PresentationTags = presentation.PresentationTags.Select(x => x.Tag.TagName)
		};

	internal static PresentationListItemResponse ToPresentationListItemResponse(this Presentation presentation, string? languageCode = null)
		=> new()
		{
			Id = presentation.PresentationId,
			Title = presentation.PresentationTitle(languageCode)
		};

	internal static RelatedPresentationsResponse ToRelatedPresentationsResponse(this Presentation presentation, string? languageCode = null)
		=> new()
		{
			PresentationId = presentation.PresentationId,
			PresentationTitle = presentation.PresentationTitle(languageCode),
			RelatedPresentations = presentation.RelatedPresentations.OrderBy(x => x.SortOrder).Select(x => x.RelatedPresentation.ToPresentationListItemResponse(languageCode)).ToList()
		};

	internal static PresentationTextResponse ToPresentationTextResponse(this PresentationText presentationText)
		=> new()
		{
			PresentationId = presentationText.PresentationId,
			LanguageCode = presentationText.LanguageCode,
			Title = presentationText.PresentationTitle,
			ShortTitle = presentationText.PresentationShortTitle,
			Abstract = presentationText.Abstract,
			ShortAbstract = presentationText.ShortAbstract,
			Summary = presentationText.Summary,
			AdditionalDetails = presentationText.AdditionalDetails,
			LearningObjectives = presentationText.LearningObjectives.Select(x => x.ToResponse()).ToList()
		};

	internal static List<PresentationTextResponse> ToPresentationTextResponseList(this ICollection<PresentationText> presentationTexts)
		=> presentationTexts.Select(x => x.ToPresentationTextResponse()).ToList();

	internal static PresentationSpeakersResponse ToSpeakerList(this Presentation presentation)
		=> new()
		{
			PresentationId = presentation.PresentationId,
			PresentationTitle = presentation.PresentationTitle(),
			PrimarySpeaker = presentation.PresentationSpeakers.FirstOrDefault(x => x.IsPrimary == true)?.ToResponse() ?? new PresentationSpeakerResponse(),
			SecondarySpeakers = presentation.PresentationSpeakers.Where(x => x.IsPrimary == false).Select(x => x.ToResponse()).ToList()
		};


}