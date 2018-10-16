using System;
using TwistedGames.Core.Common;
using TwistedGames.Core.Games;
using TwistedGames.Core.Games.Snake;

namespace TwistedGames.Core.SingleGames.Snake
{
    public class SnakeGameManager : ISnakeGameManager, IGameManager<SnakeGameRepresentation>
    {
        public GameSize GameSize { get; }
        private const int InitialSnakeLength = 4;
        private SnakeGameState SnakeGameState { get; set; }
        private DiscreteCyclesCounter Counter { get; } = new DiscreteCyclesCounter(TimeSpan.FromSeconds(0.3));
        private SnakeGame Game { get; } = new SnakeGame();
        private object Locker { get; } = new object();

        public SnakeGameManager(GameSize gameSize)
        {
            GameSize = gameSize;
            SnakeGameState = new SnakeGameState(GameSize, InitialSnakeLength, true);
            SnakeGameState.Stop();
        }


        public bool AllowTeleport => true;
        public SnakeState SnakeState => SnakeGameState.SnakeState;

        public SnakeFieldState GetFieldState(GamePoint point) => SnakeGameState.GetFieldState(point);

        public void OnWallCollision() => SnakeGameState.Stop();

        public void OnBonusCollision() => SnakeGameState.IncreaseScore();

        public SnakeGameRepresentation DoAction(ActionType action)
        {
            lock (Locker)
            {
                if (SnakeGameState.IsStopped)
                {
                    if (action == ActionType.Action)
                    {
                        SnakeGameState = new SnakeGameState(GameSize, InitialSnakeLength, true);
                        Counter.Restart();
                    }
                }
                else
                {
                    Game.DoAction(this, action);
                }

                return GetActualGameState();
            }
        }

        public SnakeGameRepresentation GetActualGameState()
        {
            lock (Locker)
            {
                if (!SnakeGameState.IsStopped)
                {
                    for (int i = 0; i < Counter.GetGameTicksCount(); i++)
                    {
                        Game.OnTick(this);
                    }
                }

                return SnakeGameState.GetRepresentation();
            }
        }
    }
}