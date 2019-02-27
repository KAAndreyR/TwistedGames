using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TwistedGames.Core.Common;
using TwistedGames.Core.Games;
using TwistedGames.Core.Games.Tetris;

namespace TwistedGames.Core.SingleGames.Tetris
{
    public class TetrisGameState : GameState, ITetrisState
    {
        public IReadOnlyList<GamePoint> TetrisFigure { get; set; }
        private List<BitArray> Walls { get; }

        public TetrisGameState(GameSize gameSize) : base(gameSize)
        {
            GenerateFigure();
            Walls = new List<BitArray>(gameSize.Height);
            for (int i = 0; i < gameSize.Height; i++)
            {
                AddWallLevel();
            }
        }

        private void GenerateFigure()
        {
            TetrisFigure = FigureHelper.GenerateNewFigure(Random, GameSize);
        }


        public override FieldState GetFieldState(GamePoint point)
        {
            if (TetrisFigure.Contains(point))
                return FieldState.TetrisFigure;
            if (Walls[point.Y][point.X])
                return FieldState.Wall;
            return FieldState.Empty;
        }

        public bool IsBusy(GamePoint point)
        {
            return point.IsAccommodated(GameSize) && Walls[point.Y][point.X];
        }

        public void OnFigureStop(List<int> filledLines)
        {
            foreach (var point in TetrisFigure)
            {
                if (point.IsAccommodated(GameSize))
                {
                    Walls[point.Y][point.X] = true;
                }
            }

            foreach (var line in filledLines.OrderByDescending(line => line))
            {
                Walls.RemoveAt(line);
                AddWallLevel();
            }
            IncreaseScore(filledLines.Count);
            GenerateFigure();
        }

        public void OnOwerflow() => Stop();

        private void AddWallLevel()
        {
            Walls.Add(new BitArray(GameSize.Width));
        }
    }
}