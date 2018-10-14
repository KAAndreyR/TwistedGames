using TwistedGames.Core.Games;
using TwistedGames.Core.Games.Snake;
using TwistedGames.Core.Games.Tetris;

namespace TwistedGames.Core.TwoTwistedGame
{
    public interface ITwoGamesManager : ISnakeGameManager, ITetrisManager, IGameManager<TwoGamesRepresentation>
    {
        
    }
}