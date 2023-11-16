using Xivotec.CleanArchitecture.Application.Services.Time;

namespace Xivotec.CleanArchitecture.Infrastructure.Services.SystemClock;

public class SystemClockService : ISystemClockService
{
    public DateTimeOffset GetCurrentDate()
    {
        return DateTimeOffset.Now;
    }
}