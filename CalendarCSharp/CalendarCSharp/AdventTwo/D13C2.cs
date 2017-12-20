using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarCSharp.AdventTwo
{
    public class D13C2
    {
        public class Layer
        {
            public int Range = 0;

            public bool FreeAtPicoSecond(int picoSecond)
            {
                if (Range < 2)
                    return true;
                int securitySteps = Range * 2 - 2;
                return picoSecond % securitySteps != 0;
            }
        }

        private List<Layer> _layers = new List<Layer>();

        public int Output()
        {
            var allText = System.IO.File.ReadAllText(System.IO.Path.Combine(Environment.CurrentDirectory, "d13_input.txt"));
            var allLines = allText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            var totalLayers = Convert.ToInt32(allLines.LastOrDefault().Split(new[] { ": " }, StringSplitOptions.None)[0]) + 1; //+1 because indexing starts at 0
            for (int i = 0; i < totalLayers; i++)
            {
                _layers.Add(new Layer());
            }
            foreach (var line in allLines)
            {
                var split = line.Split(new[] { ": " }, StringSplitOptions.None).Select(x => Convert.ToInt32(x)).ToList();
                var layer = split[0];
                var range = split[1];
                _layers[layer].Range = range;
            }
            int delays = 0;
            for (int i = 0; i < int.MaxValue; i++)
            {
                int suggestedDelay = i;
                bool allClear = true;
                for (int j = 0; j < _layers.Count; j++)
                {
                    if(!_layers[j].FreeAtPicoSecond(suggestedDelay+j))
                    {
                        allClear = false;
                        break;
                    }
                }
                if(allClear == true)
                {
                    delays = i;
                    break;
                }

            }
            return delays;
        }
    }
}
