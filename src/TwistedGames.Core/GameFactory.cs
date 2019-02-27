using System;
using TwistedGames.Core.Games;
using TwistedGames.Core.Games.Snake;
using TwistedGames.Core.Games.Tetris;
using TwistedGames.Core.TwoTwistedGame;

namespace TwistedGames.Core
{
    public class GameFactory
    {
        public IGame<ITwoGamesField> CreateGameForTwoTwisted(TwoTwistedGameType gameType)
        {
            switch (gameType)
            {
                case TwoTwistedGameType.Snake:
                    return CreateSnakeGame();
                case TwoTwistedGameType.Tetris:
                    return CreateTetrisGame();
                default:
                    throw new ArgumentOutOfRangeException(nameof(gameType), gameType, null);
            }
        }

        public static IGame<ITetrisState> CreateTetrisGame()
        {
            return new TetrisGame();
        }

        public static SnakeGame CreateSnakeGame()
        {
            return new SnakeGame();
        }
    }
}