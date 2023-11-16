using FluentAssertions;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using Xivotec.CleanArchitecture.Application.Exceptions;
using Xivotec.CleanArchitecture.Application.Services.PersistenceConfiguration;
using Xivotec.CleanArchitecture.Infrastructure.Services.PersistenceConfiguration;
using Xunit;

namespace Xivotec.CleanArchitecture.Infrastructure.UnitTests.Persistence.Configuration;

public class PersistenceConfigurationServiceTest
{

    private IPersistenceConfigurationService? _sut;
    private readonly IConfiguration _configuration = Substitute.For<IConfiguration>();

    private readonly string _defaultConnectionString = "Host=host;Port=5431;Username=user;Password=pwd;Database=db;";
    private readonly string _falseDefaultConnectionString = "Host=localhost;Username=postgres;Password=postgres;Database=todolists;";

    private readonly PersistenceConfigurationDto _defaultConfigurationDto = new()
    {
        Host = "host",
        Port = "5431",
        Username = "user",
        Password = "pwd",
        PersistenceName = "db"
    };

    public PersistenceConfigurationServiceTest()
    {
        _configuration.GetConnectionString("DefaultConnection").Returns("Host=host;Port=5431;Username=user;Password=pwd;Database=db;");
    }


    [Fact]
    public void Service_ShouldThrowInvalidConfigurationException_WhenConnectionStringContainsTooFewParameter()
    {
        //Arrange
        _configuration.GetConnectionString("DefaultConnection").Returns(_falseDefaultConnectionString);
        _sut = new PersistenceConfigurationService(_configuration);

        //Act
        var requestedAction1 = () => _sut.GetPersistenceConfigurationDto();
        var requestedAction2 = () => _sut.GetPersistenceConfigurationString();

        //Assert
        requestedAction1.Should()
            .Throw<InvalidConfigurationException>();
        requestedAction2.Should()
            .Throw<InvalidConfigurationException>();
    }


    [Fact]
    public void Service_ShouldThrowInvalidConfigurationException_WhenConnectionStringIsNullOrEmpty()
    {
        //Arrange
        _configuration.GetConnectionString("DefaultConnection").Returns("");
        _sut = new PersistenceConfigurationService(_configuration);

        //Act
        var requestedAction1 = () => _sut.GetPersistenceConfigurationDto();
        var requestedAction2 = () => _sut.GetPersistenceConfigurationString();

        //Assert
        requestedAction1.Should()
            .Throw<InvalidConfigurationException>();
        requestedAction2.Should()
            .Throw<InvalidConfigurationException>();
    }


    [Fact]
    public void Configuration_ShouldReturnDefaultConnectionDto_IfSecureIsNotSet()
    {
        //Arrange
        _configuration.GetConnectionString("DefaultConnection").Returns(_defaultConnectionString);
        _sut = new PersistenceConfigurationService(_configuration);

        //Act
        var config1 = _sut.GetPersistenceConfigurationDto();
        var config2 = _sut.GetPersistenceConfigurationString();

        //Assert
        config1.Should().BeEquivalentTo(_defaultConfigurationDto);
        config2.Should().Be(_defaultConnectionString);
    }
}
