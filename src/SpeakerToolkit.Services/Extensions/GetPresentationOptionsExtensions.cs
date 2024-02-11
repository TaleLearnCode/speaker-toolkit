namespace TaleLearnCode.SpeakerToolkit.Extensions;

internal static class GetPresentationOptionsExtensions
{
	internal static GetPresentationOptions Clone(this GetPresentationOptions options)
		=> new()
		{
			IncludePresentationTexts = options.IncludePresentationTexts,
			IncludeRelatedPresentations = options.IncludeRelatedPresentations,
			IncludeSpeakers = options.IncludeSpeakers,
			IncludeTags = options.IncludeTags,
			PresentationId = options.PresentationId
		};
}