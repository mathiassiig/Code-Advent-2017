using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarCSharp.AdventOne
{
    public class D6C1
    {
        private Dictionary<string, string> _states = new Dictionary<string, string>();
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
                string existingState = null;
                _states.TryGetValue(state, out existingState);
                if (existingState != null)
                {
                    gameOver = true;
                }
                else
                {
                    _states.Add(state, state);
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
                if(intList[i] > max)
                {
                    maxIndex = i;
                    max = intList[i];
                }
            }
            return maxIndex;
        }
    }
}
