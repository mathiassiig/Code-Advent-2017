using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarCSharp.AdventTwo
{
    public class D11C1
    {

        public enum Direction
        {
            NorthWest,
            North,
            NorthEast,
            SouthEast,
            South,
            SouthWest
        }

        public class HexNode
        {
            public HexNode NWNeighbor;
            public HexNode NNeighbor;
            public HexNode NENeighbor;
            public HexNode SENeighbor;
            public HexNode SNeighbor;
            public HexNode SWNeighbor;

            public Vector2 Position;

            private List<HexNode> _neighbors;
            public List<HexNode> Neighbors
            {
                get
                {
                    if (_neighbors == null)
                    {
                        _neighbors = new List<HexNode>();
                        _neighbors.Add(NWNeighbor);
                        _neighbors.Add(NNeighbor);
                        _neighbors.Add(NENeighbor);
                        _neighbors.Add(SENeighbor);
                        _neighbors.Add(SNeighbor);
                        _neighbors.Add(SWNeighbor);
                    }
                    return _neighbors.Where(x => x != null).ToList();
                }
            }

            public HexNode(Vector2 pos)
            {
                Position = pos;
            }
        }

        public Direction ToDirection(string s)
        {
            switch (s)
            {
                case "nw":
                    return Direction.NorthWest;
                case "n":
                    return Direction.North;
                case "ne":
                    return Direction.NorthEast;
                case "se":
                    return Direction.SouthEast;
                case "s":
                    return Direction.South;
                case "sw":
                    return Direction.SouthWest;
            }
            throw new Exception("That wasn't a direction: " + s);
        }

        public class Vector2
        {
            public float X;
            public float Y;

            public Vector2(float x, float y)
            {
                X = x;
                Y = y;
            }

            public static Vector2 operator +(Vector2 a, Vector2 b)
            {
                return new Vector2(a.X + b.X, a.Y + b.Y);
            }

            public static Vector2 operator -(Vector2 a, Vector2 b)
            {
                return new Vector2(a.X - b.X, a.Y - b.Y);
            }

            public double Magnitude()
            {
                return Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2));
            }

            public Vector2 Normalized()
            {
                var length = (float)Magnitude();
                return new Vector2(X / length, Y / length);
            }

            public override bool Equals(Object obj)
            {
                var vector2 = obj as Vector2;
                if (vector2 == null)
                    return false;
                return vector2.X == X && vector2.Y == Y;
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    int hash = 17;
                    hash = hash * 23 + X.GetHashCode();
                    hash = hash * 23 + Y.GetHashCode();
                    return hash;
                }
            }
        }

        private Direction ClosestHexDirection(Vector2 unitVector)
        {
            var degrees = Math.Atan2(unitVector.Y, unitVector.X);
            if(degrees < 0)
            {
                degrees = Math.PI * 2 + degrees;
            }
            var step = Math.PI / 3;
            var directions = new List<Direction> { Direction.NorthEast, Direction.North, Direction.NorthWest, Direction.SouthWest, Direction.South, Direction.SouthEast };
            for (int i = 0; i < directions.Count(); i++)
            {
                if(degrees >= step*i && degrees < step*(i+1))
                {
                    return directions[i];
                }
            }
            throw new Exception("Couldn't find the direction");
        }

        public int ShortestSteps(HexNode start, HexNode end)
        {
            bool found = false;
            var next = new Vector2(start.Position.X, start.Position.Y);
            int steps = 0;
            while(!found)
            {
                if (next.X == end.Position.X && next.Y == end.Position.Y)
                {
                    found = true;
                }
                else
                {
                    var direction = (end.Position - next).Normalized();
                    var length = direction.Magnitude();
                    var closestDir = ClosestHexDirection(direction);
                    next = next + ToDirectionVector(closestDir);
                    steps++;
                }
            }

            return steps;
        }

        private Vector2 ToDirectionVector(Direction d)
        {
            switch (d)
            {
                case Direction.NorthWest:
                    return new Vector2(-1.5f, 0.5f);
                case Direction.North:
                    return new Vector2(0, 1f);
                case Direction.NorthEast:
                    return new Vector2(1.5f, 0.5f);
                case Direction.SouthEast:
                    return new Vector2(1.5f, -0.5f);
                case Direction.South:
                    return new Vector2(0, -1f);
                case Direction.SouthWest:
                    return new Vector2(-1.5f, -0.5f);
            }
            throw new Exception("That wasn't any known direction : " + d);
        }

        private Dictionary<Vector2, HexNode> _hexNodes = new Dictionary<Vector2, HexNode>();

        public int Output()
        {
            var allText = System.IO.File.ReadAllText(System.IO.Path.Combine(Environment.CurrentDirectory, "d11_input.txt"));
            var directions = allText.Split(',').Select(x => ToDirection(x)).ToList();

            var previousPosition = new Vector2(0, 0);
            HexNode previousNode = new HexNode(previousPosition);
            _hexNodes.Add(new Vector2(0, 0), previousNode);

            HexNode firstNode = previousNode;

            foreach (var direction in directions)
            {
                previousPosition = previousNode.Position;
                var nextPosition = previousPosition + ToDirectionVector(direction);
                HexNode nextNode = null;
                if (!_hexNodes.ContainsKey(nextPosition))
                {
                    nextNode = new HexNode(nextPosition);
                    _hexNodes.Add(nextPosition, nextNode);
                }
                else
                {
                    nextNode = _hexNodes[nextPosition];
                }
                LinkSurroundings(nextNode);
                previousNode = nextNode;
            }

            HexNode lastNode = previousNode;
            var steps = ShortestSteps(firstNode, lastNode);
            return steps;
        }

        private void LinkSurroundings(HexNode node)
        {
            var directions = Enum.GetValues(typeof(Direction)).Cast<Direction>();
            foreach (var dir in directions)
            {
                var hypotheticalNodePos = ToDirectionVector(dir) + node.Position;
                if (_hexNodes.ContainsKey(hypotheticalNodePos))
                {
                    var neighbor = _hexNodes[hypotheticalNodePos];
                    LinkNodes(dir, node, neighbor);
                }
            }
        }



        private void LinkNodes(Direction dir, HexNode from, HexNode to)
        {
            switch (dir)
            {
                case Direction.NorthWest:
                    from.NWNeighbor = to;
                    to.SENeighbor = from;
                    break;
                case Direction.North:
                    from.NNeighbor = to;
                    to.SNeighbor = from;
                    break;
                case Direction.NorthEast:
                    from.NENeighbor = to;
                    to.SWNeighbor = from;
                    break;
                case Direction.SouthEast:
                    from.SENeighbor = to;
                    to.NWNeighbor = from;
                    break;
                case Direction.South:
                    from.SNeighbor = to;
                    to.NNeighbor = from;
                    break;
                case Direction.SouthWest:
                    from.SWNeighbor = to;
                    to.NENeighbor = from;
                    break;
            }

        }
    }
}
