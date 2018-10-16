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

        public Direction DirectionTo(GamePoint other, GameSize gameSize)
        {
            var rightDiff = (other.X - X + gameSize.Width) % gameSize.Width;
            var leftDiff = (X - other.X + gameSize.Width) % gameSize.Width;
            var upDiff = (other.Y - Y + gameSize.Height) % gameSize.Height;
            var downDiff = (Y - other.Y + gameSize.Height) % gameSize.Height;
            var xDiff = Math.Min(rightDiff, leftDiff);
            var yDiff = Math.Min(upDiff, downDiff);
            if (xDiff > yDiff)
            {
                return rightDiff < leftDiff 
                    ? Direction.Right 
                    : Direction.Left;
            }
            return upDiff < downDiff
                ? Direction.Up
                : Direction.Down;
        }

        public override string ToString() => $"({X},{Y})";
    }
}