namespace TwistedGames.Core.Games
{
    public interface IGame<in TManager>
    {
        void DoAction(TManager manager, ActionType action);
        void OnTick(TManager manager);
    }
}