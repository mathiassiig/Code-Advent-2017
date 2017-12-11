using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarCSharp.AdventOne
{
    public class D6C2
    {
        private Dictionary<string, int> _states = new Dictionary<string, int>();
        public int Output()
        {
            var allText = System.IO.File.ReadAllText(System.IO.Path.Combine(Environment.CurrentDirectory, "d6_input.txt"));
            var allNumbers = allText.Split('\t').Select(x => Convert.ToInt32(x)).ToList();
            var gameOver = false;
            int cycles = 1;
            while (!gameOver)
            {
                var fattestBankIndex = MaxIndex(allNumbers);
                var distributionLeftover = allNumbers[fattestBankIndex] / allNumbers.Count;
                var distributionEach = allNumbers[fattestBankIndex] / (allNumbers.Count - 1);
                var distributionToGiveOut = allNumbers[fattestBankIndex];

                if (distributionEach == 0)
                {
                    distributionEach = 1;
                }
                for (int i = 0; i < allNumbers.Count; i++)
                {
                    int actualIndex = (fattestBankIndex + i) % allNumbers.Count;
                    if (actualIndex == fattestBankIndex)
                    {
                        allNumbers[actualIndex] = distributionLeftover;
                        distributionToGiveOut -= distributionLeftover;
                    }
                    else
                    {
                        allNumbers[actualIndex] += distributionEach;
                        distributionToGiveOut -= distributionEach;
                    }
                    if (distributionToGiveOut == 0)
                    {
                        break;
                    }
                }


                var state = ToState(allNumbers);
                int existingState = 0;
                _states.TryGetValue(state, out existingState);
                if (existingState != 0)
                {
                    return cycles - existingState;
                }
                else
                {
                    _states.Add(state, cycles);
                    cycles++;
                }
            }
            return cycles;
        }

        private string ToState(List<int> intList)
        {
            string state = "";
            foreach (var i in intList)
            {
                state += string.Format("{0}-", i);
            }
            return state;
        }

        private int MaxIndex(List<int> intList)
        {
            var max = -1;
            var maxIndex = -1;
            for (int i = 0; i < intList.Count; i++)
            {
                if (intList[i] > max)
                {
                    maxIndex = i;
                    max = intList[i];
                }
            }
            return maxIndex;
        }
    }
}
