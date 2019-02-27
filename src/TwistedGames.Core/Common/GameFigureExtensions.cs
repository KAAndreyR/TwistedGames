using System;
using System.Collections.Generic;
using System.Linq;

namespace TwistedGames.Core.Common
{
    public static class GameFigureExtensions
    {
        public static GameArea GetArea(this IReadOnlyList<GamePoint> points)
        {
            if (points.Count == 0)
                return new GameArea(new GamePoint(0, 0), new GameSize(0, 0));
            var firstPoint = points[0];
            int minX = firstPoint.X, minY = firstPoint.Y, maxX = firstPoint.X, maxY = firstPoint.Y;
            for (var index = 1; index < points.Count; index++)
            {
                var gamePoint = points[index];
                minX = Math.Min(minX, gamePoint.X);
                minY = Math.Min(minY, gamePoint.Y);
                maxX = Math.Max(maxX, gamePoint.X);
                maxY = Math.Max(maxY, gamePoint.Y);
            }
            return new GameArea(new GamePoint(minX, minY), new GameSize(maxX - minX, maxY - minY));
        }

        public static IReadOnlyList<GamePoint> Rotate(this IReadOnlyList<GamePoint> points)
        {
            if (points.Count == 0)
                return points;
            var center = points.GetArea().Center;
            return points
                .Select(p => new GamePoint(center.X + (p.Y - center.Y), center.Y - (p.X - center.X)))
                .ToList();
        }

        public static IReadOnlyList<GamePoint> Shift(this IReadOnlyList<GamePoint> points, Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:    return points.Shift(0, 1);
                case Direction.Down:  return points.Shift(0, -1);
                case Direction.Right: return points.Shift(1, 0);
                case Direction.Left:  return points.Shift(-1, 0);
                default: throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        private static IReadOnlyList<GamePoint> Shift(this IReadOnlyList<GamePoint> points, int deltaX, int deltaY)
            => points
                .Select(p => p.Shift(deltaX, deltaY))
                .ToList();


        public static bool IsAccommodated(this IReadOnlyList<GamePoint> points, GameSize gameSize, bool allowTopOverflow = false)
            => points.All(point => point.IsAccommodated(gameSize, allowTopOverflow));
    }
}