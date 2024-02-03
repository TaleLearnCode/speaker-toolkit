namespace TaleLearnCode.SpeakerToolkit.Extensions;

internal static class PresentationTagExtensions
{

	internal static TagsResponse ToTagsResponse(this IEnumerable<string>? tags) =>
		new() { Tags = tags?.ToList() ?? [] };

	internal static TagsResponse ToTagsResponse(this IEnumerable<PresentationTag>? presentationTags)
		=> presentationTags switch
		{
			null => new(),
			_ => presentationTags.Select(presentationTag => presentationTag.Tag.TagName).ToTagsResponse()
		};

}