using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarCSharp.AdventTwo
{
    public class D10C1
    {

        public int Output()
        {
            var allText = System.IO.File.ReadAllText(System.IO.Path.Combine(Environment.CurrentDirectory, "d10_input.txt"));
            List<int> lengths = allText.Split(',').Select(x => Convert.ToInt32(x)).ToList();
            var numbers = Numbers();
            int currentPosition = 0;
            int skipSize = 0;
            foreach (var length in lengths)
            {
                ReverseCircular(numbers, currentPosition, length);
                currentPosition += length + skipSize;
                skipSize++;
            }
            return numbers[0] * numbers[1];
        }

        private List<int> Numbers()
        {
            List<int> numbers = new List<int>();
            for (int i = 0; i < 256; i++)
            {
                numbers.Add(i);
            }
            return numbers;
        }

        private List<int> ReverseCircular(List<int> numbers, int index, int count)
        {
            List<int> reversedSubSection = new List<int>();
            for (int i = 0; i < count; i++)
            {
                var next = (index + i) % numbers.Count;
                reversedSubSection.Add(numbers[next]);
            }
            reversedSubSection.Reverse();
            for (int i = 0; i < count; i++)
            {
                var next = (index + i) % numbers.Count;
                numbers[next] = reversedSubSection[i];
            }
            return numbers;
        }



    }
}
