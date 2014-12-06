using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;
using Color = System.Drawing.Color;

namespace Zac____Jumping_Jelly
{
    class Program
    {
        private static Obj_AI_Hero Player { get { return ObjectManager.Player; } }
        public static Orbwalking.Orbwalker Orbwalker;
        public static Spell Q, W, E, R;

        public static Items.Item DFG;


        public static Menu Menu;

        public const string VersionE = "1.0.0";

        private static int ch5 = 0; // for check E range 
        static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += Game_OnGameLoad;
        }

        private static void Game_OnGameLoad(EventArgs args)
        {
            if (Player.ChampionName != "Zac") 
                return;


            Q = new Spell(SpellSlot.Q, 550);
            W = new Spell(SpellSlot.W, 350);
            E = new Spell(SpellSlot.E, 1150);
            R = new Spell(SpellSlot.R, 300); // In objectmanger said "850", but I don't know why 850. I changed to 300

            Q.SetSkillshot(0.5f, 120f, 902, true, SkillshotType.SkillshotLine);
          //  E.SetCharged("ZacE", "ZacE", 0, 1550, 1.5f);

             // W.SetSkillshot(0.5f, 350, 1600, true); // I don't think It is neccesarry
            // R.SetSkillshot(0.5f, 1800, 1800, true, SkillshotType.SkillshotCone);


            //  DFG = Utility.Map.GetMap()._MapType == Utility.Map.MapType.TwistedTreeline ? new Items.Item(3188, 750) : new Items.Item(3128, 750);
            DFG = new Items.Item(3128, 750);

            //Base menu
            Menu = new Menu("Zac - Jumping Jelly", "Zac - Jumping Jelly", true);

            //SimpleTs
            Menu.AddSubMenu(new Menu("Target Selector", "Target Selector"));
            SimpleTs.AddToMenu(Menu.SubMenu("Target Selector"));

            //Orbwalker
            Menu.AddSubMenu(new Menu("Orbwalker", "Orbwalker"));
            new Orbwalking.Orbwalker(Menu.SubMenu("Orbwalker"));

            //Combo
            Menu.AddSubMenu(new Menu("Combo", "Combo"));
            Menu.SubMenu("Combo").AddItem(new MenuItem("UseQ", "Use Q").SetValue(true));
            Menu.SubMenu("Combo").AddItem(new MenuItem("UseW", "Use W").SetValue(true));
           // Menu.SubMenu("Combo").AddItem(new MenuItem("UseRS", "Only R if Enenmies >").SetValue(new Slider(0, 0, 6)));
            Menu.SubMenu("Combo").AddItem(new MenuItem("ComboActive", "Combo").SetValue(new KeyBind(32, KeyBindType.Press)));

            //Harass
            Menu.AddSubMenu(new Menu("Harass", "Harass"));
            Menu.SubMenu("Harass").AddItem(new MenuItem("UseQH", "Use Q").SetValue(true));
            Menu.SubMenu("Harass").AddItem(new MenuItem("UseWH", "Use W").SetValue(false));

            // dd
            Menu.SubMenu("Harass").AddItem(new MenuItem("HarassHealth", "Only Harass if health >").SetValue(new Slider(0, 0, 100)));
            Menu.SubMenu("Harass").AddItem(new MenuItem("HarassActive", "Harass").SetValue(new KeyBind("C".ToCharArray()[0], KeyBindType.Press)));
            Menu.SubMenu("Harass").AddItem(new MenuItem("HarassActiveT", "Harass (Toggle)").SetValue(new KeyBind("Y".ToCharArray()[0], KeyBindType.Toggle, true)));

 
            Menu.AddSubMenu(new Menu("Items", "Items"));
            Menu.SubMenu("Items").AddItem(new MenuItem("DFG1", "Deathfire Grasp").SetValue(true));




            //Misc
              Menu.AddSubMenu(new Menu("Misc", "Misc"));
               Menu.SubMenu("Misc").AddItem(new MenuItem("Packets", "Packet Casting").SetValue(false));
               //Menu.SubMenu("Misc").AddItem(new MenuItem("GapQ", "Use Q for Gap Closer").SetValue(true));







            //Drawings
            Menu.AddSubMenu(new Menu("Drawings", "Drawing"));
            Menu.SubMenu("Drawing").AddItem(new MenuItem("DrawQ", "Q Range").SetValue(new Circle(false, Color.Green)));
            Menu.SubMenu("Drawing").AddItem(new MenuItem("DrawQCD", "Q Cooldown").SetValue(new Circle(false, Color.DarkRed)));
            Menu.SubMenu("Drawing").AddItem(new MenuItem("DrawW", "W Range").SetValue(new Circle(false, Color.Green)));
            Menu.SubMenu("Drawing").AddItem(new MenuItem("DrawWCD", "W Cooldown").SetValue(new Circle(false, Color.DarkRed)));
            Menu.SubMenu("Drawing").AddItem(new MenuItem("DrawE", "E Range").SetValue(new Circle(false, Color.Green)));
            Menu.SubMenu("Drawing").AddItem(new MenuItem("DrawECD", "E Cooldown").SetValue(new Circle(false, Color.DarkRed)));
            Menu.SubMenu("Drawing").AddItem(new MenuItem("DrawR", "R Range").SetValue(new Circle(false, Color.Green)));
            Menu.SubMenu("Drawing").AddItem(new MenuItem("DrawRCD", "R Cooldown").SetValue(new Circle(false, Color.DarkRed)));





            Menu.AddToMainMenu();

         
            Drawing.OnDraw += Drawing_OnDraw;
            Game.OnGameUpdate += Game_OnGameUpdate;

          

            //   Game.OnGameSendPacket += PacketHandler; //for emote delay
            //  Drawing.OnEndScene += Drawing_OnEndScene;


            Game.PrintChat("<font color=\"#33CC00\">Zac</font> - Jumping Jelly v" + VersionE + " By <font color=\"#0066FF\">E2Slayer</font>");

        //    Orbwalking.AfterAttack += Orbwalking_AfterAttack;

        

        }



