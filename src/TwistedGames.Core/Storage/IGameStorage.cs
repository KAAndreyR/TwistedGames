using System;
using TwistedGames.Core.Games;

namespace TwistedGames.Core.Storage
{
    public interface IGameStorage<out TGameManager> where TGameManager : IGameManager
    {
        TGameManager GetOrCreateGame(Guid gameId);
    }
}