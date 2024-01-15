namespace TaleLearnCode.SpeakerToolkit;

public partial class ConfigServices
{

	private const string _azureSql_DataSource = "AzureSql:DataSource";
	private const string _azureSql_UserId = "AzureSql:UserId";
	private const string _azureSql_Password = "AzureSql:Password";
	private const string _azureSql_Catalog = "AzureSql:Catalog";

	private string AzureSqlDataSource => GetConfigValue(_azureSql_DataSource);
	private string AzureSqlUserId => GetConfigValue(_azureSql_UserId);
	private string AzureSqlPassword => GetConfigValue(_azureSql_Password);
	private string AzureSqlCatalog => GetConfigValue(_azureSql_Catalog);

	public string AzureSqlConnectionString
		=> $"Data Source={AzureSqlDataSource}; Initial Catalog={AzureSqlCatalog};User ID={AzureSqlUserId};Password={AzureSqlPassword};TrustServerCertificate=True;Connection Timeout=30";

}