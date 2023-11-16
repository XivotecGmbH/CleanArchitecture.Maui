namespace Xivotec.CleanArchitecture.Application.Services.PersistenceConfiguration;

/// <summary>
/// Service for retrieving persistence connection configuration from appsettings.json.
/// </summary>
public interface IPersistenceConfigurationService
{
    /// <summary>
    /// Creates a new <see cref="PersistenceConfigurationDto"/> with the acquired connection configuration. 
    /// </summary>
    /// <returns>The created <see cref="PersistenceConfigurationDto"/></returns>
    PersistenceConfigurationDto GetPersistenceConfigurationDto();

    /// <summary>
    /// Returns the SecureConnectionString if set, otherwise the DefaultConnectionString
    /// </summary>
    string GetPersistenceConfigurationString();
}