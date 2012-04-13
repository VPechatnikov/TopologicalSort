using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopologicalSort
{
    public interface ILevelSortableTask
    {
        int TaskID { get; set; }

        int ProcessingLevel { get; set; }

        IEnumerable<ILevelSortableTask> Predecessors { get; set; }
    }
}
