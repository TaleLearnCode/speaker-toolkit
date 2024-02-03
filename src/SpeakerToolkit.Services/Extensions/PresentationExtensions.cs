namespace TaleLearnCode.SpeakerToolkit.Extensions;

internal static class PresentationExtensions
{

	internal static string PresentationTitle(this Presentation presentation, string? languageCode = null)
	{
		if (languageCode is not null)
		{
			PresentationText? presentationText = presentation.PresentationTexts.FirstOrDefault(x => x.LanguageCode == languageCode);
			if (presentationText is not null)
				return presentationText.PresentationTitle;
		}
		PresentationText? defaultPresentationText = presentation.PresentationTexts.FirstOrDefault(x => x.LanguageCode == presentation.DefaultLanguageCode);
		if (defaultPresentationText is not null)
			return defaultPresentationText.PresentationTitle;
		return presentation.PresentationTexts.FirstOrDefault()?.PresentationTitle ?? string.Empty;
	}

	internal static PresentationTagsResponse ToPresentationTagsResponse(this Presentation presentation)
		=> new()
		{
			PresentationId = presentation.PresentationId,
			PresentationTitle = presentation.PresentationTitle(),
			PresentationTags = presentation.PresentationTags.Select(x => x.Tag.TagName)
		};

}