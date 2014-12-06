using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;
using Color = System.Drawing.Color;

using System;
using System.IO;

namespace Teemo___Satan_Yordle
{
    class Program
    {
        private static Obj_AI_Hero Player { get { return ObjectManager.Player; } }
        private static Orbwalking.Orbwalker Orbwalker;
        private static Spell Q, W, E, R;

        private static Items.Item DFG, Botrk, Frostclaim, Youmuus, Hextech, Cutlass;


        public static Menu Menu;



        private static ShroomTables ShroomPositions;

        public const string VersionE = "1.0.2";

        static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += Game_OnGameLoad;
        }


        private static void Game_OnGameLoad(EventArgs args)
        {
            if (Player.ChampionName != "Teemo") //Satan
                return;


            Q = new Spell(SpellSlot.Q, 580);
            W = new Spell(SpellSlot.W);
            R = new Spell(SpellSlot.R, 230);

            Q.SetTargetted(0f, 2000f);
            R.SetSkillshot(0.1f, 75f, float.MaxValue, false, SkillshotType.SkillshotCircle);
          
            DFG = new Items.Item(3128, 750);
            Cutlass = new Items.Item(3144, 450);
            Hextech = new Items.Item(3146, 700);
            Frostclaim = new Items.Item(3092, 850);
            Botrk = new Items.Item(3153, 450);
            Youmuus = new Items.Item(3142, 650);



            //Base menu
            Menu = new Menu("Teemo Satan Yordle", "Teemo Satan Yordle", true);

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
            Menu.SubMenu("Combo").AddItem(new MenuItem("UseR", "Use R").SetValue(true));
            Menu.SubMenu("Combo").AddItem(new MenuItem("ComboActive", "Combo").SetValue(new KeyBind(32, KeyBindType.Press)));

            //Harass
            Menu.AddSubMenu(new Menu("Harass", "Harass"));
            Menu.SubMenu("Harass").AddItem(new MenuItem("UseQH", "Use Q").SetValue(true));
            Menu.SubMenu("Harass").AddItem(new MenuItem("UseWH", "Use W").SetValue(false));

            // Does harass need R? - decide it later
            Menu.SubMenu("Harass").AddItem(new MenuItem("HarassMana", "Only Harass if mana >").SetValue(new Slider(0, 0, 100)));
            Menu.SubMenu("Harass").AddItem(new MenuItem("HarassActive", "Harass").SetValue(new KeyBind("C".ToCharArray()[0], KeyBindType.Press)));
            Menu.SubMenu("Harass").AddItem(new MenuItem("HarassActiveT", "Harass (Toggle)").SetValue(new KeyBind("Y".ToCharArray()[0], KeyBindType.Toggle, true)));

            Menu.AddSubMenu(new Menu("Items", "Items"));
            Menu.SubMenu("Items").AddItem(new MenuItem("DFG1", "Deathfire Grasp").SetValue(true));
            Menu.SubMenu("Items").AddItem(new MenuItem("Cutlass1", "Bilgewater's Cutlass").SetValue(true));
            Menu.SubMenu("Items").AddItem(new MenuItem("Hextech1", "Hextech Gunblade").SetValue(true));
            Menu.SubMenu("Items").AddItem(new MenuItem("Frostclaim1", "Frost Queen's Claim").SetValue(true));
            Menu.SubMenu("Items").AddItem(new MenuItem("Botrk1", "Blade of the Ruined King").SetValue(true));
            Menu.SubMenu("Items").AddItem(new MenuItem("Youmuus1", "Youmuu's Ghostblade").SetValue(true));



            //R settings
            Menu.AddSubMenu(new Menu("R Settings", "Rsettings"));
            Menu.SubMenu("Rsettings").AddItem(new MenuItem("ShroomH", "Auto Use R on High Priorities").SetValue(true));
            Menu.SubMenu("Rsettings").AddItem(new MenuItem("ShroomM", "Auto Use R on Midium Priorities").SetValue(true));
            Menu.SubMenu("Rsettings").AddItem(new MenuItem("ShroomOn", "Auto Use R").SetValue(new StringList(new[] { "Always", "Only Combo", "No" }, 0)));



            var Misc = new Menu("Misc", "Misc");
            Misc.AddItem(new MenuItem("Packets", "Packet Casting").SetValue(false));
            Misc.AddItem(new MenuItem("GapQ", "Use Q for Gap Closer").SetValue(true));
            {
                var Emotes = new Menu("EmoteSpammer", "EmoteSpammer");
                Emotes.AddItem(new MenuItem("Type", "Spam Type").SetValue(new StringList(new[] { "Laugh", "Taunt", "Joke", "Off" }, 3)));
                Emotes.AddItem(new MenuItem("EmotePress", "EmoteSpam")).SetValue(new KeyBind(32, KeyBindType.Press));
         
                Emotes.AddItem(new MenuItem("EmoteD", "Spam Delay")).SetValue(new Slider(100, 1000, 1));


                Misc.AddSubMenu(Emotes);



            }

            Menu.AddSubMenu(Misc);





            //Drawings
            Menu.AddSubMenu(new Menu("Drawings", "Drawing"));
            Menu.SubMenu("Drawing").AddItem(new MenuItem("DrawQ", "Q Range").SetValue(new Circle(false, Color.Green)));
            Menu.SubMenu("Drawing").AddItem(new MenuItem("DrawQCD", "Q Cooldown").SetValue(new Circle(false, Color.DarkRed)));
            Menu.SubMenu("Drawing").AddItem(new MenuItem("DrawR", "R Range").SetValue(new Circle(false, Color.Green)));
            Menu.SubMenu("Drawing").AddItem(new MenuItem("DrawRCD", "R Cooldown").SetValue(new Circle(false, Color.DarkRed)));
            Menu.SubMenu("Drawing").AddItem(new MenuItem("ShroomH1", "Shroom High Priorities").SetValue(true));
            Menu.SubMenu("Drawing").AddItem(new MenuItem("ShroomM1", "Shroom Midium Priorities").SetValue(true));
            Menu.SubMenu("Drawing").AddItem(new MenuItem("ShroomV", "Shroom Vision Range").SetValue(new Slider(1500, 4000, 1000)));




            Menu.AddToMainMenu();


            Drawing.OnDraw += Drawing_OnDraw;
            Game.OnGameUpdate += Game_OnGameUpdate;
            AntiGapcloser.OnEnemyGapcloser += AntiGapcloser_OnEnemyGapcloser;

          


            Game.PrintChat("<font color=\"#33CC00\">Teemo</font> - Satan Yordle v" + VersionE + " By <font color=\"#0066FF\">E2Slayer</font>");

            Orbwalking.AfterAttack += Orbwalking_AfterAttack;

            ShroomPositions = new ShroomTables();


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


            if (R.IsReady() && Menu.Item("DrawR").GetValue<Circle>().Active)
            {
                Utility.DrawCircle(Player.Position, R.Range, Menu.Item("DrawR").GetValue<Circle>().Color);

            }

            if (!R.IsReady() && Menu.Item("DrawRCD").GetValue<Circle>().Active &&
                ObjectManager.Player.Spellbook.GetSpell(SpellSlot.R).Level > 0)
            {
                Utility.DrawCircle(Player.Position, R.Range, Menu.Item("DrawRCD").GetValue<Circle>().Color);
            }

            //  if (!ShroomOnC2) return;//Shroom drawing check
            // I think it isn't necessary. It was stopping drawing the shroom when player turn off Auto R


            // Shroom Drawing , Made by : LXMedia1 in UltimateCarry2
            // Fixed Drawing Shroom On/Off 

            if (Menu.Item("ShroomH1").GetValue<bool>())
            {
                foreach (
                    var pos in
                        ShroomPositions.HighPriority.Where(
                            shrom =>
                                shrom.Distance(ObjectManager.Player.Position) <=
                                Menu.Item("ShroomV").GetValue<Slider>().Value))
                {
                    Utility.DrawCircle(pos, 50, Color.Red);
                }
            }

            if (Menu.Item("ShroomM1").GetValue<bool>())
            {
                foreach (
                    var pos in
                        ShroomPositions.MediumPriority.Where(
                            shrom =>
                                shrom.Distance(ObjectManager.Player.Position) <=
                                Menu.Item("ShroomV").GetValue<Slider>().Value))
                {
                    Utility.DrawCircle(pos, 50, Color.Yellow);
                }
            }



        } // Drawing End

