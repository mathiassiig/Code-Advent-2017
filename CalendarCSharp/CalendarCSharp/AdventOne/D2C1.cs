using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CalendarCSharp.AdventOne
{
    public class D2C1
    {
        private int[,] _table;
        private const string _filename = "d2c1_input.txt";
        public int Output()
        {
            int total = 0;
            var allText = System.IO.File.ReadAllText(System.IO.Path.Combine(Environment.CurrentDirectory, _filename));
            var rows = allText.Split('\n');
            var columnCount = rows[0].Split('\t').Length;
            foreach (var r in rows)
            {
                var asNumbers = r.Split('\t').Select(x => Convert.ToInt32(x)).ToList();
                var biggest = asNumbers.Max(x => x);
                var smallest = asNumbers.Min(x => x);
                var diff = biggest - smallest;
                total += diff;
            }
            return total;
        }
    }
}
