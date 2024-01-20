namespace TaleLearnCode.SpeakerToolkit.Exceptions;

public class ObjectAlreadyExistsException : Exception
{
	private const string _message = "The object already exists.";
	public ObjectAlreadyExistsException() : base(_message) { }
	public ObjectAlreadyExistsException(string message) : base(message) { }
	public ObjectAlreadyExistsException(string message, Exception innerException) : base(message, innerException) { }
}