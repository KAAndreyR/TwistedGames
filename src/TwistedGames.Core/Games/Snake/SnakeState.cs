using System;
using System.Collections.Generic;
using System.Linq;
using TwistedGames.Core.Common;

namespace TwistedGames.Core.Games.Snake
{
    public class SnakeState
    {
        private bool CanSnakeGrow { get; }
        public List<GamePoint> SnakePoints { get; }
        private Direction Direction { get; set; } = Direction.Right;

        public SnakeState(int initialSnakeLength, bool canSnakeGrow)
        {
            if (initialSnakeLength < 1)
                throw new ArgumentOutOfRangeException(nameof(initialSnakeLength), initialSnakeLength, "Snake length must be positive");
            CanSnakeGrow = canSnakeGrow;
            var startSnakePoints = Enumerable.Range(0, initialSnakeLength).Reverse().Select(i => new GamePoint(i, 0));
            SnakePoints = new List<GamePoint>(startSnakePoints);
        }

        
        public void Revert(GameSize gameSize)
        {
            for (int i = 0; i < SnakePoints.Count / 2; i++)
            {
                var tmp = SnakePoints[i];
                SnakePoints[i] = SnakePoints[SnakePoints.Count - i - 1];
                SnakePoints[SnakePoints.Count - i - 1] = tmp;
            }

            Direction = SnakePoints.Count > 1 
                ? SnakePoints[1].DirectionTo(SnakePoints[0], gameSize)
                : RevertDirection(Direction);
        }

        private Direction RevertDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:    return Direction.Down;
                case Direction.Down:  return Direction.Up;
                case Direction.Right: return Direction.Left;
                case Direction.Left:  return Direction.Right;
                default: throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        public void SetDirection(Direction direction, GameSize gameSize)
        {
            if (SnakePoints.Count > 1 && !SnakePoints[1].Equals(GetNextHeadPosition(direction).CorrectPoint(gameSize)))
                Direction = direction;
        }

        public GamePoint GetNextHeadPosition() => GetNextHeadPosition(Direction);
        public GamePoint GetNextHeadPosition(Direction direction) => SnakePoints[0].AddDirection(direction);

        public void Move(GamePoint nextHeadPosition, bool hasBonus)
        {
            if (!CanSnakeGrow || !hasBonus)
                SnakePoints.RemoveAt(SnakePoints.Count - 1);
            SnakePoints.Insert(0, nextHeadPosition);
        }
    }
}