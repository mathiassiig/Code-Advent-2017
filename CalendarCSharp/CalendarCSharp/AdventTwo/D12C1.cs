using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarCSharp.AdventTwo
{
    public class D12C1
    {
        /*public class Cluster
        {
            public Dictionary<int, int> ClusterValues = new Dictionary<int, int>();
        }*/

        public class Line
        {
            public int Left;
            public List<int> Right;
        }

        //public List<Cluster> _clusters = new List<Cluster>();
        public int Output()
        {
            var allText = System.IO.File.ReadAllText(System.IO.Path.Combine(Environment.CurrentDirectory, "d12_input.txt"));
            var allLines = allText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            List<Line> allLinesConverted = new List<Line>();
            foreach (var line in allLines)
            {
                var segs = line.Split(new string[] { " <-> " }, StringSplitOptions.None);
                var from = Convert.ToInt32(segs[0]);
                var toS = segs[1].Split(',').Select(x => Convert.ToInt32(x)).ToList();
                allLinesConverted.Add(new Line() { Left = from, Right = toS });
            }
            Queue<int> _targets = new Queue<int>();
            HashSet<int> values = new HashSet<int>();
            values.Add(0);
            _targets.Enqueue(0);
            while(_targets.Count > 0)
            {
                var target = _targets.Dequeue();
                var targetLine = allLinesConverted.FirstOrDefault(x => x.Left == target);
                foreach(var potentialTarget in targetLine.Right)
                {
                    if(!values.Contains(potentialTarget))
                    {
                        _targets.Enqueue(potentialTarget);
                        values.Add(potentialTarget);
                    }
                }
            }
            return values.Count;
            //return SolutionOne(allLines);
        }
        /*
        int SolutionOne(string[] allLines)
        {
            foreach (var line in allLines)
            {
                var segs = line.Split(new string[] { " <-> " }, StringSplitOptions.None);
                var from = Convert.ToInt32(segs[0]);
                var toS = segs[1].Split(',').Select(x => Convert.ToInt32(x)).ToList();
                var all = new List<int>(toS);
                all.Add(from);
                HashSet<Cluster> existingClusters = new HashSet<Cluster>();
                foreach (var cluster in _clusters)
                {
                    foreach (var program in all)
                    {
                        if (cluster.ClusterValues.ContainsKey(program))
                        {
                            existingClusters.Add(cluster);
                        }
                    }
                }
                if (existingClusters.Count == 1)
                {
                    foreach (var program in all)
                    {
                        if (!existingClusters.FirstOrDefault().ClusterValues.ContainsKey(program))
                        {
                            existingClusters.FirstOrDefault().ClusterValues.Add(program, program);
                        }
                    }
                }
                else if (existingClusters.Count > 1)
                {
                    var clustersToMerge = existingClusters.ToList();
                    Cluster clustersMerged = new Cluster();
                    foreach (var cluster in clustersToMerge)
                    {
                        foreach (var program in cluster.ClusterValues)
                        {
                            if (!clustersMerged.ClusterValues.ContainsKey(program.Key))
                            {
                                clustersMerged.ClusterValues.Add(program.Key, program.Value);
                            }
                        }
                        _clusters.Remove(cluster);
                    }
                    _clusters.Add(clustersMerged);
                }
                else
                {
                    var newCluster = new Cluster();
                    foreach (var program in all)
                    {
                        if (!newCluster.ClusterValues.ContainsKey(program))
                        {
                            newCluster.ClusterValues.Add(program, program);
                        }
                    }
                    _clusters.Add(newCluster);
                }

            }
            var zeroClusters = _clusters.Where(x => x.ClusterValues.ContainsKey(0)).ToList();
            Dictionary<int, int> allValues = new Dictionary<int, int>();
            foreach (var cluster in _clusters)
            {
                foreach (var program in cluster.ClusterValues)
                {
                    if (!allValues.ContainsKey(program.Key))
                    {
                        allValues.Add(program.Key, program.Value);
                    }
                    else
                    {
                        Console.WriteLine("Existed in two clusters : " + program);
                    }
                }
            }
            var clusterZero = _clusters.Where(x => x.ClusterValues.ContainsKey(0)).FirstOrDefault();
            return clusterZero.ClusterValues.Count;
        }*/
    }
}
