#region

using LeagueSharp.Common;

#endregion

namespace Mata_View
{
    public class Menus
    {
        public static Menu Menu;
        public static MenuItem testsize;

        public static void Menuadd()
        {
            Menu = new Menu("Mata View", "Mata View", true);

            var configs = (new Menu("Skills Timer Configs", "Skills Timer Configs"));
            var enemyskill = (new MenuItem("enemyskill", "Show Enemies SKills Timer").SetValue(true));
            var allyskill = (new MenuItem("allyskill", "Show Allies Skills Timer").SetValue(true));
            var activeskill = (new MenuItem("activeskill", "Active Skill Timer").SetValue(true));

            var textmenu = (new Menu("Text Configs", "Text Configs"));
            testsize = (new MenuItem("textsize", "Text Size").SetValue(new Slider(16, 30, 10)));
       

            Menu.AddSubMenu(configs);
            Menu.SubMenu("Skills Timer Configs").AddItem(enemyskill);
            Menu.SubMenu("Skills Timer Configs").AddItem(allyskill);
            Menu.SubMenu("Skills Timer Configs").AddItem(activeskill);

            Menu.AddSubMenu(textmenu);
            Menu.SubMenu("Text Configs").AddItem(testsize);

            Menu.AddToMainMenu();
        }
    }
}
