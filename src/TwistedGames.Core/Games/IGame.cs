using TwistedGames.Core.Common;

namespace TwistedGames.Core.Games
{
    public interface IGame<in TField>
    {
        void DoAction(TField field, ActionType action);
        void OnTick(TField field);
    }
}