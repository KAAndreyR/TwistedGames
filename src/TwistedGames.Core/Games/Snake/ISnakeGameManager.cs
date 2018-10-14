using TwistedGames.Core.Common;

namespace TwistedGames.Core.Games.Snake
{
    public interface ISnakeGameManager
    {
        SnakeState SnakeState { get; }
        GameSize GameSize { get; }
        SnakeFieldState GetFieldState(GamePoint point);
        bool AllowTeleport { get; }
        void OnWallCollision();
        void OnBonusCollision();
    }
}