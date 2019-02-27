using System;
using TwistedGames.Core.Common;
using TwistedGames.Core.Games;

namespace TwistedGames.Core.SingleGames
{
    public abstract class GameState : IGameState
    {
        protected Random Random { get; } = new Random();
        public GameSize GameSize { get; }
        public bool IsStopped { get; private set; }
        private int Score { get; set; }

        protected GameState(GameSize gameSize)
        {
            GameSize = gameSize;
        }

        public void Stop() => IsStopped = true;

        protected void IncreaseScore(int additionalScore = 1) => Score += additionalScore;

        public GameRepresentation GetRepresentation()
        {
            return new GameRepresentation(GetFieldRepresentation(), Score, IsStopped);
        }

        private FieldState[,] GetFieldRepresentation()
        {
            var result = new FieldState[GameSize.Height, GameSize.Width];
            for (int i = 0; i < GameSize.Height; i++)
            {
                for (int j = 0; j < GameSize.Width; j++)
                {
                    result[i, j] = GetFieldState(new GamePoint(j, i));
                }
            }

            return result;
        }

        public abstract FieldState GetFieldState(GamePoint gamePoint);
    }
}