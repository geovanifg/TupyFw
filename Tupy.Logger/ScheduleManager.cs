//using System;
//using System.Collections.Generic;
//using System.Threading;
//using System.Threading.Tasks;
//using Tupy;

//namespace TupyLogger
//{
//    public class ScheduleManager
//    {
//        private bool canexecute = false;
//        public bool Running { get; private set; }

//        public void Start()
//        {
//            canexecute = true;

//            Task.Run(async () =>
//            {
//                while (canexecute)
//                {
//                    Running = true;

//                    var awaitingminutes = 60;

//                    var execresult = await ExecuteAllAsync();

//                    if (canexecute)
//                    {
//                        if (execresult.IsSuccess == false)
//                            awaitingminutes = 10;

//                        Thread.Sleep(awaitingminutes * 60 * 1000);
//                    }
//                }

//                Running = false;
//            });
//        }

//        public void Stop()
//        {
//            canexecute = false;
//        }

//        private async Task<List<ExecutionResponse>> ExecuteForEventSourceAsync(EventSource eventsrc)
//        {
//            var result = new List<ExecutionResponse>();

//            var providererrors = new List<string>();

//            var expiration = DateTime.Now;

//            ExecutionResponse response = null;

//            switch (eventsrc.RetentionPeriodoType)
//            {
//                case TimePeriodTypes.Minute:
//                    expiration = DateTime.Now.AddMinutes(eventsrc.MinimumRetention * (-1));
//                    break;
//                case TimePeriodTypes.Hour:
//                    expiration = DateTime.Now.AddHours(eventsrc.MinimumRetention * (-1));
//                    break;
//                case TimePeriodTypes.Day:
//                    expiration = DateTime.Now.AddDays(eventsrc.MinimumRetention * (-1));
//                    break;
//            }

//            foreach (var provider in LoggerOrchestrator.ProviderManager.List())
//            {
//                try
//                {
//                    response = await provider.RemoveBefore(eventsrc.Name, expiration);
//                }
//                catch (Exception ex)
//                {
//                    response = new ExecutionResponse(false)
//                    {
//                        Message = $"Provider {provider.ID} ({provider.Type.ToString()}) RemoveBefor error.",
//                        Content = ex.Message
//                    };
//                }

//                // If execution error, then add in the list
//                if (response.IsSuccess == false)
//                {
//                    result.Add(response);
//                }
//            }

//            return result;
//        }

//        private async Task<List<ExecutionResponse>> ExecuteAllAsync()
//        {
//            var result = new List<ExecutionResponse>();

//            foreach (var item in LoggerOrchestrator.EventSourceManager.List())
//            {
//                if (item.MinimumRetention <= 0)
//                    continue;

//                var response = await ExecuteForEventSourceAsync(item);

//                if (response.Count > 0)
//                    result.AddRange(response);
//            }

//            return result;
//        }

//        public List<ExecutionResponse> ExecuteOnce()
//        {
//            List<ExecutionResponse> result = null;

//            Task.Run(async () =>
//            {
//                result = await ExecuteAllAsync();
//            });

//            return result;
//        }
//    }
//}
