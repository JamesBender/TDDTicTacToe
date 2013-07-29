using System;
using System.Linq;

namespace TicTacToe.Core
{
    public class GameEngine : IGameEngine
    {
        private readonly IOnlineGamingProxy onlineGamingProxy;
        private Guid sessionKey;

        private const string GameName = "Tic Tac Toe";

        public GameEngine(IOnlineGamingProxy gamingProxy)
        {
            onlineGamingProxy = gamingProxy;
        }

        public bool SendHighScore(string playerName, int score)
        {
            if (sessionKey == null || sessionKey == Guid.Empty)
            {
                sessionKey = onlineGamingProxy.LogIn(playerName);
            }

            var result = onlineGamingProxy.ReportScore(sessionKey, GameName, score);
            return result;
        }

        public string GetWinner(string[,] board)
        {
            string returnValue;

            returnValue = CheckBoardColumnsForWinner(board);

            if (!string.IsNullOrEmpty(returnValue))
                return returnValue;

            returnValue = CheckBoardRowsForWinner(board);
            if (!string.IsNullOrEmpty(returnValue))
                return returnValue;

            var hasThreeInARowFromTopLeftToBottomRight = (board[0, 0] != string.Empty && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2]);
            var hasThreeInARowFromTopRightToBottomLeft = (board[0, 2] != string.Empty && board[0, 2] == board[1, 1] && board[2, 0] == board[1, 1]);
            if (hasThreeInARowFromTopLeftToBottomRight || hasThreeInARowFromTopRightToBottomLeft)
            {
                return board[1, 1];
            }

            return String.Empty;
        }

        private string CheckBoardRowsForWinner(string[,] board)
        {
            for (var rowIndex = 0; rowIndex < 3; rowIndex++)
            {
                if (board[0, rowIndex] != String.Empty && board[0, rowIndex] == board[1, rowIndex] && board[1, rowIndex] == board[2, rowIndex])
                {
                    return board[0, rowIndex];
                }
            }
            return string.Empty;
        }

        private string CheckBoardColumnsForWinner(string[,] board)
        {
            for (var columnIndex = 0; columnIndex < 3; columnIndex++)
            {
                if (board[columnIndex, 0] != String.Empty && board[columnIndex, 0] == board[columnIndex, 1] && board[columnIndex, 1] == board[columnIndex, 2])
                {
                    return board[columnIndex, 0];
                }
            }
            return string.Empty;
        }
    }
}
