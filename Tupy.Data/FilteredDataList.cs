using System.Collections.Generic;

namespace Tupy.Data
{
    public class FilteredDataList<T>
    {
        public long RecordsTotal { get; set; }
        public long RecordsFiltered { get; set; }
        public List<T> Items { get; set; }
    }
}
