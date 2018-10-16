using System;
using TwistedGames.Core.Common;

namespace TwistedGames.Core.Games.Snake
{
    public class SnakeGame : IGame<ISnakeGameManager>
    {
        public void DoAction(ISnakeGameManager manager, ActionType action)
        {
            var snake = manager.SnakeState;
            switch (action)
            {
                case ActionType.Action:
                    snake.Revert(manager.GameSize);
                    break;
                case ActionType.Up:
                    snake.SetDirection(Direction.Up);
                    break;
                case ActionType.Down:
                    snake.SetDirection(Direction.Down);
                    break;
                case ActionType.Right:
                    snake.SetDirection(Direction.Right);
                    break;
                case ActionType.Left:
                    snake.SetDirection(Direction.Left);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(action), action, null);
            }
        }

        public void OnTick(ISnakeGameManager manager)
        {
            var snake = manager.SnakeState;
            var nextHeadPosition = snake.GetNextHeadPosition();
            var gameSize = manager.GameSize;
            if (!manager.AllowTeleport && !CheckBorders(nextHeadPosition, gameSize))
            {
                manager.OnWallCollision();
                return;
            }
            nextHeadPosition = CorrectNextHeadPosition(nextHeadPosition, gameSize);
            var fieldState = manager.GetFieldState(nextHeadPosition);

            switch (fieldState)
            {
                case SnakeFieldState.Wall:
                case SnakeFieldState.Snake:
                    manager.OnWallCollision();
                    return;
                case SnakeFieldState.Empty:
                    snake.Move(nextHeadPosition, false);
                    break;
                case SnakeFieldState.Bonus:
                    snake.Move(nextHeadPosition, true);
                    manager.OnBonusCollision();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(fieldState), fieldState, "Unexpected fieldState");
            }
        }

        public GamePoint CorrectNextHeadPosition(GamePoint point, GameSize gameSize)
        {
            if (point.X < 0) point = new GamePoint(gameSize.Width + point.X, point.Y);
            if (point.Y < 0) point = new GamePoint(point.X, gameSize.Height + point.Y);
            if (point.X >= gameSize.Width) point = new GamePoint(point.X % gameSize.Width, point.Y);
            if (point.Y >= gameSize.Height) point = new GamePoint(point.X, point.Y % gameSize.Height);
            return point;
        }

        public bool CheckBorders(GamePoint point, GameSize gameSize)
        {
            return point.X >= 0 && point.Y >= 0 && point.X < gameSize.Width && point.Y < gameSize.Height;
        }
    }
}