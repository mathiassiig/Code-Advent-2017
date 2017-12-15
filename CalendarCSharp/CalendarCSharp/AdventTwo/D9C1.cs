using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarCSharp.AdventTwo
{
    public class D9C1
    {
        //private Dictionary<int, int> groups = new Dictionary<int, int>();
        private int depthLevel = 0;
        private int totalScore = 0;
        public int Output()
        {
            var allText = System.IO.File.ReadAllText(System.IO.Path.Combine(Environment.CurrentDirectory, "d9_input.txt"));
            bool garbage = false;
            bool ignoreNext = false;
            foreach (var c in allText)
            {
                if (ignoreNext)
                {
                    ignoreNext = false;
                }
                else
                {
                    if (garbage)
                    {
                        switch (c)
                        {
                            case '!':
                                ignoreNext = true;
                                break;
                            case '>':
                                garbage = false;
                                break;
                        }
                    }
                    else
                    {
                        switch (c)
                        {
                            case '!':
                                ignoreNext = true;
                                break;
                            case '<':
                                garbage = true;
                                break;
                            case '{':
                                depthLevel++;
                                break;
                            case '}':
                                totalScore += depthLevel;
                                depthLevel--;
                                break;
                        }
                    }
                }
            }
            return totalScore;
        }



    }
}
