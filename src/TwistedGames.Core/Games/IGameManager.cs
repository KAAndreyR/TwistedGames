namespace TwistedGames.Core.Games
{
    public interface IGameManager<out TGameRepresentation>
    {
        TGameRepresentation DoAction(ActionType action);
        TGameRepresentation GetActualGameState();
    }
}