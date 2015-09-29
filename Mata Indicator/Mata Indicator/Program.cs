#region

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

#endregion

namespace Mata_Indicator
{
    class Program
    {
        private static readonly Obj_AI_Hero Player = ObjectManager.Player;
        public static Menu Menu = new Menu("Mata Indicator", "Mata Indicator", true);
        public static Menu Champ = new Menu(Player.ChampionName, Player.ChampionName);
        public static readonly Dictionary<int, bool> Slotcheck = new Dictionary<int, bool>();
        private static readonly string[] ButtonKey = new string[] { "Y", "U", "H","J"};

        public static Spell Q { get; private set; }
        public static Spell W { get; private set; }
        public static Spell E { get; private set; }
        public static Spell R { get; private set; }

        static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += Game_OnGameLoad;
        }

        public static void Game_OnGameLoad(EventArgs args)
        {
            try
            {
                Q = new Spell(SpellSlot.Q);
                W = new Spell(SpellSlot.W);
                E = new Spell(SpellSlot.E);
                R = new Spell(SpellSlot.R);


                MenuHandlerRun();
                Game.PrintChat("<font color=\"#4EE2EC\">Mata Indicator</font> 1.0.0.0v [Beta] - <font color=\"#B6B6B4\">E2Slayer</font>");

                Drawing.OnDraw += Drawing_OnDraw;
                Game.OnUpdate += Game_OnGameUpdate;
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
        }

        private static void Game_OnGameUpdate(EventArgs args)
        {
            Editor(1);
            Editor(2);
            Editor(3);
            Editor(4);
        }

        private static void Editor(int slotnumber)
        {
            if (Champ.Item("Editor" + slotnumber + Player.ChampionName).GetValue<KeyBind>().Active)
            {
                var dis = (int)Player.Distance(Game.CursorPos);
                Champ.Item("Range" + slotnumber + Player.ChampionName).SetValue(new Slider(dis, 100, 2000));
                Champ.Item("Angle" + slotnumber + Player.ChampionName).SetValue(new Slider(dis, 0, 180));
            }
        }


        private static void Drawing_OnDraw(EventArgs args)
        {
            
            foreach (var slot in Slotcheck)
            {
                if (Champ.Item("Enable" + slot.Key + Player.ChampionName).GetValue<bool>())
                {
                    var bindspell = Champ.Item("Bindspell" + slot.Key + Player.ChampionName).GetValue<StringList>().SelectedIndex;
                switch (bindspell)
                {
                    case 0: //None
                        if (Menu.Item("Type" + slot.Key + Player.ChampionName).GetValue<StringList>().SelectedIndex == 0)
                        {
                            Render.Circle.DrawCircle(ObjectManager.Player.Position, Champ.Item("Range" + slot.Key + Player.ChampionName).GetValue<Slider>().Value, Champ.Item("Color" + slot.Key + Player.ChampionName).GetValue<Color>(), Champ.Item("Width" + slot.Key + Player.ChampionName).GetValue<Slider>().Value);
                        }
                        else
                        {
                            var coneAngle = Champ.Item("Angle" + slot.Key + Player.ChampionName).GetValue<Slider>().Value;
                            var abilRange = Champ.Item("Range" + slot.Key + Player.ChampionName).GetValue<Slider>().Value;
                            var drawColor = Champ.Item("Color" + slot.Key + Player.ChampionName).GetValue<Color>();
                            var lineWidth = Champ.Item("Width" + slot.Key + Player.ChampionName).GetValue<Slider>().Value;
                           var line = new Geometry.Polygon.Line(ObjectManager.Player.Position, Game.CursorPos, abilRange);
                             var direction = ObjectManager.Player.Direction.To2D().Perpendicular();
                            Render.Circle.DrawCircle(ObjectManager.Player.Position, Champ.Item("Range" + slot.Key + Player.ChampionName).GetValue<Slider>().Value, Champ.Item("Color" + slot.Key + Player.ChampionName).GetValue<Color>(), Champ.Item("Width" + slot.Key + Player.ChampionName).GetValue<Slider>().Value);
                            line.Draw(drawColor,lineWidth);
                      //      var currentAngel = coneAngle * (float) Math.PI / 180;
           //                 var conePoint1 = ObjectManager.Player.Position + abilRange * direction.Rotated(currentAngel);
                         //  currentAngel = (360-coneAngle) * (float) Math.PI / 180;
                         //   var conePoint2 = ObjectManager.Player.Position + abilRange * direction.Rotated(currentAngel);
                        }
                        break;

                    case 1: //Q
                         if (Q.IsReady())
                             goto case 0;
                         break;
                    case 2: //W
                        if (W.IsReady())
                            goto case 0;
                        break;
                    case 3: //E
                        if (E.IsReady())
                            goto case 0;
                        break;
                    case 4: //R
                        if (R.IsReady())
                            goto case 0;
                        break;
                }  
                }
      
               
            }
        }


        public static void MenuHandlerRun()
        {
            Menu.AddSubMenu(Champ);
            for (var i = 1; i < 5; i++)
            {
                SlotCreate(i);
            }
            Menu.AddToMainMenu();
        }

        public static void SlotCreate(int number)
        {

            var slot1 = (new Menu("Indicator Slot " + number , "Slot " + number + Player.ChampionName));
            var slot1Type = (new MenuItem("Type" + number + Player.ChampionName, "Drawing Type").SetValue(new StringList(new[] { "Circle", "Line" })));
            var slot1Color = (new MenuItem("Color" + number + Player.ChampionName, "Color").SetValue(Color.Lime));
            var slot1Range = new MenuItem("Range" + number + Player.ChampionName, "Range").SetValue(new Slider(100, 100, 2000));
            var btkey = ButtonKey.GetValue(number - 1);
            var slot1Editor = (new MenuItem("Editor" + number + Player.ChampionName, "Range Editor").SetValue(new KeyBind(btkey.ToString().ToCharArray()[0], KeyBindType.Press)));
            var slot1Width = new MenuItem("Width" + number + Player.ChampionName, "Width").SetValue(new Slider(5, 10, 1));
            var slot1Bind = (new MenuItem("Bindspell" + number + Player.ChampionName, "Bind Spell").SetValue(new StringList(new[] { "None", "Q", "W", "E", "R" })));
            var slot1Enable = (new MenuItem("Enable" + number + Player.ChampionName, "Enable").SetValue(false));

            Champ.AddSubMenu(slot1);
            Champ.SubMenu("Slot " + number + Player.ChampionName).AddItem(slot1Type);
            Champ.SubMenu("Slot " + number + Player.ChampionName).AddItem(slot1Color);
            Champ.SubMenu("Slot " + number + Player.ChampionName).AddItem(slot1Range);
            Champ.SubMenu("Slot " + number + Player.ChampionName).AddItem(slot1Editor);
            Champ.SubMenu("Slot " + number + Player.ChampionName).AddItem(slot1Width);
            Champ.SubMenu("Slot " + number + Player.ChampionName).AddItem(slot1Bind);
            Champ.SubMenu("Slot " + number + Player.ChampionName).AddItem(slot1Enable);

            Slotcheck.Add(number, Menu.Item("Enable" + number + Player.ChampionName).GetValue<bool>());
        }
    }
}
