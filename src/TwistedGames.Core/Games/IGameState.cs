using TwistedGames.Core.Common;

namespace TwistedGames.Core.Games
{
    public interface IGameState
    {
        GameSize GameSize { get; }
        FieldState GetFieldState(GamePoint point);
    }
}