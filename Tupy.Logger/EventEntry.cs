using System;
using Tupy.Extensions;

namespace Tupy.Logger
{
    public class EventEntry
    {
        public long EventEntryId { get; private set; }
        public DateTimeOffset TimeGenerated { get; private set; }
        public string Source { get; set; }
        public EventEntryTypes EntryType { get; set; }
        public string Category { get; set; }
        public string Message { get; set; }
        public string Data { get; set; }
        public string MachineName { get; set; }
        public string UserName { get; set; }

        public EventEntry()
        {
            var now = DateTime.Now;

            EventEntryId = now.ToLong();
            TimeGenerated = now;
        }
    }
}