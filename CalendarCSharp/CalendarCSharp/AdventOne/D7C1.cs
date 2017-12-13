using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarCSharp.AdventOne
{
    public class D7C1
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
        public string Output()
        {
            var allText = System.IO.File.ReadAllText(System.IO.Path.Combine(Environment.CurrentDirectory, "d7_input.txt"));
            var allLines = allText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            var discs = ProcessDiscs(allLines);
            var bottom = FindBottomDisc(discs);
            return bottom.Name;
        }

        private Disc FindBottomDisc(Dictionary<string, Disc> discs)
        {
            var pivot = discs.FirstOrDefault().Value;
            bool found = false;
            while(!found)
            {
                if(pivot.Parent == null)
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

        private Dictionary<string, Disc> ProcessDiscs(string[] allLines)
        {
            var discs = new Dictionary<string, Disc>();
            foreach (var line in allLines)
            {
                var allWords = line.Split(' ');
                var name = allWords[0];
                var weight = Convert.ToInt32(allWords[1].Replace("(", "").Replace(")", ""));
                Disc disc = null;
                discs.TryGetValue(name, out disc);
                if (disc == null)
                {
                    disc = new Disc() { Name = name, Weight = weight };
                    discs.Add(name, disc);
                }
                // has children:
                if (allWords.Length > 2)
                {
                    // index 2 is arrow allWords[2]; 
                    for (int i = 3; i < allWords.Length; i++)
                    {
                        var childDiscName = allWords[i].Replace(",", "");
                        Disc childDisc = null;
                        discs.TryGetValue(childDiscName, out childDisc);
                        if (childDisc == null)
                        {
                            childDisc = new Disc() { Name = childDiscName };
                            discs.Add(childDiscName, childDisc);
                        }
                        disc.AddChild(childDisc);
                    }
                }
            }
            return discs;
        }
    }
}
