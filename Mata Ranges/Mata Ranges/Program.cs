#region

using System;
using LeagueSharp;
using LeagueSharp.Common;

#endregion


namespace Mata_Ranges
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += Game_OnGameLoad;
        }

        private static void Game_OnGameLoad(EventArgs args)
        {
            Ranges.RangesMenu();
            Chat("1.0.0.0");
        }

        private static void Chat(string message)
        {
            Game.PrintChat("<font color=\"#4EE2EC\">Mata Ranges</font> - " + message + " v - <font color=\"#B6B6B4\">E2Slayer</font>");
        }

    }
}
