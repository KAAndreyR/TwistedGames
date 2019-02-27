using System;
using System.Collections.Generic;
using System.Linq;
using TwistedGames.Core.Common;

namespace TwistedGames.Core.Games.Tetris
{
    public static class FigureHelper
    {
        private const int MaxRotations = 4;

        private static readonly List<IReadOnlyList<GamePoint>> BaseFigures = new List<IReadOnlyList<GamePoint>>
        {
            // ▉
            //▉▉▉
            new List<GamePoint>
            {
                new GamePoint(0, 0),
                new GamePoint(1, 0),
                new GamePoint(2, 0),
                new GamePoint(1, 1)
            },
            //▉
            //▉▉▉
            new List<GamePoint>
            {
                new GamePoint(0, 0),
                new GamePoint(1, 0),
                new GamePoint(2, 0),
                new GamePoint(0, 1)
            },
            //  ▉
            //▉▉▉
            new List<GamePoint>
            {
                new GamePoint(0, 0),
                new GamePoint(1, 0),
                new GamePoint(2, 0),
                new GamePoint(2, 1)
            },
            //▉▉
            // ▉▉
            new List<GamePoint>
            {
                new GamePoint(1, 0),
                new GamePoint(2, 0),
                new GamePoint(0, 1),
                new GamePoint(1, 1)
            },
            // ▉▉
            //▉▉
            new List<GamePoint>
            {
                new GamePoint(0, 0),
                new GamePoint(1, 0),
                new GamePoint(1, 1),
                new GamePoint(2, 1)
            },
            //▉▉▉▉
            new List<GamePoint>
            {
                new GamePoint(0, 0),
                new GamePoint(1, 0),
                new GamePoint(2, 0),
                new GamePoint(3, 0)
            }
        };


        public static IReadOnlyList<GamePoint> GenerateNewFigure(Random random, GameSize gameSize)
        {
            var template = BaseFigures[random.Next(BaseFigures.Count)];
            var figureArea = template.GetArea();
            IReadOnlyList<GamePoint> figure = template
                .Select(p => new GamePoint(p.X + (gameSize.Width - figureArea.Size.Width) / 2, p.Y + gameSize.Height))
                .ToList();
            var rotations = random.Next(MaxRotations);
            for (int i = 0; i < rotations; i++)
            {
                figure = figure.Rotate();
            }
            return figure;
        }
    }
}