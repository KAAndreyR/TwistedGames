using System.Collections;
using System.Collections.Generic;
using TwistedGames.Core.Common;
using TwistedGames.Core.Games;
using TwistedGames.Core.Games.Snake;

namespace TwistedGames.Core.SingleGames.Snake
{
    public class SnakeGameState : GameState, ISnakeGameState
    {
        public SnakeState SnakeState { get; }
        public GamePoint Bonus { get; set; }
        private List<BitArray> Walls { get; }

        public SnakeGameState(GameSize gameSize, int initialSnakeLength, bool canSnakeGrow) : base(gameSize)
        {
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

        public void OnBonusCollision()
        {
            IncreaseScore();
            Bonus = GenerateBonus();
        }

        public override FieldState GetFieldState(GamePoint point)
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

        public void OnWallCollision() => Stop();
        public bool AllowTeleport => true;
    }
}