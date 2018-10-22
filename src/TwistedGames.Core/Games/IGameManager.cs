using TwistedGames.Core.Common;

namespace TwistedGames.Core.Games
{
    public interface IGameManager
    {
        GameRepresentation DoAction(ActionType action);
        GameRepresentation GetActualGameState();
    }
}