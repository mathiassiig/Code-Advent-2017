using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CalendarCSharp.AdventOne
{
    public class D2C2
    {
        private int[,] _table;
        private const string _filename = "d2_input.txt";
        public int Output()
        {
            int total = 0;
            var allText = System.IO.File.ReadAllText(System.IO.Path.Combine(Environment.CurrentDirectory, _filename));
            var rows = allText.Split('\n');
            var columnCount = rows[0].Split('\t').Length;
            foreach (var r in rows)
            {
                var asNumbers = r.Split('\t').Select(x => Convert.ToInt32(x)).ToList();
                for (int i = 0; i < asNumbers.Count; i++)
                {
                    for (int j = 0; j < asNumbers.Count; j++)
                    {
                        if (i != j)
                        {
                            var division = asNumbers[i] / (double)asNumbers[j];
                            if (division % 1 == 0)
                            {
                                total += (int)division;
                            }
                        }
                    }
                }
            }
            return total;
        }
    }
}
