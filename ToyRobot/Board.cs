using System;
using System.Collections.Generic;
using System.Text;

namespace ToyRobot
{
    public class Board
    {
        private bool placed;
        public Position currentPosition { get; private set; }
        private int xCoord;
        private int yCoord;
        public Board(int xMax, int yMax)
        {
            xCoord = xMax;
            yCoord = yMax;
            currentPosition = new Position(0, 0, Direction.NORTH);
        }

        public (bool Result, string Message) ExecuteCommand(Command command)
        {
            if (!placed && !(command is PlaceCommand))
            {
                return (false, "Place command needs to be entered first");
            }

            if (command is PlaceCommand)
            {
                return Place(((PlaceCommand)command).Position);
            }
            
            switch (command.BaseCommand)
            {
                case CommandType.LEFT:
                {
                    return Left();
                }
                case CommandType.RIGHT:
                {
                    return Right();
                }
                case CommandType.MOVE:
                {
                    return Move();
                }
                case CommandType.REPORT:
                {
                    return Report();
                }
                default:
                    return (false, "Invalid command entered");
            }

        }

        private (bool Result, string Message) Place(Position position)
        {
            if (!position.Direction.HasValue)
            {
                if (!placed)
                {
                    return (false, "Direction is required when placing the robot");
                }
                else
                {
                    position = new Position(position.XCoord, position.YCoord, currentPosition.Direction);
                }
            }

            if (!IsValidMove(position))
            {
                return (false, "Please enter place coordinates within the bounds of the board");
            }
           

            this.currentPosition = position;
            placed = true;
            return (true, "Placed Successfully");
        }

        private (bool Result, string Message) Move()
        {
            var requestedPosition = default(Position);
            switch (currentPosition.Direction)
            {
                case Direction.NORTH:
                    {
                        requestedPosition = new Position(currentPosition.XCoord, currentPosition.YCoord + 1, currentPosition.Direction);
                        break;
                    }
                case Direction.WEST:
                    {
                        requestedPosition = new Position(currentPosition.XCoord - 1, currentPosition.YCoord, currentPosition.Direction);
                        break;
                    }
                case Direction.SOUTH:
                    {
                        requestedPosition = new Position(currentPosition.XCoord, currentPosition.YCoord - 1, currentPosition.Direction);
                        break;
                    }
                case Direction.EAST:
                    {
                        requestedPosition = new Position(currentPosition.XCoord + 1, currentPosition.YCoord, currentPosition.Direction);
                        break;
                    }
            }

            if (!IsValidMove(requestedPosition))
            {
                return (false, "Invalid Move");
            }

            currentPosition = requestedPosition;
            return (true, "Moved Successfully");
        }

        private (bool Result, string Message) Left()
        {
            currentPosition = GetDirectionBasedOnCurrentPosition(MoveDirection.LEFT);
            return (true, "Turned left successfully");
        }

        private (bool Result, string Message) Right()
        {
            currentPosition = GetDirectionBasedOnCurrentPosition(MoveDirection.RIGHT);
            return (true, "Turned right successfully");
        }

        private (bool Result, string Message) Report()
        {
            return (true, currentPosition.ToString());
        }

        private bool IsValidMove(Position destination)
        {
            var isValidMove = destination.XCoord >= 0 &&
                              destination.YCoord >= 0 &&
                              destination.XCoord < xCoord &&
                              destination.YCoord < yCoord;
            return isValidMove;
        }

        private Position GetDirectionBasedOnCurrentPosition(MoveDirection moveDirection)
        {
            var destinationDirection = default(Direction);
            if (moveDirection == MoveDirection.LEFT)
            {
                switch (currentPosition.Direction)
                {
                    case Direction.NORTH:
                        {
                            destinationDirection = Direction.WEST;
                            break;
                        }
                    case Direction.WEST:
                        {
                            destinationDirection = Direction.SOUTH;
                            break;
                        }
                    case Direction.SOUTH:
                        {
                            destinationDirection = Direction.EAST;
                            break;
                        }
                    case Direction.EAST:
                        {
                            destinationDirection = Direction.NORTH;
                            break;
                        }
                }
            }
            else
            {
                switch (currentPosition.Direction)
                {
                    case Direction.NORTH:
                        {
                            destinationDirection = Direction.EAST;
                            break;
                        }
                    case Direction.WEST:
                        {
                            destinationDirection = Direction.NORTH;
                            break;
                        }
                    case Direction.SOUTH:
                        {
                            destinationDirection = Direction.WEST;
                            break;
                        }
                    case Direction.EAST:
                        {
                            destinationDirection = Direction.SOUTH;
                            break;
                        }
                }
            }

            return new Position(currentPosition.XCoord, currentPosition.YCoord, destinationDirection);
        }
    }
}
