using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarCSharp.AdventTwo
{
    public class D12C2
    {
        public class Cluster
        {
            public Dictionary<int, int> ClusterValues = new Dictionary<int, int>();
        }

        public List<Cluster> _clusters = new List<Cluster>();
        public int Output()
        {
            var allText = System.IO.File.ReadAllText(System.IO.Path.Combine(Environment.CurrentDirectory, "d12_input.txt"));
            var allLines = allText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
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
            return _clusters.Count;
        }
    }
}
