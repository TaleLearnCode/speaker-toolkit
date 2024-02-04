namespace TaleLearnCode.SpeakerToolkit.Exceptions;

public class BusinessRuleException : Exception
{
	private const string _message = "A business rule was violated.";
	public BusinessRuleException() : base(_message) { }
	public BusinessRuleException(string message) : base(message) { }
	public BusinessRuleException(string message, Exception innerException) : base(message, innerException) { }
}