        /* 
         ========================
         * Drawing Part 
         ========================
         */



        private static void Drawing_OnDraw(EventArgs args)
        {

            if (Player.IsDead)
                return;
    

            if (Q.IsReady() && Menu.Item("DrawQ").GetValue<Circle>().Active)
            {
                Utility.DrawCircle(Player.Position, Q.Range, Menu.Item("DrawQ").GetValue<Circle>().Color);

            }

            if (!Q.IsReady() && Menu.Item("DrawQCD").GetValue<Circle>().Active &&
                ObjectManager.Player.Spellbook.GetSpell(SpellSlot.Q).Level > 0)
            {
                Utility.DrawCircle(Player.Position, Q.Range, Menu.Item("DrawQCD").GetValue<Circle>().Color);
            }

            if (W.IsReady() && Menu.Item("DrawW").GetValue<Circle>().Active)
            {
                Utility.DrawCircle(Player.Position, W.Range, Menu.Item("DrawW").GetValue<Circle>().Color);

            }

            if (!W.IsReady() && Menu.Item("DrawWCD").GetValue<Circle>().Active &&
                ObjectManager.Player.Spellbook.GetSpell(SpellSlot.W).Level > 0)
            {
                Utility.DrawCircle(Player.Position, W.Range, Menu.Item("DrawWCD").GetValue<Circle>().Color);
            }
            if (E.IsReady() && Menu.Item("DrawE").GetValue<Circle>().Active)
            {
                Utility.DrawCircle(Player.Position, E.Range, Menu.Item("DrawW").GetValue<Circle>().Color);

            }

            if (!E.IsReady() && Menu.Item("DrawQCD").GetValue<Circle>().Active &&
                ObjectManager.Player.Spellbook.GetSpell(SpellSlot.E).Level > 0)
            {
                Utility.DrawCircle(Player.Position, E.Range, Menu.Item("DrawECD").GetValue<Circle>().Color);
            }


            if (R.IsReady() && Menu.Item("DrawR").GetValue<Circle>().Active)
            {
                Utility.DrawCircle(Player.Position, R.Range, Menu.Item("DrawR").GetValue<Circle>().Color);

            }

            if (!R.IsReady() && Menu.Item("DrawRCD").GetValue<Circle>().Active &&
                ObjectManager.Player.Spellbook.GetSpell(SpellSlot.R).Level > 0)
            {
                Utility.DrawCircle(Player.Position, R.Range, Menu.Item("DrawRCD").GetValue<Circle>().Color);
            }




        } // Drawing End

