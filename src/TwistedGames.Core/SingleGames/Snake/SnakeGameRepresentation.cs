using TwistedGames.Core.Games.Snake;

namespace TwistedGames.Core.SingleGames.Snake
{
    public class SnakeGameRepresentation
    {
        public SnakeFieldState[,] Field { get; }
        public int Score { get; }
        public bool IsStopped { get; }

        public SnakeGameRepresentation(SnakeFieldState[,] field, int score, bool isStopped)
        {
            Field = field;
            Score = score;
            IsStopped = isStopped;
        }
    }
}