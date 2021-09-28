namespace ToyRobot
{
    public struct Position
    {
        public int XCoord { get; }
        public int YCoord { get; }
        public Direction? Direction { get; }
        public Position(int xCoord, int yCoord, Direction? direction)
        {
            XCoord = xCoord;
            YCoord = yCoord;
            Direction = direction;
        }

        public override string ToString()
        {
            return $"{XCoord}, {YCoord}, {Direction}";
        }
    }
}
