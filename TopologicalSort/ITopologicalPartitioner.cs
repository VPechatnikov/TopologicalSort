using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopologicalSort
{
    public interface ITopologicalPartitioner
    {
        IList<ICollection<Node>> DestructivePartition(ICollection<Node> nodes);
    }
}
