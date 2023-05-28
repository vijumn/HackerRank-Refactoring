using System;

namespace Refactoring.LegacyService.Services
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}