using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Helpers
{
    public static class DateTimeOffsetExtensions
    {
        public static int GetCurrentAge(this DateTimeOffset dateTimeOffset)
        {
            var currentTime = DateTime.UtcNow;
            int age = currentTime.Year - dateTimeOffset.Year;

            if (currentTime > dateTimeOffset.AddYears(age))
            {
                age--;
            }
            return age;
        }
    }
}
