using System;

namespace TicTacToe.Core
{
    public interface IGameEngine
    {
        bool SendHighScore(string playerName, int score);

        string GetWinner(string[,] board);
    }
}