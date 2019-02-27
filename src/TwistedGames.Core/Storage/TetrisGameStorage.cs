using TwistedGames.Core.Common;
using TwistedGames.Core.SingleGames.Tetris;

namespace TwistedGames.Core.Storage
{
    public class TetrisGameStorage : GameStorage<TetrisGameManager>
    {
        protected override TetrisGameManager CreateGame() => new TetrisGameManager(new GameSize(10, 18));
    }
}