using System.Collections.Generic;

namespace Tupy.Data
{
    public class GroupedDisplayItem : DisplayItem
    {
        public bool IsGroup { get; set; }
        public IEnumerable<GroupedDisplayItem> Items { get; set; }

    }
}
