using System.Collections.Generic;
using System.Linq;

namespace Tupy.Logger
{
    public class EventSourceManager
    {
        private List<EventSource> eventsourceslist;

        public EventSourceManager()
        {
            eventsourceslist = new List<EventSource>();
        }

        public IEnumerable<EventSource> List()
        {
            return eventsourceslist;
        }

        public bool Add(EventSource source)
        {
            var result = false;

            if (eventsourceslist.Where(r => r.Name == source.Name).Count() == 0)
            {
                eventsourceslist.Add(source);
                result = true;
            }
            return result;
        }

        public bool Remove(string name)
        {
            var result = false;

            var provider = eventsourceslist.Where(r => r.Name == name).FirstOrDefault();

            if (provider != null)
            {
                eventsourceslist.Remove(provider);
                result = true;
            }
            return result;
        }
    }
}