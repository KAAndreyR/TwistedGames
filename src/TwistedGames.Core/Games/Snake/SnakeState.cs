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
        private Direction Direction { get; set; }

        public SnakeState(int initialSnakeLength, bool canSnakeGrow)
        {
            if (initialSnakeLength < 1)
                throw new ArgumentOutOfRangeException(nameof(initialSnakeLength), initialSnakeLength, "Snake length must be positive");
            CanSnakeGrow = canSnakeGrow;
            SnakePoints = new List<GamePoint>(Enumerable.Range(0, initialSnakeLength).Select(i => new GamePoint(i, 0)));
        }

        
        public void Revert()
        {
            for (int i = 0; i < SnakePoints.Count / 2; i++)
            {
                var tmp = SnakePoints[i];
                SnakePoints[i] = SnakePoints[SnakePoints.Count - i - 1];
                SnakePoints[SnakePoints.Count - i - 1] = tmp;
            }
        }

        public void SetDirection(Direction direction)
        {
            if (SnakePoints.Count > 1 && SnakePoints[1] != SnakePoints[0].AddDirection(direction))
                Direction = direction;
        }

        public GamePoint GetNextHeadPosition() => SnakePoints[0].AddDirection(Direction);

        public void Move(GamePoint nextHeadPosition, bool hasBonus)
        {
            if (!CanSnakeGrow || !hasBonus)
                SnakePoints.RemoveAt(SnakePoints.Count - 1);
            SnakePoints.Insert(0, nextHeadPosition);
        }
    }
}