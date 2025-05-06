namespace Unhurd.Api.Configurations;

public class CosmosContainerConfig
{
    public string Name { get; set; } = string.Empty;
    public string PartitionKey { get; set; } = string.Empty;
}
