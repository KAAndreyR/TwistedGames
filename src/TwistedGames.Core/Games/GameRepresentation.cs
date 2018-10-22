namespace TwistedGames.Core.Games
{
    public class GameRepresentation
    {
        public FieldState[,] Field { get; }
        public int Score { get; }
        public bool IsStopped { get; }

        public GameRepresentation(FieldState[,] field, int score, bool isStopped)
        {
            Field = field;
            Score = score;
            IsStopped = isStopped;
        }
    }
}