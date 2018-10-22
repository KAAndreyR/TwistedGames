using System;
using System.Collections;
using System.Collections.Generic;
using TwistedGames.Core.Common;
using TwistedGames.Core.Games;
using TwistedGames.Core.Games.Snake;

namespace TwistedGames.Core.SingleGames.Snake
{
    public class SnakeGameState
    {
        private Random Random { get; } = new Random();
        public GameSize GameSize { get; }
        public SnakeState SnakeState { get; }
        public GamePoint Bonus { get; set; }
        private List<BitArray> Walls { get; }
        public bool IsStopped { get; private set; }
        public int Score { get; private set; }

        public SnakeGameState(GameSize gameSize, int initialSnakeLength, bool canSnakeGrow)
        {
            GameSize = gameSize;
            SnakeState = new SnakeState(initialSnakeLength, canSnakeGrow);
            Walls = new List<BitArray>(gameSize.Height);
            for (int i = 0; i < gameSize.Height; i++)
            {
                Walls.Add(new BitArray(gameSize.Width));
            }

            Bonus = GenerateBonus();
        }

        private GamePoint GenerateBonus()
        {
            GamePoint newBonus;
            do
            {
                newBonus = new GamePoint(Random.Next(GameSize.Width), Random.Next(GameSize.Height));
            } while (GetFieldState(newBonus) != FieldState.Empty);
            return newBonus;
        }

        public void Stop()
        {
            IsStopped = true;
        }

        public void IncreaseScore()
        {
            Score++;
            Bonus = GenerateBonus();
        }

        public FieldState GetFieldState(GamePoint point)
        {
            if (point.Equals(Bonus))
                return FieldState.Bonus;
            if (Walls[point.Y][point.X])
                return FieldState.Wall;
            if (SnakeState.SnakePoints[0].Equals(point))
                return FieldState.SnakeHead;
            if (SnakeState.SnakePoints.Contains(point))
                return FieldState.Snake;
            return FieldState.Empty;
        }

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
    }
}