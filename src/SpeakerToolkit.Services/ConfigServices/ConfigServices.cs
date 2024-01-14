using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;

namespace TaleLearnCode.SpeakerToolkit;

public partial class ConfigServices
{

	private readonly IConfigurationRoot _config;

	public ConfigServices(string appConfigEndpoint, string environment)
	{
		ConfigurationBuilder builder = new();
		DefaultAzureCredential defaultAzureCredential = new();
		builder.AddAzureAppConfiguration(options =>
		{
			options.Connect(new Uri(appConfigEndpoint), defaultAzureCredential)
				.Select(KeyFilter.Any, LabelFilter.Null)
				.Select(KeyFilter.Any, environment)
				.ConfigureRefresh(refresh =>
				{
					refresh.Register("SpeakerToolkit:Settings:Sentinel", refreshAll: true)
						.SetCacheExpiration(TimeSpan.FromSeconds(5));
				})
				.ConfigureKeyVault(kv =>
				{
					kv.SetCredential(defaultAzureCredential);
				});
		});
		_config = builder.Build();
	}

	private string GetConfigValue(string key) => _config[key]!;

}