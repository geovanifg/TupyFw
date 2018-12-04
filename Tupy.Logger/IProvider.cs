using System;
using System.Threading.Tasks;

namespace Tupy.Logger
{
    public interface IProvider
    {
        ProviderTypes Type { get; }
        string ID { get; set; }
        //ExecutionResponse WriteEntry(EventEntry entry);
        Task<ExecutionResponse> WriteEntryAsync(EventEntry entry);
        //ExecutionResponse RemoveBefore(string sourceName, DateTime date);
        Task<ExecutionResponse> RemoveBeforeAsync(string sourceName, DateTime date);
    }
}
