namespace TaleLearnCode.SpeakerToolkit.Models;

public partial class Speaker
{
	public string FullName => $"{FirstName} {LastName}";
}