using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
using System.Text.Json.Serialization;
using TaleLearnCode.SpeakerToolkit;

string environment = Environment.GetEnvironmentVariable("AZURE_FUNCTIONS_ENVIRONMENT")!;
string appConfigEndpoint = Environment.GetEnvironmentVariable("AppConfigEndpoint")!;
ConfigServices configServices = new(appConfigEndpoint, environment);

JsonSerializerOptions jsonSerializerOptions = new()
{
	PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
	DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
	DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
	WriteIndented = true
};

var host = new HostBuilder()
		.ConfigureFunctionsWebApplication()
		.ConfigureServices(services =>
		{
			services.AddApplicationInsightsTelemetryWorkerService();
			services.ConfigureFunctionsApplicationInsights();
			services.AddSingleton(configServices);
			services.AddSingleton(jsonSerializerOptions);
		})
		.Build();

host.Run();
