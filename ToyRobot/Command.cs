namespace ToyRobot
{
    public enum CommandType
    {
        PLACE,
        MOVE,
        LEFT,
        RIGHT,
        REPORT
    }
    public class Command
    {
        public CommandType BaseCommand { get; }
        public Command(CommandType baseCommand)
        {
            BaseCommand = baseCommand;
        }
    }

    public class PlaceCommand : Command
    {
        public Position Position { get; }
        public PlaceCommand(CommandType baseCommand, Position position): base(baseCommand)
        {
            Position = position;
        }
    }
}
