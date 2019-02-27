namespace TwistedGames.Core.Common
{
    public class GameArea
    {
        public GamePoint LeftBottomPoint { get; }
        public GameSize Size { get; }

        public GameArea(GamePoint leftBottomPoint, GameSize size)
        {
            LeftBottomPoint = leftBottomPoint;
            Size = size;
        }

        public GamePoint Center 
            => new GamePoint(LeftBottomPoint.X + Size.Width / 2, LeftBottomPoint.Y + Size.Height / 2);
    }
}