using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarCSharp.AdventOne
{
    public class D3C1
    {
        public int _input = 312051;

        public int Output()
        {
            var ring = Ring(_input);
            var values = RingCardinalValues(ring);
            var smallestDiff = values.Min(x => Math.Abs(x - _input));
            return ring + smallestDiff;
        }

        private List<int> RingCardinalValues(int ring)
        {
            var values = new List<int>();
            var biggest = RingSouthValue(ring);
            for(int i = 0; i < 4; i++)
            {
                values.Add(biggest - i*2*ring);
            }
            values.Reverse();
            return values;
        }

        private int RingSouthValue(int ring)
        {
            if (ring == 0)
            {
                return 1;
            }
            else
            {
                return RingSouthValue(ring - 1) + (ring * 8) - 1;
            }
        }

        private int Ring(int i)
        {
            var temp = i;
            int negation = 8;
            int ring = -1;
            while (temp > 0)
            {
                if (ring == -1)
                {
                    temp -= 1;
                }
                else
                {
                    temp -= negation;
                    negation += 8;
                }
                ring++;
            }
            return ring;
        }
    }
}
