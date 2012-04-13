using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopologicalSort
{
    public class TopologicalSorter: ITopologicalSorter
    {
        readonly ITopologicalPartitioner _partitioner;

        public TopologicalSorter(ITopologicalPartitioner partitioner)
        {
            _partitioner = partitioner;
        }

        public void AssignLevelsIfNoCycleExists(ICollection<ILevelSortableTask> tasks)
        {
            var partitionedTasks = _partitioner.DestructivePartition(CreateNodes(tasks));
            var flattenedPartition = partitionedTasks.SelectMany(nodes => 
                                                                 nodes.Select(node => node.Task.TaskID))
                                                     .ToList();

            var numUnreachableTasks = tasks.Count - flattenedPartition.Count;
            if (numUnreachableTasks == 0)
                AssignLevels(partitionedTasks);
            else
            {
                var cycleTaskIDs = tasks.Where(t => !flattenedPartition.Contains(t.TaskID))
                                        .Select(t => t.TaskID.ToString())
                                        .ToArray();
                var err = "Cycle detected in task graph.  There are " + numUnreachableTasks +
                    " tasks that are unreachable due to at least one cycle. The task IDs are: " +
                    String.Join(",", cycleTaskIDs);

                throw new InvalidOperationException(err);
            }
        }

        private void AssignLevels(IList<ICollection<Node>> partitionedTasks)
        {
            var processingLevel = 0;
            foreach (var partition in partitionedTasks)
                AssignLevel(partition, ++processingLevel);
        }

        private void AssignLevel(ICollection<Node> partition, int processingLevel)
        {
            foreach (var node in partition)
                node.Task.ProcessingLevel = processingLevel;
        }

        private ICollection<Node> CreateNodes(ICollection<ILevelSortableTask> tasks)
        {
            var taskToNode = tasks.ToDictionary(t => t, t => new Node(t));
            AddEdges(taskToNode);
            return taskToNode.Values;
        }

        private void AddEdges(IDictionary<ILevelSortableTask, Node> taskToNode)
        {
            foreach (var taskNodePair in taskToNode)
                foreach (var predecessor in taskNodePair.Key.Predecessors)
                    taskNodePair.Value.AddPredecessor(taskToNode[predecessor]);
        }
    }
}
