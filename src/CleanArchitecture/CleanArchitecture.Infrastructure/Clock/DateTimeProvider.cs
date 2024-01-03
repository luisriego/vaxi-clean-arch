using CleanArchitecture.Application.Abstractions.Clock;

namespace CleanArchitecture.Infrastructure;

internal sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime CurrentTime => DateTime.UtcNow;
}