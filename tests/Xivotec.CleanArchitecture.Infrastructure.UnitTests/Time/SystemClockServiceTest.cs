using FluentAssertions;
using Xivotec.CleanArchitecture.Infrastructure.Services.SystemClock;
using Xunit;

namespace Xivotec.CleanArchitecture.Infrastructure.UnitTests.Time;

public class SystemClockServiceTest
{
    private readonly SystemClockService _sut = new();

    [Fact]
    public void GetCurrentDate_ShouldReturnValidString_WhenFormattedForDate()
    {
        //Arrange
        //Act
        var date = _sut.GetCurrentDate().ToString("dd MM yyyy");

        //Assert
        date.Should().NotBeEmpty();
        date.Should()
            .MatchRegex(
                @"\d{2} \d{2} \d{4}");
    }

    [Fact]
    public void GetCurrentDate_ShouldReturnValidString_WhenFormattedForTime()
    {
        //Arrange
        //Act
        var date = _sut.GetCurrentDate().ToString("h:mm:ss");

        //Assert
        date.Should().NotBeEmpty();
        date.Should()
            .MatchRegex(
                "^(0?[1-9]|1[0-2]):[0-5][0-9]:[0-5][0-9]$");
    }
}