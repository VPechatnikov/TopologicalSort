using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopologicalSort
{
    public class TopologicalPartitioner : ITopologicalPartitioner
    {
        public IList<ICollection<Node>> DestructivePartition(ICollection<Node> nodes)
        {
            var partitions = new List<ICollection<Node>>();
            for (var roots = FindRoots(nodes); roots.Count > 0; roots = GetNewRoots(roots))
                partitions.Add(roots);
            return partitions;
        }

        private ICollection<Node> GetNewRoots(IEnumerable<Node> roots)
        {
            var nextRoots = new List<Node>();
            foreach (var root in roots)
                RemoveRootAndAddNewRoots(root, nextRoots);
            return nextRoots;
        }

        private void RemoveRootAndAddNewRoots(Node root, List<Node> nextRoots)
        {
            foreach (var successor in new List<Node>(root.Successors))
                RemoveCurrentRootAndPossiblyAddNewRoot(root, successor, nextRoots);
        }

        private void RemoveCurrentRootAndPossiblyAddNewRoot(Node root, Node successor, List<Node> nextRoots)
        {
            root.RemoveSuccesor(successor);
            if (IsRoot(successor))
                nextRoots.Add(successor);
        }

        private ICollection<Node> FindRoots(ICollection<Node> nodes)
        {
            return nodes.Where(IsRoot).ToArray();
        }

        private static bool IsRoot(Node node)
        {
            return node.Predecessors.Count == 0;
        }
    }
}
