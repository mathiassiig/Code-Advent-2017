using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarCSharp.AdventOne
{
    public class D4C1
    {
        public int Output()
        {
            var allText = System.IO.File.ReadAllText(System.IO.Path.Combine(Environment.CurrentDirectory, "d4_input.txt"));
            var allPhrases = allText.Split('\n');
            var validCount = 0;
            foreach(var phraseLine in allPhrases)
            {
                var dict = new Dictionary<string, string>();
                var valid = true;
                foreach(var word in phraseLine.Split(' '))
                {
                    string existing = null;
                    dict.TryGetValue(word, out existing);
                    if(existing != null)
                    {
                        valid = false;
                        break;
                    }
                    dict.Add(word, word);
                }
                if(valid)
                {
                    validCount++;
                }
            }
            return validCount;
        }
    }
}
