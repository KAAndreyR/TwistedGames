using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using TwistedGames.Core.Games;

namespace TwistedGames.Core.Storage
{
    public abstract class GameStorage<TGameManager> : GameStorage, IGameStorage<TGameManager> where TGameManager : IGameManager
    {
        private readonly object _clearLocker = new object();
        private DateTime LastClearTime { get; set; } = DateTime.Now;

        private ConcurrentDictionary<Guid, GameRecord<TGameManager>> Games { get; } =
            new ConcurrentDictionary<Guid, GameRecord<TGameManager>>();

        public TGameManager GetOrCreateGame(Guid gameId)
        {
            GameRecord<TGameManager> gameRecord;
            while (!TryGetOrCreateGame(gameId, out gameRecord)) { }
            return gameRecord.GameManager;
        }

        private bool TryGetOrCreateGame(Guid gameId, out GameRecord<TGameManager> gameRecord)
        {
            gameRecord = Games.GetOrAdd(gameId, CreateGameRecord);
            var now = DateTime.Now;
            lock (gameRecord)
            {
                if (TryRemoveOutdatedGame(gameId, gameRecord, now))
                {
                    return false;
                }

                gameRecord.LastActivityTimestamp = now;
            }

            CheckClearTimestamp(now);
            return true;
        }

        private void CheckClearTimestamp(DateTime now)
        {
            // ReSharper disable once InconsistentlySynchronizedField
            if (now - LastClearTime <= CheckFrequency)
            {
                return;
            }
            lock (_clearLocker)
            {
                if (now - LastClearTime <= CheckFrequency)
                {
                    return;
                }
                Task.Run((Action) ClearOutdatedGames);
                LastClearTime = now;
            }
        }

        private void ClearOutdatedGames()
        {
            var now = DateTime.Now;
            var games = Games.ToList();
            foreach (var game in games)
            {
                if (GameIsOutdated(game.Value, now))
                {
                    lock (game.Value)
                    {
                        TryRemoveOutdatedGame(game.Key, game.Value, now);
                    }
                }
            }
        }

        private bool TryRemoveOutdatedGame(Guid gameId, GameRecord<TGameManager> record, DateTime removeTime)
        {
            if (!GameIsOutdated(record, removeTime))
            {
                return false;
            }

            if (Games.TryGetValue(gameId, out var actualRecord) && actualRecord == record)
            {
                Games.TryRemove(gameId, out _);
            }
            return true;
        }

        private static bool GameIsOutdated(GameRecord<TGameManager> record, DateTime removeTime)
        {
            return removeTime - record.LastActivityTimestamp > Timeout;
        }

        private GameRecord<TGameManager> CreateGameRecord(Guid gameId)
        {
            return new GameRecord<TGameManager>(CreateGame());
        }

        protected abstract TGameManager CreateGame();
    }

    public class GameStorage
    {
        protected static readonly TimeSpan Timeout = TimeSpan.FromMinutes(0.5);
        protected static readonly TimeSpan CheckFrequency = TimeSpan.FromMinutes(0.1);
    }
}