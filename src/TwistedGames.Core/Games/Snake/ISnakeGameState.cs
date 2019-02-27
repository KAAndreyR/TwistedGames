namespace TwistedGames.Core.Games.Snake
{
    public interface ISnakeGameState : IGameState
    {
        SnakeState SnakeState { get; }
        bool AllowTeleport { get; }
        void OnWallCollision();
        void OnBonusCollision();
    }
}