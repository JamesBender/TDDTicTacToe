using System;
using System.Threading;

namespace TicTacToe.Core
{
    public class OnlineGamingProxy : IOnlineGamingProxy
    {
        private Guid currentSessionId;

        public Guid LogIn(string playerName)
        {
            Thread.Sleep(2000);
            currentSessionId = Guid.NewGuid();
            return currentSessionId;
        }

        public bool ReportScore(Guid sessionId, string gameName, int score)
        {
            Thread.Sleep(5000);
            if (sessionId == currentSessionId)
            {
                return true;
            }
            return false;
        }

        public void LogOut(Guid sessionId)
        {
            Thread.Sleep(1000);
            return;
        }
    }
}