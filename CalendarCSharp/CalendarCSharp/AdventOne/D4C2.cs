using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarCSharp.AdventOne
{
    public class D4C2
    {
        public int Output()
        {
            var allText = System.IO.File.ReadAllText(System.IO.Path.Combine(Environment.CurrentDirectory, "d4_input.txt"));
            var allPhrases = allText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            var validCount = 0;
            foreach (var phraseLine in allPhrases)
            {
                var dict = new Dictionary<string, string>();
                var valid = true;
                foreach (var word in phraseLine.Split(' '))
                {
                    string existing = null;
                    dict.TryGetValue(word, out existing);
                    if (existing != null)
                    {
                        valid = false;
                        break;
                    }
                    else if (AnagramExists(word, dict))
                    {
                        valid = false;
                        break;
                    }
                    dict.Add(word, word);
                }
                if (valid)
                {
                    validCount++;
                }
            }
            return validCount;
        }

        private bool AnagramExists(string input, Dictionary<string, string> dict)
        {
            foreach(var word in dict)
            {
                if(IsAnagram(input, word.Key))
                {
                    return true;
                }
            }
            return false;
        }

        // from https://stackoverflow.com/questions/16141643/given-two-strings-is-one-an-anagram-of-the-other
        private static bool IsAnagram(string s1, string s2)
        {
            if (string.IsNullOrEmpty(s1) || string.IsNullOrEmpty(s2))
                return false;
            if (s1.Length != s2.Length)
                return false;

            foreach (char c in s2)
            {
                int ix = s1.IndexOf(c);
                if (ix >= 0)
                    s1 = s1.Remove(ix, 1);
                else
                    return false;
            }

            return string.IsNullOrEmpty(s1);
        }
    }
}
