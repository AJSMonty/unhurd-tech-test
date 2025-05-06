namespace Unhurd.Api.Configurations;

public class CosmosDbConfig
{
    public string Account { get; set; } = string.Empty;
    public string Key { get; set; } = string.Empty;
    public string DatabaseName { get; set; } = string.Empty;
    public Dictionary<string, CosmosContainerConfig> Containers { get; set; } = new();
}
