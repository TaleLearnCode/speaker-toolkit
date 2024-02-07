namespace TaleLearnCode.SpeakerToolkit.Extensions;

internal static class PresentationSpeakerExtensions
{

	internal static PresentationSpeakerResponse ToResponse(this PresentationSpeaker speaker)
		=> new()
		{
			PresentationId = speaker.PresentationId,
			PresentationTitle = speaker.Presentation.PresentationTitle(),
			SpeakerId = speaker.SpeakerId,
			SpeakerName = speaker.Speaker.SpeakerName()
		};

}