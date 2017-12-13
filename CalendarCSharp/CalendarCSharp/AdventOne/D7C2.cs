using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarCSharp.AdventOne
{
    public class D7C2
    {
        public class Disc
        {
            public string Name;
            public int Weight;
            public List<Disc> Children = new List<Disc>();
            public Disc Parent = null;
            public void AddChild(Disc child)
            {
                child.Parent = this;
                Children.Add(child);
            }
        }

        private Dictionary<string, Disc> discs = new Dictionary<string, Disc>();

        public int Output()
        {
            var allText = System.IO.File.ReadAllText(System.IO.Path.Combine(Environment.CurrentDirectory, "d7_input.txt"));
            var allLines = allText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            ProcessDiscs(allLines);
            ProcessDiscChildren(allLines);
            var bottom = FindBottomDisc(discs);
            var wrong = FindWrongDisc(bottom);
            return CalculateAdjustedWeight(wrong);
        }

        private class CalculationResult
        {
            public int index;
            public int weight;
            public CalculationResult(int i, int w)
            {
                weight = w;
                index = i;
            }
        }

        private int CalculateAdjustedWeight(Disc wrongDisc)
        {
            var parent = wrongDisc.Parent;
            List<CalculationResult> results = new List<CalculationResult>();
            int i = 0;
            foreach (var child in parent.Children)
            {
                var weight = CalculateSubTreeTotalWeight(child);
                results.Add(new CalculationResult(i, weight));
                i++;
            }
            var incorrectWeight = results.GroupBy(x => x.weight).Where(x => x.Count() == 1).SelectMany(x => x).FirstOrDefault().weight;
            var correctWeight = results.Distinct().Where(x => x.weight != incorrectWeight).FirstOrDefault().weight;
            var diff = correctWeight - incorrectWeight;
            return wrongDisc.Weight + diff;
        }

        private Disc FindWrongDisc(Disc bottom)
        {
            var pivot = bottom;
            while (true)
            {
                List<CalculationResult> results = new List<CalculationResult>();
                int i = 0;
                foreach (var child in pivot.Children)
                {
                    var weight = CalculateSubTreeTotalWeight(child);
                    results.Add(new CalculationResult(i, weight));
                    i++;
                }
                var unique = results.GroupBy(x => x.weight).Where(x => x.Count() == 1).SelectMany(x => x).FirstOrDefault();
                if(unique == null)
                {
                    return pivot;
                }
                else
                {
                    pivot = pivot.Children[unique.index];
                }
            }
        }

        private int CalculateSubTreeTotalWeight(Disc d)
        {
            int totalWeight = d.Weight;
            if (d.Children.Count != 0)
            {
                foreach (var v in d.Children)
                {
                    totalWeight += CalculateSubTreeTotalWeight(v);
                }
            }
            return totalWeight;
        }

        private Disc FindBottomDisc(Dictionary<string, Disc> discs)
        {
            var pivot = discs.FirstOrDefault().Value;
            bool found = false;
            while (!found)
            {
                if (pivot.Parent == null)
                {
                    found = true;
                }
                else
                {
                    pivot = pivot.Parent;
                }
            }
            return pivot;
        }

        private void ProcessDiscs(string[] allLines)
        {
            foreach (var line in allLines)
            {
                var allWords = line.Split(' ');
                var name = allWords[0];
                var weight = Convert.ToInt32(allWords[1].Replace("(", "").Replace(")", ""));
                var disc = new Disc() { Name = name, Weight = weight };
                discs.Add(name, disc);
            }
        }

        private void ProcessDiscChildren(string[] allLines)
        {
            foreach (var line in allLines)
            {
                var allWords = line.Split(' ');
                var name = allWords[0];
                Disc disc = discs[name];
                if (allWords.Length > 2)
                {
                    // index 2 is arrow allWords[2]; 
                    for (int i = 3; i < allWords.Length; i++)
                    {
                        var childDiscName = allWords[i].Replace(",", "");
                        Disc childDisc = discs[childDiscName];
                        disc.AddChild(childDisc);
                    }
                }
            }
        }
    }
}
