namespace TaleLearnCode.SpeakerToolkit.Responses;

public class UpsertResponse<T>(T item, UpsertAction action)
{
	public T Item { get; set; } = item;
	public UpsertAction Action { get; set; } = action;
}