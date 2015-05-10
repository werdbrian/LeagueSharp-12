
using System;
using System.Collections.Generic;
using LeagueSharp.Common;

namespace Mata_View___Rework
{
    class Program
    {
        public static List<String> CurrentChampions = new List<String>();

        private static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += Game_OnGameLoad;
        }

        private static void Game_OnGameLoad(EventArgs args)
        {
            Console.WriteLine("Mata View - Rework is Loaded 2.0.0.0 [May, 9]");
            var menu = new MenuList();
            var skill = new SkillsList();
            var detect = new DetectMain();
        }
    }
}
