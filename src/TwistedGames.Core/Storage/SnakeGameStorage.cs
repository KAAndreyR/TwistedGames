using TwistedGames.Core.Common;
using TwistedGames.Core.SingleGames.Snake;

namespace TwistedGames.Core.Storage
{
    public class SnakeGameStorage : GameStorage<SnakeGameManager>
    {
        protected override SnakeGameManager CreateGame() => new SnakeGameManager(new GameSize(20, 15));
    }
}
