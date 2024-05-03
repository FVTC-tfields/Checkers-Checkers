namespace Checkers.UI2.Models
{
    public class GamePiece
    {
        public int row { get; set; }
        public int column { get; set; }
        public Direction direction { get; set; }
        public string Color { get; set; }
    }

    public enum Direction
    {
        Up,
        Down,
        Both
    }
}
