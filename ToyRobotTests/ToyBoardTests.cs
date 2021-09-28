using NUnit.Framework;
using ToyRobot;

namespace ToyRobotTests
{
    public class Tests
    {
        private Board board;
        private string placeCommand;
        [SetUp]
        public void Setup()
        {
            board = new Board(6, 6);
            placeCommand = "PLACE 2,2,EAST";
        }

        [Test]
        public void Test_Place_Command_Should_Be_Called_First()
        {
            var execute = board.ExecuteCommand(CommandInterpreter.GetCommandFromString("LEFT"));
            Assert.IsFalse(execute.Result);
            execute = board.ExecuteCommand(CommandInterpreter.GetCommandFromString("RIGHT"));
            Assert.IsFalse(execute.Result);
            execute = board.ExecuteCommand(CommandInterpreter.GetCommandFromString("MOVE"));
            Assert.IsFalse(execute.Result);
            execute = board.ExecuteCommand(CommandInterpreter.GetCommandFromString(placeCommand));
            Assert.IsTrue(execute.Result);
        }

        [Test]
        public void Test_Place_Command_SecondTime_Without_Specifying_Direction_Should_Set_Initial_Direction()
        {
            board.ExecuteCommand(CommandInterpreter.GetCommandFromString(placeCommand));
            var execute = board.ExecuteCommand(CommandInterpreter.GetCommandFromString("PLACE 4,2"));
            Assert.IsTrue(execute.Result);
            Assert.AreEqual(Direction.EAST, board.currentPosition.Direction);
        }

        [Test]
        public void Test_Left_Command_When_Current_Direction_East_Should_Change_Direction_To_North()
        {
            board.ExecuteCommand(CommandInterpreter.GetCommandFromString(placeCommand));
            board.ExecuteCommand(CommandInterpreter.GetCommandFromString("LEFT"));
            Assert.AreEqual(Direction.NORTH, board.currentPosition.Direction);
        }

        [Test]
        public void Test_Right_Command_When_Current_Direction_East_Should_Change_Direction_To_South()
        {
            board.ExecuteCommand(CommandInterpreter.GetCommandFromString(placeCommand));
            board.ExecuteCommand(CommandInterpreter.GetCommandFromString("RIGHT"));
            Assert.AreEqual(Direction.SOUTH, board.currentPosition.Direction);
        }

        [Test]
        public void Test_Move_Command_When_Current_Direction_East_Should_Increase_XCoord_By_1()
        {
            board.ExecuteCommand(CommandInterpreter.GetCommandFromString(placeCommand));
            board.ExecuteCommand(CommandInterpreter.GetCommandFromString("MOVE"));
            Assert.AreEqual(3, board.currentPosition.XCoord);
            Assert.AreEqual(2, board.currentPosition.YCoord);
        }

        [Test]
        public void Test_Move_Command_When_Current_Direction_West_Should_Decrease_XCoord_By_1()
        {
            board.ExecuteCommand(CommandInterpreter.GetCommandFromString("PLACE 2,3,WEST"));
            board.ExecuteCommand(CommandInterpreter.GetCommandFromString("MOVE"));
            Assert.AreEqual(1, board.currentPosition.XCoord);
            Assert.AreEqual(3, board.currentPosition.YCoord);
        }

        [Test]
        public void Test_Move_Command_When_Current_Direction_North_Should_Increase_YCoord_By_1()
        {
            board.ExecuteCommand(CommandInterpreter.GetCommandFromString("PLACE 2,2,NORTH"));
            board.ExecuteCommand(CommandInterpreter.GetCommandFromString("MOVE"));
            Assert.AreEqual(2, board.currentPosition.XCoord);
            Assert.AreEqual(3, board.currentPosition.YCoord);
        }

        [Test]
        public void Test_Move_Command_When_Current_Direction_South_Should_Decrease_YCoord_By_1()
        {
            board.ExecuteCommand(CommandInterpreter.GetCommandFromString("PLACE 2,3,SOUTH"));
            board.ExecuteCommand(CommandInterpreter.GetCommandFromString("MOVE"));
            Assert.AreEqual(2, board.currentPosition.XCoord);
            Assert.AreEqual(2, board.currentPosition.YCoord);
        }

        [Test]
        public void Test_Move_Command_When_On_Border_Moving_South_Should_Error()
        {
            board.ExecuteCommand(CommandInterpreter.GetCommandFromString("PLACE 2,0,SOUTH"));
            var execute = board.ExecuteCommand(CommandInterpreter.GetCommandFromString("MOVE"));
            Assert.IsFalse(execute.Result);
            Assert.AreEqual(2, board.currentPosition.XCoord);
            Assert.AreEqual(0, board.currentPosition.YCoord);
        }

        [Test]
        public void Test_Move_Command_When_On_Border_Moving_North_Should_Error()
        {
            board.ExecuteCommand(CommandInterpreter.GetCommandFromString("PLACE 2,5,NORTH"));
            var execute = board.ExecuteCommand(CommandInterpreter.GetCommandFromString("MOVE"));
            Assert.IsFalse(execute.Result);
            Assert.AreEqual(2, board.currentPosition.XCoord);
            Assert.AreEqual(5, board.currentPosition.YCoord);
        }

        [Test]
        public void Test_Move_Command_When_On_Border_Moving_West_Should_Error()
        {
            board.ExecuteCommand(CommandInterpreter.GetCommandFromString("PLACE 0,5,WEST"));
            var execute = board.ExecuteCommand(CommandInterpreter.GetCommandFromString("MOVE"));
            Assert.IsFalse(execute.Result);
            Assert.AreEqual(0, board.currentPosition.XCoord);
            Assert.AreEqual(5, board.currentPosition.YCoord);
        }

        [Test]
        public void Test_Move_Command_When_On_Border_Moving_East_Should_Error()
        {
            board.ExecuteCommand(CommandInterpreter.GetCommandFromString("PLACE 5,5,EAST"));
            var execute = board.ExecuteCommand(CommandInterpreter.GetCommandFromString("MOVE"));
            Assert.IsFalse(execute.Result);
            Assert.AreEqual(5, board.currentPosition.XCoord);
            Assert.AreEqual(5, board.currentPosition.YCoord);
        }
    }
}