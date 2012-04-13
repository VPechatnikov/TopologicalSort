using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopologicalSort
{
    public interface ITopologicalSorter
    {
        void AssignLevelsIfNoCycleExists(ICollection<ILevelSortableTask> tasks);
    }
}
