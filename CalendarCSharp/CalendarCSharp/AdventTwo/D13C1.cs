using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarCSharp.AdventTwo
{
    public class D13C1
    {
        public class Layer
        {
            public int Depth = 0;
            public int Range = 0;
            private int _securityScannerIndex = 0;
            private int _securityScannerDirection = -1;

            public void Tick()
            {
                if (Range < 2)
                    return;
                if(_securityScannerIndex == 0)
                {
                    _securityScannerDirection = 1;
                }
                if(_securityScannerIndex == Range-1)
                {
                    _securityScannerDirection = -1;
                }
                _securityScannerIndex += _securityScannerDirection;
            }

            public bool HasScannerOnZero()
            {
                if (Range == 0)
                    return false;
                if (_securityScannerIndex == 0)
                    return true;
                return false;
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
                _layers.Add(new Layer() { Depth = i });
            }
            foreach (var line in allLines)
            {
                var split = line.Split(new[] { ": " }, StringSplitOptions.None).Select(x => Convert.ToInt32(x)).ToList();
                var layer = split[0];
                var range = split[1];
                _layers[layer].Range = range;
            }
            int totalSeverity = 0;
            for (int i = 0; i < totalLayers; i++)
            {
                if(_layers[i].HasScannerOnZero())
                {
                    totalSeverity += _layers[i].Range * _layers[i].Depth;
                }
                foreach(var layer in _layers)
                {
                    layer.Tick();
                }
            }
            return totalSeverity;
        }
    }
}
