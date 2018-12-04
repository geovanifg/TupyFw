using Tupy.Jobs;

namespace Tupy.Logger
{
    public class EventSource
    {
        public string Name { get; set; }
        public int MinimumRetention { get; set; }
        public FrequencyOptions RetentionPeriodoType { get; set; }
    }
}
