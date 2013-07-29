using System;
using NUnit.Framework;
using Ninject;
using TicTacToe.Core;

namespace TicTacToe.IntegrationTests
{
    //The tests in this class are designed to compliment the unit tests
    //by testing the integration between components of the application.
    //These components can be horizontal (other business domain service
    //classes the are enlisted to complete a task) or vertical (web 
    //services, databases, file systems, deep levels in the architecture,
    //etc.) In this case the only integration point is between my game
    //engine and the online proxy, so I don't care about testing the
    //various game conditions that should be covered by unit tests of
    //the game engine.
    public class GameEngineTests
    {
        private IGameEngine engine;

        [SetUp]
        public void SetupGameEngineForIntegrationTests()
        {
            var kernel = new StandardKernel(new TicTacToeModule());
            this.engine = kernel.Get<IGameEngine>();
        }

        [Test]
        public void ShouldBeAbleToPostHighScoreToOnlineGamingService()
        {
            //Arrange
            var score = 100;
            var playerName = "Bob";
            
            //Act
            var result = engine.SendHighScore(playerName, score);

            //Assert
            Assert.IsTrue(result);
        }
    }
}