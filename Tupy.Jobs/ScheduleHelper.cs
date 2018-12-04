using System;

namespace Tupy.Jobs
{
    public static class ScheduleHelper
    {
        public static bool CanExecute(this Schedule self)
        {
            var result = false;

            if (self.LastExecution == null)
                return true;

            var diff = DateTime.Now.Subtract((DateTime)self.LastExecution);

            switch (self.FrequencyOption)
            {
                case FrequencyOptions.Minute:
                    if (self.FrequencyInterval < diff.TotalMinutes)
                        result = true;
                    break;
                case FrequencyOptions.Hour:
                    if (self.FrequencyInterval < diff.TotalHours)
                        result = true;
                    break;
                case FrequencyOptions.Day:
                    if (self.FrequencyInterval < diff.TotalDays)
                        result = true;
                    break;
            }

            return result;
        }
    }
}
