using System;
using System.Linq;
using NUnit.Framework;
using Telerik.JustMock;
using TicTacToe.Core;

namespace TicTacToe.UnitTests
{
    //This class contains the unit tests. These test JUST the unit
    //of work and are designed to isolate the other parts of the
    //application from the unit of code under test.
    [TestFixture]
    class GameEngineTests
    {
        private IGameEngine engine;
        private string[,] board;
        private IOnlineGamingProxy onlineGamingProxyMock;

        [SetUp]
        public void SetupTests()
        {
            this.onlineGamingProxyMock = Mock.Create<IOnlineGamingProxy>();
            engine = new GameEngine(onlineGamingProxyMock);
            board = new string[3, 3] { { String.Empty, String.Empty, String.Empty }, { String.Empty, String.Empty, String.Empty }, { String.Empty, String.Empty, String.Empty } };
        }

        [Test]
        public void WhenBoardIsEmptyThereIsNoWinnerButGameIsNotOver()
        {           
            var result = engine.GetWinner(board);

            Assert.AreEqual(String.Empty, result);
        }

        //The tests that check for winners in the rows demonstrate some techniques
        //that can be used when you have test cases that have similar mechanics,
        //but have data that varies. The approach used in the row examples are
        //good if your unit test framework does not support "row tests" or test 
        //cases, as shown below. If your frameword does support these constructs,
        //you should favor them over this approach.
        private string PerformTicTacToeGameBoardRowTest(int gameboardRow, string expectedToken)
        {
            board[gameboardRow, 0] = expectedToken;
            board[gameboardRow, 1] = expectedToken;
            board[gameboardRow, 2] = expectedToken;

            var result = engine.GetWinner(board);
            return result;
        }
        
        [Test]
        public void WhenXIsInAllTopRowThenXWins()
        {
            var expectedToken = "X";
            var gameboardRow = 0;

            var result = PerformTicTacToeGameBoardRowTest(gameboardRow, expectedToken);

            Assert.AreEqual(expectedToken, result);
        }
  
        [Test]
        public void WhenOIsInAllTopRowThenOWins()
        {
            var expectedToken = "O";
            var gameboardRow = 0;

            var result = PerformTicTacToeGameBoardRowTest(gameboardRow, expectedToken);

            Assert.AreEqual(expectedToken, result);
        }

        public void WhenXIsInAllMiddleRowThenXWins()
        {
            var expectedToken = "X";
            var gameboardRow = 1;

            var result = PerformTicTacToeGameBoardRowTest(gameboardRow, expectedToken);

            Assert.AreEqual(expectedToken, result);
        }

        [Test]
        public void WhenOIsInAllMiddleRowThenOWins()
        {
            var expectedToken = "O";
            var gameboardRow = 1;

            var result = PerformTicTacToeGameBoardRowTest(gameboardRow, expectedToken);

            Assert.AreEqual(expectedToken, result);
        }

        public void WhenXIsInAllBottomRowThenXWins()
        {
            var expectedToken = "X";
            var gameboardRow = 2;

            var result = PerformTicTacToeGameBoardRowTest(gameboardRow, expectedToken);

            Assert.AreEqual(expectedToken, result);
        }

        [Test]
        public void WhenOIsInAllBottomRowThenOWins()
        {
            var expectedToken = "O";
            var gameboardRow = 2;

            var result = PerformTicTacToeGameBoardRowTest(gameboardRow, expectedToken);

            Assert.AreEqual(expectedToken, result);
        }
        
        //The test that check for winners in columns use the "row test" or test case 
        //method. In this case you have a series of test where the mechanics are the
        //same and your data varies. You can define a series of test cases using the
        //TestCase attribute and supply what is changing. The test method will 
        //execute N times to complete the tests. As far as the test runner is 
        //concernced this results in N distinct tests. (well, the nUnit and JustCode 
        //test runners at least, I didn't check any others).
        [TestCase(0, "X")]
        [TestCase(1, "X")]
        [TestCase(2, "X")]
        [TestCase(0, "O")]
        [TestCase(1, "O")]
        [TestCase(2, "O")]
        public void WhenTheSameTokeIsInEveryFieldOfAColumnThenThatTokenWins(int rowIdx, string token)
        {
            board[rowIdx, 0] = token;
            board[rowIdx, 1] = token;
            board[rowIdx, 2] = token;

            var result = engine.GetWinner(board);

            Assert.AreEqual(token, result);
        }

        //Of course, sometimes a little brute-force is easier. :)
        private string SetBoardWithTokenForDiagonalFromTopLeftToBottomRight(string expectedToken)
        {
            board[0, 0] = expectedToken;
            board[1, 1] = expectedToken;
            board[2, 2] = expectedToken;

            var result = engine.GetWinner(board);
            return result;
        }

        [Test]
        public void WhenXHasAllCellsDiagonallyFromTopLeftToBottomRightThanXWins()
        {
            var expectedToken = "X";

            var result = SetBoardWithTokenForDiagonalFromTopLeftToBottomRight(expectedToken);

            Assert.AreEqual(expectedToken, result);
        }
  
        [Test]
        public void WhenOHasAllCellsDiagonallyFromTopLeftToBottomRightThanOWins()
        {
            var expectedToken = "O";

            var result = SetBoardWithTokenForDiagonalFromTopLeftToBottomRight(expectedToken);

            Assert.AreEqual(expectedToken, result);
        }

        private string SetBoardWithTokenForDiagonalFromTopRightToBottomLeft(string expectedToken)
        {
            board[0, 2] = expectedToken;
            board[1, 1] = expectedToken;
            board[2, 0] = expectedToken;

            var result = engine.GetWinner(board);
            return result;
        }

        [Test]
        public void WhenXHasAllCellsDiagonallyFromTopRightToBottomLeftThanXWins()
        {
            var expectedToken = "X";

            var result = SetBoardWithTokenForDiagonalFromTopRightToBottomLeft(expectedToken);

            Assert.AreEqual(expectedToken, result);
        }
  
        [Test]
        public void WhenOHasAllCellsDiagonallyFromTopRightToBottomLeftThanOWins()
        {
            var expectedToken = "O";

            var result = SetBoardWithTokenForDiagonalFromTopRightToBottomLeft(expectedToken);

            Assert.AreEqual(expectedToken, result);
        }

        [Test]
        public void ShouldBeAbleToPostHighScoreToOnlineGamingService()
        {
            //Arrange
            var score = 100;
            var playerName = "Bob";
            var sessionId = Guid.NewGuid();

            Mock.Arrange(() => onlineGamingProxyMock.LogIn(playerName)).Returns(sessionId);
            Mock.Arrange(() => onlineGamingProxyMock.ReportScore(sessionId, Arg.IsAny<string>(), score)).Returns(true).MustBeCalled();

            //Act
            var result = engine.SendHighScore(playerName, score);

            //Assert
            Assert.IsTrue(result);
            Mock.Assert(onlineGamingProxyMock);
        }
    }
}
