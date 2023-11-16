namespace Xivotec.CleanArchitecture.Application.Services.PersistenceConfiguration;

/// <summary>
/// DTO for retrieving persistence connection configuration from appsettings.json.
/// </summary>
public sealed class PersistenceConfigurationDto
{
    public string? Host { get; set; }
    public string? Port { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? PersistenceName { get; set; }
}