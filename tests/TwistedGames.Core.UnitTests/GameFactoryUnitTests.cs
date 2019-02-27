using System;
using TwistedGames.Core.Games.Snake;
using TwistedGames.Core.Games.Tetris;
using TwistedGames.Core.TwoTwistedGame;
using Xunit;

namespace TwistedGames.Core.UnitTests
{
    public class GameFactoryUnitTests
    {
        private static GameFactory GameFactory => new GameFactory();

        [Fact]
        public void CreateSnakeGame()
        {
            Assert.IsType<SnakeGame>(GameFactory.CreateSnakeGame());
        }

        [Fact]
        public void CreateTetrisGame()
        {
            Assert.IsType<TetrisGame>(GameFactory.CreateTetrisGame());
        }


        [Fact]
        public void CreateGameForTwoTwistedGames_WrongGameType()
        {
            var wrongType = (TwoTwistedGameType)int.MaxValue;
            Assert.Throws<ArgumentOutOfRangeException>(() => GameFactory.CreateGameForTwoTwisted(wrongType));
        }

        [Fact]
        public void CreateGameForTwoTwistedGames_TetrisGameType()
        {
            Assert.IsType<TetrisGame>(GameFactory.CreateGameForTwoTwisted(TwoTwistedGameType.Tetris));
        }

        [Fact]
        public void CreateGameForTwoTwistedGames_SnakeGameType()
        {
            Assert.IsType<SnakeGame>(GameFactory.CreateGameForTwoTwisted(TwoTwistedGameType.Snake));
        }
    }
}
