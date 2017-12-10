using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarCSharp.AdventOne
{
    public class D3C2
    {
        public Dictionary<Tuple<int, int>, int> Nodes = new Dictionary<Tuple<int, int>, int>();
        public int _input = 312051;


        public enum Direction
        {
            Right,
            Up,
            Left,
            Down
        }

        public int Output()
        {
            int ring = 0;
            var dir = Direction.Right;
            int rAmount = 1;
            int uAmount = 0;
            int lAmount = 0;
            int dAmount = 0;

            // R0, R1, R2
            // 2*R -1 for up
            // 2*R for left
            // 2*R for down
            // 2*R+1 for right
            var nextPosition = new Tuple<int, int>(0, 0);
            var output = 0;
            Nodes.Add(nextPosition, 1);
            while (output < _input)
            {
                if (rAmount == 0)
                {
                    dir = Direction.Up;
                    ring++;
                    uAmount = 2 * ring - 1;
                    lAmount = 2 * ring;
                    dAmount = 2 * ring;
                    rAmount = 2 * ring + 1;
                }
                if (uAmount == 0 && dir == Direction.Up) dir = Direction.Left;
                if (lAmount == 0 && dir == Direction.Left) dir = Direction.Down;
                if (dAmount == 0 && dir == Direction.Down) dir = Direction.Right;

                var lastPosition = Nodes.LastOrDefault().Key;
                var dirVector = ConvertDirToVector(dir);
                nextPosition = new Tuple<int, int>(lastPosition.Item1 + dirVector.Item1, lastPosition.Item2 + dirVector.Item2);
                var sum = GetNeighborSum(nextPosition.Item1, nextPosition.Item2);
                Nodes.Add(nextPosition, sum);
                switch (dir)
                {
                    case Direction.Right:
                        rAmount--;
                        break;
                    case Direction.Up:
                        uAmount--;
                        break;
                    case Direction.Left:
                        lAmount--;
                        break;
                    case Direction.Down:
                        dAmount--;
                        break;
                }
                output = sum;
            }
            return output;
        }

        private Tuple<int, int> ConvertDirToVector(Direction dir)
        {
            switch (dir)
            {
                case Direction.Right:
                    return new Tuple<int, int>(1, 0);
                case Direction.Up:
                    return new Tuple<int, int>(0, 1);
                case Direction.Left:
                    return new Tuple<int, int>(-1, 0);
                case Direction.Down:
                    return new Tuple<int, int>(0, -1);
            }
            return new Tuple<int, int>(0, 0);
        }

        private int GetNeighborSum(int x, int y)
        {
            int n, ne, e, se, s, sw, w, nw = 0;
            Nodes.TryGetValue(new Tuple<int, int>(x, y + 1), out n);
            Nodes.TryGetValue(new Tuple<int, int>(x + 1, y + 1), out ne);
            Nodes.TryGetValue(new Tuple<int, int>(x + 1, y), out e);
            Nodes.TryGetValue(new Tuple<int, int>(x + 1, y - 1), out se);
            Nodes.TryGetValue(new Tuple<int, int>(x, y - 1), out s);
            Nodes.TryGetValue(new Tuple<int, int>(x - 1, y - 1), out sw);
            Nodes.TryGetValue(new Tuple<int, int>(x - 1, y), out w);
            Nodes.TryGetValue(new Tuple<int, int>(x - 1, y + 1), out nw);
            return n + ne + e + se + s + sw + w + nw;
        }

    }
}
