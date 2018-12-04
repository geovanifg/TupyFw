using System;

namespace Tupy.Jobs
{
    public class Schedule
    {
        public FrequencyOptions FrequencyOption { get; set; } = FrequencyOptions.Minute;
        public int FrequencyInterval { get; set; }
        public DateTime? LastExecution { get; private set; }

        public void Update()
        {
            LastExecution = DateTime.Now;
        }
    }
}
