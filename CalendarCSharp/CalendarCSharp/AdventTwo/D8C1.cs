using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarCSharp.AdventTwo
{
    public class D8C1
    {
        private int ConvertToIncDec(string incDec)
        {
            if (incDec == "inc") return 1;
            else if (incDec == "dec") return -1;
            throw new Exception("incDec Wasn't inc or dec, it was: " + incDec);
        }

        private bool TestCondition(string condition, int x, int y)
        {
            switch (condition)
            {
                case ">": return x > y;
                case "<": return x < y;
                case "<=": return x <= y;
                case ">=": return x >= y;
                case "!=": return x != y;
                case "==": return x == y;
                default: throw new Exception("Condition not found bruh: " + condition);
            }
        }

        private Dictionary<string, int> _registers = new Dictionary<string, int>();

        public int Output()
        {
            var allText = System.IO.File.ReadAllText(System.IO.Path.Combine(Environment.CurrentDirectory, "d8_input.txt"));
            var allLines = allText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            foreach(var line in allLines)
            {
                var instructions = line.Split(' ');

                var modifiedRegister = instructions[0];
                var incDec = instructions[1];
                var incDecAmount = Convert.ToInt32(instructions[2]);
                var junk = instructions[3]; //just 'if'
                var conditionRegister = instructions[4];
                var condition = instructions[5];
                var conditionValueRight = Convert.ToInt32(instructions[6]);

                var incDecFinalAmount = ConvertToIncDec(incDec) * incDecAmount;
                var conditionValueLeft = GetRegisterValue(conditionRegister);

                if (TestCondition(condition, conditionValueLeft, conditionValueRight))
                {
                    var modifiedRegisterValue = GetRegisterValue(modifiedRegister);
                    _registers[modifiedRegister] = modifiedRegisterValue + incDecFinalAmount;
                }
            }
            return _registers.Max(x => x.Value);
        }

        private int GetRegisterValue(string registerName)
        {
            if(_registers.ContainsKey(registerName))
            {
                return _registers[registerName];
            }
            _registers.Add(registerName, 0);
            return _registers[registerName];
        }
    }
}
