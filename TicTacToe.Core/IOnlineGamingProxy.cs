using System;

namespace TicTacToe.Core
{
    public interface IOnlineGamingProxy
    {
        Guid LogIn(string playerName);
        bool ReportScore(Guid sessionId, string gameName, int score);
        void LogOut(Guid sessionId);
    }
}