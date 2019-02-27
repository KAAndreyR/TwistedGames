using TwistedGames.Core.Common;
using TwistedGames.Core.Games.Snake;

namespace TwistedGames.Core.SingleGames.Snake
{
    public class SnakeGameManager : SingleGameManager<SnakeGame, SnakeGameState>
    {
        private const int InitialSnakeLength = 4;

        public SnakeGameManager(GameSize gameSize) : base(gameSize)
        {
            GameState = CreateStartingGameState();
            GameState.Stop();
        }

        protected sealed override SnakeGameState CreateStartingGameState()
        {
            return new SnakeGameState(GameSize, InitialSnakeLength, true);
        }
    }
}