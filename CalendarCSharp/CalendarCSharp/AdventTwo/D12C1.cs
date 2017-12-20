using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarCSharp.AdventTwo
{
    public class D12C1
    {
        public class Line
        {
            public int Left;
            public List<int> Right;
        }

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
        }
    }
}
