using System;
using Tupy;

namespace Tupy.Jobs
{
    public class Job
    {
        public string Name { get; set; }
        public Schedule Schedule { get; set; }
        public Action StepAction { get; set; }
        public Action<ExecutionResponse> ReportStatus { get; set; }

        public Job()
        {
            Schedule = new Schedule()
            {
                FrequencyOption = FrequencyOptions.Hour,
                FrequencyInterval = 1
            };
        }

        public Job(FrequencyOptions frequencyOption, int frequencyInterval) : this()
        {
            Schedule.FrequencyOption = frequencyOption;
            Schedule.FrequencyInterval = frequencyInterval;
        }

        public ExecutionResponse Execute()
        {
            var result = new ExecutionResponse(true);

            try
            {
                StepAction();
                Schedule.Update();
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = "An error has occured.";
                result.Content = ex.Message;
            }

            ReportStatus?.Invoke(result);

            return result;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}