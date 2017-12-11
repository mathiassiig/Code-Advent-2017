using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarCSharp.AdventOne
{
    public class D5C1
    {
        public int Output()
        {
            var allText = System.IO.File.ReadAllText(System.IO.Path.Combine(Environment.CurrentDirectory, "d5_input.txt"));
            var allLines = allText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            var allNumbers = allLines.Select(x => Convert.ToInt32(x)).ToList();
            int index = 0;
            int jumps = 0;
            while(index >= 0 && index < allNumbers.Count)
            {
                var oldIndex = index;
                index += allNumbers[index];
                allNumbers[oldIndex] = allNumbers[oldIndex] + 1;
                jumps++;
            }
            return jumps;
        }
    }
}
