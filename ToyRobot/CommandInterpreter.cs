using System;

namespace ToyRobot
{
    public static class CommandInterpreter
    {
        public static Command GetCommandFromString(string command)
        {
            if (string.IsNullOrEmpty(command))
            {
                throw new InvalidOperationException("Invalid command passed");
            }
            command = command.Trim().ToUpper();
            if (command.StartsWith("PLACE"))
            {
                var commandStringList = command.Split(" ");
                if (commandStringList.Length != 2)
                {
                    throw new InvalidOperationException("Invalid format for PLACE command passed");
                }
                var coordinatesAndDirection  = commandStringList[1].Split(",");
                if (coordinatesAndDirection.Length != 2 && coordinatesAndDirection.Length != 3)
                {
                    throw new InvalidOperationException("Invalid format for PLACE command passed");
                }
                var directionEnum = coordinatesAndDirection.Length == 3 ?
                                    (Direction?)Enum.Parse(typeof(Direction), coordinatesAndDirection[2]) :
                                    null;
                var placePosition = new Position(int.Parse(coordinatesAndDirection[0]), int.Parse(coordinatesAndDirection[1]), directionEnum);
                return new PlaceCommand(CommandType.PLACE, placePosition);
            }
          
            switch(command)
            {
                case "MOVE":
                    {
                        return new Command(CommandType.MOVE);
                    }
                case "LEFT":
                    {
                        return new Command(CommandType.LEFT);
                    }
                case "RIGHT":
                    {
                        return new Command(CommandType.RIGHT);
                    }
                case "REPORT":
                    {
                        return new Command(CommandType.REPORT);
                    }
                default:
                    throw new InvalidOperationException("Invalid command passed");
            }
        }

    }
}