        /* 
         ========================
         * GameUpdate
         ========================
         */
        private static void Game_OnGameUpdate(EventArgs args)
        {
            var ShroomOnC = Menu.Item("ShroomOn").GetValue<StringList>().SelectedIndex;
            var TypeC = Menu.Item("Type").GetValue<StringList>().SelectedIndex;
            if (Player.IsDead)
                return;

            if (ShroomOnC == 0) // 0 is Shroom Auto On
            {
                AutoR();
            }



            if (Menu.Item("ComboActive").GetValue<KeyBind>().Active)
            {
                Combo();

            }


            else if (Menu.Item("HarassActive").GetValue<KeyBind>().Active ||
                     Menu.Item("HarassActiveT").GetValue<KeyBind>().Active)
            {
                Harass();
            }



            if (TypeC == 3) return;

            // spammer
           
            if (ObjectManager.Player.HasBuff("Recall")) return;


            {
                if ((Menu.Item("EmotePress").GetValue<KeyBind>().Active))
                {
                    SPAM();
                }
               
            }
        }


        /* 
       ========================
       * Emote Spammer  
      * Made by : TheKushStyle
         * github.com/TheKushStyle/LeagueSharp/tree/master/EmoteSpammer
       ========================
       */
        private static void SPAM()
        {
            var TypeC = Menu.Item("Type").GetValue<StringList>().SelectedIndex;

            if (TypeC == 0)
            {
                Packet.C2S.Emote.Encoded(new Packet.C2S.Emote.Struct(2)).Send();
               
            }
            else if (TypeC == 1)
            {
                Packet.C2S.Emote.Encoded(new Packet.C2S.Emote.Struct(1)).Send();
             
            }
            else if (TypeC == 2)
            {
                Packet.C2S.Emote.Encoded(new Packet.C2S.Emote.Struct(3)).Send();
      
            }
        }

