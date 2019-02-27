using TwistedGames.Core.Common;
using TwistedGames.Core.Games.Tetris;

namespace TwistedGames.Core.SingleGames.Tetris
{
    public class TetrisGameManager : SingleGameManager<TetrisGame, TetrisGameState>
    {
        public TetrisGameManager(GameSize gameSize) : base(gameSize)
        {
            GameState = CreateStartingGameState();
            GameState.Stop();
        }

        protected sealed override TetrisGameState CreateStartingGameState()
        {
            return new TetrisGameState(GameSize);
        }
    }
}