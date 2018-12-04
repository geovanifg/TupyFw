using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tupy.Logger
{
    public class ProviderManager
    {
        private List<IProvider> providers;

        public ProviderManager()
        {
            providers = new List<IProvider>();
        }

        public IEnumerable<IProvider> List()
        {
            return providers;
        }

        public bool Add(IProvider provider)
        {
            var result = false;

            if (providers.Where(r => r.ID == provider.ID).Count() == 0)
            {
                providers.Add(provider);
                result = true;
            }
            return result;
        }

        public bool Remove(string ID)
        {
            var result = false;

            var provider = providers.Where(r => r.ID == ID).FirstOrDefault();

            if (provider != null)
            {
                providers.Remove(provider);
                result = true;
            }
            return result;
        }

        private ExecutionResponse GenerateResponseFromException(IProvider provider, Exception ex)
        {
            var result = new ExecutionResponse(false)
            {        
                Source = $"Provider {provider.ID} ({provider.Type.ToString()})",
                Content = ex.Message
            };
            return result;
        }

        public List<ExecutionResponse> WriteEntry(EventEntry entry)
        {
            List<ExecutionResponse> result = null;
            //Task.Run(async () => { result = await WriteEntryAsync(entry); });

            var a = Task.Run(async () => { result = await WriteEntryAsync(entry); });
            a.Wait();

            return result;
        }

        public async Task<List<ExecutionResponse>> WriteEntryAsync(EventEntry entry)
        {
            var result = new List<ExecutionResponse>();

            ExecutionResponse response = null;

            foreach (var item in providers)
            {
                try
                {
                    response = await item.WriteEntryAsync(entry);
                }
                catch (Exception ex)
                {
                    response = GenerateResponseFromException(item, ex);
                    response.Message = $"An error has occurred during log entry writing.";
                }

                if (response.IsSuccess == false)
                {
                    result.Add(response);
                }
            }
            return result;
        }

        //public List<ExecutionResponse> RemoveBefore(string sourceName, DateTime expirationDate)
        //{
        //    List<ExecutionResponse> result = null;
        //    Task.Run(async () => { result = await RemoveBeforeAsync(sourceName, expirationDate); });
        //    return result;
        //}

        public async Task<List<ExecutionResponse>> RemoveBeforeAsync(string sourceName, DateTime expirationDate)
        {
            var result = new List<ExecutionResponse>();

            ExecutionResponse response = null;

            foreach (var item in providers)
            {
                try
                {
                    response = await item.RemoveBeforeAsync(sourceName, expirationDate);
                }
                catch (Exception ex)
                {
                    response = GenerateResponseFromException(item, ex);
                    response.Message = $"An error has occurred during remove entryes.";
                }

                if (response.IsSuccess == false)
                {
                    result.Add(response);
                }
            }
            return result;
        }
    }
}
