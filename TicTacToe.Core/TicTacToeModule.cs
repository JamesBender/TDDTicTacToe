using System;
using Ninject.Modules;

namespace TicTacToe.Core
{
    public class TicTacToeModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IOnlineGamingProxy>().To<OnlineGamingProxy>();
            Bind<IGameEngine>().To<GameEngine>();
        }
    }
}