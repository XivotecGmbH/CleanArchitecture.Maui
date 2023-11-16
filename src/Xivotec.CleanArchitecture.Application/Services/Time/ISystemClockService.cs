namespace Xivotec.CleanArchitecture.Application.Services.Time;

public interface ISystemClockService
{
    public DateTimeOffset GetCurrentDate();
}