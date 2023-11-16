using Microsoft.Extensions.Configuration;
using Xivotec.CleanArchitecture.Application.Exceptions;
using Xivotec.CleanArchitecture.Application.Services.PersistenceConfiguration;

namespace Xivotec.CleanArchitecture.Infrastructure.Services.PersistenceConfiguration;

/// <inheritdoc cref="IPersistenceConfigurationService"/>
public sealed class PersistenceConfigurationService : IPersistenceConfigurationService
{
    private readonly IConfiguration _configuration;

    // DB configuration string should have 5 parameter + 1 terminator
    private const int PersistenceConfigurationDtoValuesCount = 6;

    /// <inheritdoc cref="IPersistenceConfigurationService"/>
    public PersistenceConfigurationService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <inheritdoc cref="IPersistenceConfigurationService.GetPersistenceConfigurationDto()"/>
    public PersistenceConfigurationDto GetPersistenceConfigurationDto()
    {
        var configuration = GetDefaultPersistenceConfigurationDto();

        return configuration;
    }

    /// <inheritdoc cref="IPersistenceConfigurationService.GetPersistenceConfigurationString()"/>
    public string GetPersistenceConfigurationString()
    {
        var configuration = GetDefaultPersistenceConfigurationString();

        return configuration;
    }

    private PersistenceConfigurationDto GetDefaultPersistenceConfigurationDto()
    {
        // get DefaultConnection string from config
        var configValue = GetDefaultPersistenceConfigurationString();

        // split DefaultConnection string in parts for dto
        var configParts = configValue.Split(';');
        if (configParts.Length < PersistenceConfigurationDtoValuesCount)
        {
            throw new InvalidConfigurationException("Default Connection");
        }

        // build configuration Dto from string parts
        var configuration = new PersistenceConfigurationDto()
        {
            Host = configParts[0].Split('=')[1],
            Port = configParts[1].Split('=')[1],
            Username = configParts[2].Split('=')[1],
            Password = configParts[3].Split('=')[1],
            PersistenceName = configParts[4].Split('=')[1]
        };

        return configuration;
    }

    private string GetDefaultPersistenceConfigurationString()
    {
        // get DefaultConnection string from config
        var configString = _configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrEmpty(configString))
        {
            throw new InvalidConfigurationException("Default Connection");
        }

        var configParts = configString.Split(';');
        if (configParts.Length < PersistenceConfigurationDtoValuesCount)
        {
            throw new InvalidConfigurationException("Default Connection");
        }

        return configString;
    }
}