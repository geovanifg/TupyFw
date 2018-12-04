using System;
using System.Collections.Generic;
using Tupy.Jobs;

namespace Tupy.Logger
{
    public static class LoggerOrchestrator
    {
        private static JobManager jobManager = new JobManager();
        private static EventSourceManager EventSourceManager { get; set; } = new EventSourceManager();
        public static ProviderManager ProviderManager { get; set; } = new ProviderManager();
        public static List<ExecutionResponse> ExecutionErrors { get; set; } = new List<ExecutionResponse>();

        //public Action<ExecutionResponse> ReportStatus { get; set; }

        public static void AddEventSource(EventSource e)
        {
            if (EventSourceManager.Add(e))
            {
                if (e.MinimumRetention > 0)
                    AddJob(e);
            }
        }

        private static void AddJob(EventSource e)
        {
            var job = new Job()
            {
                Name = e.Name + " clean job"
            };

            job.StepAction = async delegate ()
            {
                var expiration = DateTime.Now;

                var responses = await ProviderManager.RemoveBeforeAsync(e.Name, expiration);

                if (responses.Count > 0)
                {
                    ExecutionErrors.AddRange(responses);
                }
            };
            job.Schedule.FrequencyOption = e.RetentionPeriodoType;
            job.Schedule.FrequencyInterval = e.MinimumRetention;

            jobManager.Jobs.Add(job);
        }

        public static void Start()
        {
            ExecutionErrors.Clear();
            jobManager.Start();
        }

        public static void Stop()
        {
            jobManager.Stop();
        }
    }
}