using System;
using TwistedGames.Core.Common;
using TwistedGames.Core.Games;

namespace TwistedGames.Core.SingleGames
{
    public abstract class SingleGameManager<TGame, TGameState> : IGameManager
        where TGame : IGame<TGameState>, new()
        where TGameState : GameState
    {
        protected GameSize GameSize { get; }
        protected TGameState GameState { get; set; }
        private DiscreteCyclesCounter Counter { get; } = new DiscreteCyclesCounter(TimeSpan.FromSeconds(0.3));
        private TGame Game { get; } = new TGame();
        private object Locker { get; } = new object();

        protected SingleGameManager(GameSize gameSize)
        {
            GameSize = gameSize;
        }

        public GameRepresentation DoAction(ActionType action)
        {
            lock (Locker)
            {
                if (GameState.IsStopped)
                {
                    if (action == ActionType.Action)
                    {
                        GameState = CreateStartingGameState();
                        Counter.Restart();
                    }
                }
                else
                {
                    Game.DoAction(GameState, action);
                }

                return GetActualGameState();
            }
        }

        public GameRepresentation GetActualGameState()
        {
            lock (Locker)
            {
                if (!GameState.IsStopped)
                {
                    for (int i = 0; i < Counter.GetGameTicksCount(); i++)
                    {
                        Game.OnTick(GameState);
                    }
                }

                return GameState.GetRepresentation();
            }
        }

        protected abstract TGameState CreateStartingGameState();
    }
}