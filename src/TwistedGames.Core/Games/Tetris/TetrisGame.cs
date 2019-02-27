using System;
using System.Collections.Generic;
using System.Linq;
using TwistedGames.Core.Common;

namespace TwistedGames.Core.Games.Tetris
{
    public class TetrisGame : IGame<ITetrisState>
    {
        public void DoAction(ITetrisState field, ActionType action)
        {
            var figure = field.TetrisFigure;
            switch (action)
            {
                case ActionType.Up:
                case ActionType.Action: TryRotateFigure(figure, field); break;
                case ActionType.Down:   TryShiftFigure(figure, field, Direction.Down); break;
                case ActionType.Right:  TryShiftFigure(figure, field, Direction.Right); break;
                case ActionType.Left:   TryShiftFigure(figure, field, Direction.Left); break;
                default: throw new ArgumentOutOfRangeException(nameof(action), action, null);
            }
        }

        private bool TryShiftFigure(IReadOnlyList<GamePoint> figure, ITetrisState field, Direction direction)
        {
            return TryChangeFigure(field, figure.Shift(direction));
        }

        private static bool TryChangeFigure(ITetrisState field, IReadOnlyList<GamePoint> newFigure)
        {
            var gameSize = field.GameSize;
            if (newFigure.Any(p => !p.IsAccommodated(gameSize, true)) || newFigure.Any(field.IsBusy))
                return false;
            field.TetrisFigure = newFigure;
            return true;
        }

        private void TryRotateFigure(IReadOnlyList<GamePoint> figure, ITetrisState field)
        {
            TryChangeFigure(field, figure.Rotate());
        }

        public void OnTick(ITetrisState field)
        {
            var figure = field.TetrisFigure;
            if (!TryShiftFigure(figure, field, Direction.Down))
            {
                var filledLines = figure
                    .GroupBy(p => p.Y)
                    .Where(g => Enumerable.Range(0, field.GameSize.Width)
                        .All(column => field.IsBusy(new GamePoint(column, g.Key)) || g.Any(p => p.X == column)))
                    .Select(g => g.Key)
                    .ToList();
                field.OnFigureStop(filledLines);
                if (!figure.IsAccommodated(field.GameSize))
                {
                    field.OnOwerflow();
                }
            }
        }
    }
}