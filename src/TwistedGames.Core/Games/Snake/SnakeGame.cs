using System;
using TwistedGames.Core.Common;

namespace TwistedGames.Core.Games.Snake
{
    public class SnakeGame : IGame<ISnakeGameState>
    {
        public void DoAction(ISnakeGameState field, ActionType action)
        {
            var snake = field.SnakeState;
            switch (action)
            {
                case ActionType.Action:
                    snake.Revert(field.GameSize);
                    break;
                case ActionType.Up:
                    snake.SetDirection(Direction.Up, field.GameSize);
                    break;
                case ActionType.Down:
                    snake.SetDirection(Direction.Down, field.GameSize);
                    break;
                case ActionType.Right:
                    snake.SetDirection(Direction.Right, field.GameSize);
                    break;
                case ActionType.Left:
                    snake.SetDirection(Direction.Left, field.GameSize);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(action), action, null);
            }
        }

        public void OnTick(ISnakeGameState field)
        {
            var snake = field.SnakeState;
            var nextHeadPosition = snake.GetNextHeadPosition();
            var gameSize = field.GameSize;
            if (!field.AllowTeleport && !nextHeadPosition.IsAccommodated(gameSize))
            {
                field.OnWallCollision();
                return;
            }
            nextHeadPosition = nextHeadPosition.CorrectPoint(gameSize);
            var fieldState = field.GetFieldState(nextHeadPosition);

            switch (fieldState)
            {
                case FieldState.Wall:
                case FieldState.Snake:
                    field.OnWallCollision();
                    return;
                case FieldState.Empty:
                    snake.Move(nextHeadPosition, false);
                    break;
                case FieldState.Bonus:
                    snake.Move(nextHeadPosition, true);
                    field.OnBonusCollision();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(fieldState), fieldState, "Unexpected fieldState");
            }
        }
    }
}