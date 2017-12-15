using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarCSharp.AdventTwo
{
    public class D10C2
    {

        public string Output()
        {
            var allText = System.IO.File.ReadAllText(System.IO.Path.Combine(Environment.CurrentDirectory, "d10_input.txt"));
            List<byte> lengths = allText.Select(x => Encoding.Default.GetBytes(x.ToString())[0]).ToList();
            lengths.AddRange(new byte[] { 17, 31, 73, 47, 23 });
            var sparseHash = Numbers();
            int currentPosition = 0;
            int skipSize = 0;
            for (int i = 0; i < 64; i++)
            {
                foreach (var length in lengths)
                {
                    ReverseCircular(sparseHash, currentPosition, length);
                    currentPosition += length + skipSize;
                    skipSize++;
                }
            }
            var denseHash = DenseHash(sparseHash);
            return ToHexString(denseHash);
            
        }

        private string ToHexString(List<int> list)
        {
            string hexString = "";
            foreach(var i in list)
            {
                hexString += i.ToString("x");
            }
            return hexString;
        }

        private List<int> DenseHash(List<int> sparseHash)
        {
            List<int> denseHash = new List<int>();
            for (int i = 0; i < 16; i++)
            {
                var ii = i * 16;
                var next =
                    sparseHash[ii] ^ sparseHash[ii+1] ^ sparseHash[ii+2] ^ sparseHash[ii+3] ^
                    sparseHash[ii+4] ^ sparseHash[ii+5] ^ sparseHash[ii+6] ^ sparseHash[ii+7] ^
                    sparseHash[ii+8] ^ sparseHash[ii+9] ^ sparseHash[ii+10] ^ sparseHash[ii+11] ^
                    sparseHash[ii+12] ^ sparseHash[ii+13] ^ sparseHash[ii+14] ^ sparseHash[ii+15];
                denseHash.Add(next);
            }
            return denseHash;
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
