//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace TupyLogger.Providers
//{
//    public class EventViewerProvider : IProvider
//    {
//        public ProviderTypes Type { get => ProviderTypes.TextFile; }
//        public string ID { get; set; }
//        public string FolderPath { get; set; }

//        public void Blau()
//        {
//            using (EventLog eventLog = new EventLog("Application"))
//            {
//                eventLog.Source = "Application";
//                eventLog.WriteEntry("Log message example", EventLogEntryType.Information, 101, 1);
//            }
//        }

//        public bool WriteEntry(EventEntry entry)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