        /* 
          ========================
          * R Auto 
         * Made by : LXMedia1 in UltimateCarry2
          ========================
          */
        private static void AutoR()
        {
            var PacketE = Menu.Item("Packets").GetValue<bool>();

            if (Menu.Item("ShroomH").GetValue<bool>())
                foreach (var place in ShroomPositions.HighPriority.Where(pos => pos.Distance(ObjectManager.Player.Position) <= R.Range && !IsShroomed(pos)))
                    R.Cast(place, PacketE);
            if (Menu.Item("ShroomM").GetValue<bool>())
                foreach (var place in ShroomPositions.MediumPriority.Where(pos => pos.Distance(ObjectManager.Player.Position) <= R.Range && !IsShroomed(pos)))
                    R.Cast(place, PacketE);

        }

        /* 
       ========================
       * Use Q to Gap-Closer
      * Probably needs some improvements, keep working
       ========================
       */

        private static void AntiGapcloser_OnEnemyGapcloser(ActiveGapcloser gapcloser)
        {
            var PacketE = Menu.Item("Packets").GetValue<bool>();


            if (!Menu.Item("GapQ").GetValue<bool>()) return;

            if (Q.IsReady() && gapcloser.Sender.IsValidTarget(Q.Range))//&& Player.Distance(gapcloser.Sender, true) <= 500 
            {

                Q.Cast(gapcloser.Sender, PacketE);

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
            if (Cutlass.IsReady() && Menu.Item("Cutlass1").GetValue<bool>())
            {
                Cutlass.Cast(TG);
            }
            if (Hextech.IsReady() && Menu.Item("Hextech1").GetValue<bool>())
            {

                Hextech.Cast(TG);
            }
            if (Frostclaim.IsReady() && Menu.Item("Frostclaim1").GetValue<bool>())
            {

                Frostclaim.Cast(TG);
            }
            if (Botrk.IsReady() && Menu.Item("Botrk1").GetValue<bool>())
            {

                Botrk.Cast(TG);
            }
            if (Youmuus.IsReady() && Menu.Item("Youmuus1").GetValue<bool>())
            {

                Youmuus.Cast();
            }

        }



        /* 
             
          
   ========================
   * AA reset
   ========================
   */
        private static void Orbwalking_AfterAttack(Obj_AI_Base unit, Obj_AI_Base target)
        {
            var ComA = Menu.Item("ComboActive").GetValue<KeyBind>().Active;
            var HarA = Menu.Item("HarassActive").GetValue<KeyBind>().Active;
            var HarAT = Menu.Item("HarassActiveT").GetValue<KeyBind>().Active;
            var PacketE = Menu.Item("Packets").GetValue<bool>();


            if ((ComA || HarA || HarAT) && unit.IsMe && (target is Obj_AI_Hero))
            {
                if (Menu.Item("UseQ").GetValue<bool>() && ComA && Q.IsReady())
                {
                    Q.Cast(target, PacketE);
                }


                else if (Menu.Item("UseQH").GetValue<bool>() && (HarA || HarAT) && Q.IsReady())
                {
                    if (Player.Mana / Player.MaxMana * 100 < Menu.Item("HarassMana").GetValue<Slider>().Value) return;
                    Q.Cast(target, PacketE);
                }
            }


        }

        /* 
    ========================
    * Combo / Harass
    ========================
    */
        private static void Combo()
        {
            var TG = SimpleTs.GetTarget(Q.Range, SimpleTs.DamageType.Physical);
            var PacketE = Menu.Item("Packets").GetValue<bool>();
            var TGW = SimpleTs.GetTarget(Orbwalking.GetRealAutoAttackRange(ObjectManager.Player) + 150, SimpleTs.DamageType.Physical);


            AutoR();

            if (TG == null) return;

            ItemUse();

            if (Menu.Item("UseW").GetValue<bool>() && R.IsReady())
            {
                if (TGW.Distance(ObjectManager.Player) >= Orbwalking.GetRealAutoAttackRange(ObjectManager.Player))
                    W.Cast();
            }

            if (Menu.Item("UseR").GetValue<bool>() && R.IsReady())
            {
                if (TG.IsValidTarget(R.Range))
                    R.Cast(TG.Position, PacketE);
            }

        }

        private static void Harass()
        {
            var TG = SimpleTs.GetTarget(Q.Range, SimpleTs.DamageType.Physical);
            var TGW = SimpleTs.GetTarget(Orbwalking.GetRealAutoAttackRange(ObjectManager.Player) + 150, SimpleTs.DamageType.Physical);
            var PacketE = Menu.Item("Packets").GetValue<bool>();

            if (Player.Mana / Player.MaxMana * 100 < Menu.Item("HarassMana").GetValue<Slider>().Value) return;

            if (Menu.Item("UseWH").GetValue<bool>() && R.IsReady())
            {
                if (TGW.Distance(ObjectManager.Player) >= Orbwalking.GetRealAutoAttackRange(ObjectManager.Player))
                    W.Cast();
            }



        }


        /* 
    ================================================================================================================================================
    * Shroom Pos
    * Made by : LXMedia1 in UltimateCarry2
    ================================================================================================================================================
    */


        private static bool IsShroomed(Vector3 position)
        {
            return ObjectManager.Get<Obj_AI_Base>().Where(obj => obj.Name == "Noxious Trap").Any(obj => position.Distance(obj.Position) <= 250);
        }

        internal class ShroomTables
        {
            public List<Vector3> HighPriority = new List<Vector3>();
            public List<Vector3> MediumPriority = new List<Vector3>();


            public ShroomTables()
            {
                CreateTables();
                var templist = (from pos in HighPriority
                                let x = pos.X
                                let y = pos.Y
                                let z = pos.Z
                                select new Vector3(x, z, y)).ToList();
                HighPriority = templist;
                templist = (from pos in MediumPriority
                            let x = pos.X
                            let y = pos.Y
                            let z = pos.Z
                            select new Vector3(x, z, y)).ToList();
                MediumPriority = templist;


            }

            /* 
  ========================================================================================================================================================================
             * ===========================================================================================================================================================
  */

            private void CreateTables()
            {
                //My own location.
                HighPriority.Add(new Vector3(10406, 50.08506f, 3050));
                HighPriority.Add(new Vector3(10202, -71.2406f, 4844));
                HighPriority.Add(new Vector3(11222, -2.869444f, 5592));
                HighPriority.Add(new Vector3(10032, 49.70721f, 6610));
                HighPriority.Add(new Vector3(8580, -50.36785f, 5560));
                HighPriority.Add(new Vector3(11960, 52.09994f, 7400));
                HighPriority.Add(new Vector3(4804, 40.283f, 8334));
                HighPriority.Add(new Vector3(6264, -62.41959f, 9332));
                HighPriority.Add(new Vector3(4724, -71.2406f, 10024));
                HighPriority.Add(new Vector3(3636, -8.188844f, 9348));
                HighPriority.Add(new Vector3(4452, 56.8484f, 11810));
                HighPriority.Add(new Vector3(2848, 51.84816f, 7362));

                MediumPriority.Add(new Vector3(10910, -26.55536f, 3456));
                MediumPriority.Add(new Vector3(11844, -67.9031f, 3902));
                MediumPriority.Add(new Vector3(9430, -71.2406f, 5664));
                MediumPriority.Add(new Vector3(10004, 51.98113f, 7218));
                MediumPriority.Add(new Vector3(10236, 49.54523f, 8794));
                MediumPriority.Add(new Vector3(12608, 51.69598f, 7278));
                MediumPriority.Add(new Vector3(11548, 52.21631f, 7656));
                MediumPriority.Add(new Vector3(12534, 51.7294f, 5160));
                MediumPriority.Add(new Vector3(11748, -63.7501f, 3202));
                MediumPriority.Add(new Vector3(7830, 51.69787f, 5726));
                MediumPriority.Add(new Vector3(8644, 52.32272f, 4836));
                MediumPriority.Add(new Vector3(6568, 48.527f, 4702));
                MediumPriority.Add(new Vector3(7408, 52.50325f, 2650));
                MediumPriority.Add(new Vector3(5514, 51.38131f, 3544));
                MediumPriority.Add(new Vector3(8398, -71.2406f, 6456));
                MediumPriority.Add(new Vector3(6552, -71.2406f, 8324));
                MediumPriority.Add(new Vector3(4636, 51.24505f, 6164));
                MediumPriority.Add(new Vector3(4778, 52.83177f, 7654));
                MediumPriority.Add(new Vector3(2212, 50.37255f, 7548));
                MediumPriority.Add(new Vector3(3272, 51.84087f, 7152));
                MediumPriority.Add(new Vector3(2304, 53.16499f, 9716));
                MediumPriority.Add(new Vector3(3944, -22.74386f, 11382));
                MediumPriority.Add(new Vector3(2794, 21.37915f, 11938));
                MediumPriority.Add(new Vector3(2978, -70.66211f, 11060));
                MediumPriority.Add(new Vector3(7056, 52.86944f, 9068));
                MediumPriority.Add(new Vector3(6208, 54.84456f, 10086));
                MediumPriority.Add(new Vector3(8264, 49.92609f, 10242));
                MediumPriority.Add(new Vector3(7304, 56.4768f, 12466));
                MediumPriority.Add(new Vector3(9310, 53.27245f, 11322));

                MediumPriority.Add(new Vector3(9196, 58.74224f, 2126));
                MediumPriority.Add(new Vector3(7044, 52.57567f, 3070));
                MediumPriority.Add(new Vector3(7754, 55.90973f, 11818));
                MediumPriority.Add(new Vector3(5666, 52.8381f, 12748));
                MediumPriority.Add(new Vector3(5344, -71.2406f, 9232));
            }
        }





    }// class end
}
