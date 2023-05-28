using System;
using System.Collections.Generic;
using System.Text;

namespace Refactoring.LegacyService.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.Now;
    }
}
