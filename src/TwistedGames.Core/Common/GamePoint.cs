using System;

namespace TwistedGames.Core.Common
{
    public class GamePoint : IEquatable<GamePoint>
    {
        public int X { get; }
        public int Y { get; }

        public GamePoint(int x, int y)
        {
            X = x;
            Y = y;
        }

        public GamePoint AddDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:    return new GamePoint(X, Y + 1);
                case Direction.Down:  return new GamePoint(X, Y - 1);
                case Direction.Right: return new GamePoint(X + 1, Y);
                case Direction.Left:  return new GamePoint(X - 1, Y);
                default: throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        public bool Equals(GamePoint other) => other != null && X == other.X && Y == other.Y;

        public override bool Equals(object other) => Equals(other as GamePoint);

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }
    }
}