        /* 
         ========================
         * GameUpdate
         ========================
         */
        private static void Game_OnGameUpdate(EventArgs args)
        {
          

     
          if (ch5 == 0)
            {
                var lv = (Player.Spellbook.GetSpell(SpellSlot.E).Level - 1)*100; //Zac E different level Range         
                E = new Spell(SpellSlot.E, 1150 + lv);
                E.SetCharged("ZacE", "ZacE", 0, 1150 + lv, 1.5f);
                E.SetSkillshot(0.5f, (float)(80 * Math.PI / 180), 1500, true, SkillshotType.SkillshotCone);
           
               
                if (Player.Spellbook.GetSpell(SpellSlot.E).Level == 5)
                {
                    ch5 = 1; // end
                }
            }


            if (Player.IsDead)
                return;

           ItemUse();
      

            if (Menu.Item("ComboActive").GetValue<KeyBind>().Active)
            {

                Combo();

            }


            else if (Menu.Item("HarassActive").GetValue<KeyBind>().Active ||
                     Menu.Item("HarassActiveT").GetValue<KeyBind>().Active)
            {
               Harass();
              


            }

       


        }

      
        /* 
       ========================
       * Item Usage 
      * Item code from Kurisu's source
       ========================
       */



        private static void ItemUse()
        {

            var TG = SimpleTs.GetTarget(Q.Range, SimpleTs.DamageType.Physical);

            if (TG == null) return;

            if (DFG.IsReady() && Menu.Item("DFG1").GetValue<bool>())
            {

                DFG.Cast(TG);
            }


        }

        /* 
   
    ========================
    * Combo / Harass
    ========================
    */
        private static void Combo()
        {
            var TG = SimpleTs.GetTarget(Q.Range, SimpleTs.DamageType.Magical);
            var TGW = SimpleTs.GetTarget(W.Range, SimpleTs.DamageType.Magical);

            var PacketE = Menu.Item("Packets").GetValue<bool>();
      
 
            
 
            
            if (Menu.Item("UseQ").GetValue<bool>() && Q.IsReady())
            {

                
                if (TG == null) return;
                Q.Cast(TG.Position, PacketE);
                
            }

            if (Menu.Item("UseW").GetValue<bool>() && W.IsReady())
            {

               
               if (TGW == null) return;
                W.Cast();

            }

           

        }

        private static void Harass()
        {
            var TG = SimpleTs.GetTarget(Q.Range, SimpleTs.DamageType.Magical);
            var TGW = SimpleTs.GetTarget(W.Range, SimpleTs.DamageType.Magical);
            var PacketE = Menu.Item("Packets").GetValue<bool>();

            if (Player.Health / Player.MaxHealth * 100 < Menu.Item("HarassHealth").GetValue<Slider>().Value) return;

            if (Menu.Item("UseQH").GetValue<bool>() && Q.IsReady())
            {
                if (TG == null) return;
                Q.Cast(TG, PacketE);
            }

            if (Menu.Item("UseWH").GetValue<bool>() && W.IsReady())
            {
                
                if (TGW == null) return;
                W.Cast();
            }



        }


        
    }
}
