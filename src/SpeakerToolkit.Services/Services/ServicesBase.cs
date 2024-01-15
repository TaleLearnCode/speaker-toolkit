namespace TaleLearnCode.SpeakerToolkit.Services;

public abstract class ServicesBase
{

	protected readonly ConfigServices _configServices;

	protected ServicesBase(ConfigServices configServices) => _configServices = configServices;

}