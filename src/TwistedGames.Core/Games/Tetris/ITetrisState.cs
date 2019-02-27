using System.Collections.Generic;
using TwistedGames.Core.Common;

namespace TwistedGames.Core.Games.Tetris
{
    public interface ITetrisState : IGameState
    {
        IReadOnlyList<GamePoint> TetrisFigure { get; set; }
        bool IsBusy(GamePoint point);
        void OnFigureStop(List<int> filledLines);
        void OnOwerflow();
    }
}