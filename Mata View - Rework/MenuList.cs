
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;
using Color = System.Drawing.Color;

namespace Mata_View___Rework
{
    class MenuList
    {
        public static Menu Menu;
        public static MenuItem Testsize;

        public MenuList()
        {
            Menu = new Menu("Mata View - Rework", "Mata View - Rework", true);
            var configs = (new Menu("Skills Timer Configs", "Skills Timer Configs"));
            var activeskill = (new MenuItem("activeskill", "Active All Skill Timer").SetValue(true));

            var textmenu = (new Menu("Text Configs", "Text Configs"));
            Testsize = (new MenuItem("textsize", "Text Size").SetValue(new Slider(16, 30, 10)));


            Menu.AddSubMenu(configs);
            Menu.SubMenu("Skills Timer Configs").AddItem(activeskill);

            Menu.AddSubMenu(textmenu);
            Menu.SubMenu("Text Configs").AddItem(Testsize);

            Menu.AddToMainMenu();
  
        }
    }
}
