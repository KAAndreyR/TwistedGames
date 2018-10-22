using System;
using TwistedGames.Core.Games;

namespace TwistedGames.Core.Storage
{
    public class GameRecord<TGameManager> where TGameManager : IGameManager
    {
        public TGameManager GameManager { get; }
        public DateTime LastActivityTimestamp { get; set; } = DateTime.Now;

        public GameRecord(TGameManager gameManager)
        {
            GameManager = gameManager;
        }
    }
}