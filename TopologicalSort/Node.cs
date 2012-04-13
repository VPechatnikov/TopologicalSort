using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopologicalSort
{
    public class Node
    {
        private ILevelSortableTask _task;
        private ICollection<Node> _predecessors = new List<Node>();
        private ICollection<Node> _successors = new List<Node>();

        public Node(ILevelSortableTask task)
        {
            _task = task;
        }

        public ILevelSortableTask Task { get; set; }
        public ICollection<Node> Predecessors { get { return _predecessors; } }
        public ICollection<Node> Successors { get { return _successors; } }

        public void AddPredecessor(Node node)
        {
            _predecessors.Add(node);
            node.Successors.Add(this);
        }

        public void RemoveSuccesor(Node successor)
        {
            _successors.Remove(successor);
            successor.Predecessors.Remove(this);
        }
    }
}
