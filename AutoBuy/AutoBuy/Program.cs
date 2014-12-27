#region

using System;
using System.Linq;
using LeagueSharp;
using LeagueSharp.Common;

#endregion

namespace AutoBuy
{
    class Program
    {
        public static Menu Menu;
        private static Obj_AI_Hero Player { get { return ObjectManager.Player; } }
        public static int Buyc = 0, Buyc2 = 0, Buyc3 = 0; //buyc is For Global setting, buyc2 is different settings for each champ , buyc3 is for trinket
        public static int Value1 = 17, Value2 = 2, Value3 = 0, Value4 = 0, Value5 = 0, Value6 = 0, ValueT = 3, ValueTR = 3;
        public static int HasSmite = 0;
        public static int Setting1, Setting2, Setting3, Setting4, Setting5, Setting6, SettingT, SettingTR, GSetting;
        public const string VersionE = "1.0.2";

        public static SpellSlot smiteSlot = SpellSlot.Unknown;
        public static Spell smite;

        static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += Game_OnGameLoad;
            
        }


        public static void Menuadd2()
        {
            var championName2 = ObjectManager.Player.ChampionName.ToLowerInvariant();


            Menu.AddSubMenu(new Menu(championName2, "Autobuy"));
            Menu.SubMenu("Autobuy").AddItem(new MenuItem("Item1" + championName2, "ItemSlot 1").SetValue(new StringList(new[] { "Doran's Blade", "Doran's Ring", "Doran's Shield", "Crystalline Flask", "Hunter's Machete", "Cloth Armor"/*5*/, "Boots of Speed", "Long Sword", "Ancient Coin", "Relic Shield", "Spellthief's Edge"/*10*/, "Dagger", "Brawler's Gloves", "Amplifying Tome", "Sapphire Crystal", "Faerie Charm"/*15*/, "Rejuvenation Bead", "OFF" }, Value1)));
    
            Menu.SubMenu("Autobuy").AddItem(new MenuItem("Item2" + championName2, "ItemSlot 2").SetValue(new StringList(new[] { "Faerie Charm", "Rejuvenation Bead", "OFF" }, Value2)));
            Menu.SubMenu("Autobuy").AddItem(new MenuItem("Item3" + championName2, "Health Potions").SetValue(new Slider(Value3, 0, 5)));
            Menu.SubMenu("Autobuy").AddItem(new MenuItem("Item4" + championName2, "Mana Potions").SetValue(new Slider(Value4, 0, 5)));
            Menu.SubMenu("Autobuy").AddItem(new MenuItem("Item5" + championName2, "Wards").SetValue(new Slider(Value5, 0, 3)));
            Menu.SubMenu("Autobuy").AddItem(new MenuItem("Item6" + championName2, "Vision Wards").SetValue(new Slider(Value6, 0, 2)));
            if (championName2 != "rengar")
            {
            Menu.SubMenu("Autobuy").AddItem(new MenuItem("Trinketb" + championName2, "Trinket").SetValue(new StringList(new[] { "Warding Totem(Yellow)", "Sweeping Lens(Red)", "Scrying Orb(Blue)", "OFF" }, ValueT)));
            SettingT = Menu.Item("Trinketb" + championName2).GetValue<StringList>().SelectedIndex;
            }
                else if (championName2 == "rengar")
            {
                Menu.SubMenu("Autobuy").AddItem(new MenuItem("TrinketbR" + championName2, "Trinket").SetValue(new StringList(new[] { "Bonetooth Necklace(Yellow)", "Bonetooth Necklace(Red)", "Bonetooth Necklace(Blue)", "OFF" }, ValueT)));
                SettingTR = Menu.Item("TrinketbR" + championName2).GetValue<StringList>().SelectedIndex; // For Rengar because he has special trinkets. 
                }


            Menu.AddSubMenu(new Menu("Global Settings", "Globals"));

            Menu.SubMenu("Globals").AddItem(new MenuItem("AutobuyG1", "Auto Buy Settings").SetValue(new StringList(new[] { "Always", "Only After Press Key", "OFF" }, 0)));
            Menu.SubMenu("Globals").AddItem(new MenuItem("AutobuyA", "Auto Buy Active").SetValue(new KeyBind(32, KeyBindType.Press)));
        //    Menu.SubMenu("Autobuy").AddItem(new MenuItem("Autobuyon" + championName2, "Auto Buy On").SetValue(true));
            Menu.SubMenu("Globals").AddItem(new MenuItem("AutobuySmite", "Auto Buy Machete if Player has Smite").SetValue(false));
            Menu.SubMenu("Globals").AddItem(new MenuItem("AutobuyTW", "Always Buy Warding Totem(Yellow) For Starting Item").SetValue(false));





            Setting1 = Menu.Item("Item1" + championName2).GetValue<StringList>().SelectedIndex;
            Setting2 = Menu.Item("Item2" + championName2).GetValue<StringList>().SelectedIndex;
            Setting3 = Menu.Item("Item3" + championName2).GetValue<Slider>().Value;
            Setting4 = Menu.Item("Item4" + championName2).GetValue<Slider>().Value;
            Setting5 = Menu.Item("Item5" + championName2).GetValue<Slider>().Value;
            Setting6 = Menu.Item("Item6" + championName2).GetValue<Slider>().Value;

            GSetting = Menu.Item("AutobuyG1").GetValue<StringList>().SelectedIndex;
            
           Buyc2 = 1;
       
        }

    
        private static void Game_OnGameLoad(EventArgs args)
        {


     
            Menu = new Menu("Auto Buy Starting Items", "Autobuy", true);
            //    var championName = Player.ChampionName.ToLowerInvariant();
            var championName = Player.ChampionName;
          //  setSmiteSlot();
            switch (championName) // I brought this list from Maskmans (Legacy). 
            {
                case "Ashe":
                     Value1 = 0; //doran blade
                    Value3 = 1; //health potion
                    ValueT = 0; //yello trinket
                    break;
                case "Caitlyn":
                     Value1 = 0; //doran blade
                    Value3 = 1; //health potion
                    ValueT = 0; //yello trinket
       
                    break;
                case "Corki":
                   Value1 = 0; //doran blade
                    Value3 = 1; //health potion
                    ValueT = 0; //yello trinket
                    break;
                case "Draven":
                  Value1 = 0; //doran blade
                    Value3 = 1; //health potion
                    ValueT = 0; //yello trinket
                    break;
                case "Ezreal":
                     Value1 = 0; //doran blade
                    Value3 = 1; //health potion
                    ValueT = 0; //yello trinket
                    break;
                case "Graves":
                     Value1 = 0; //doran blade
                    Value3 = 1; //health potion
                    ValueT = 0; //yello trinket
                    break;
                case "Jinx":
                    Value1 = 0; //doran blade
                    Value3 = 1; //health potion
                    ValueT = 0; //yello trinket
                    break;
                case "Kalista":
             Value1 = 0; //doran blade
                    Value3 = 1; //health potion
                    ValueT = 0; //yello trinket
                    break;
                case "Kogmaw":
                     Value1 = 0; //doran blade
                    Value3 = 1; //health potion
                    ValueT = 0; //yello trinket
                    break;
                case "Lucian":
                    Value1 = 0; //doran blade
                    Value3 = 1; //health potion
                    ValueT = 0; //yello trinket
                    break;
                case "Missfortune":
                     Value1 = 0; //doran blade
                    Value3 = 1; //health potion
                    ValueT = 0; //yello trinket
                    break;
                case "Quinn":
                  Value1 = 0; //doran blade
                    Value3 = 1; //health potion
                    ValueT = 0; //yello trinket
                    break;
                case "Sivir":
                  Value1 = 0; //doran blade
                    Value3 = 1; //health potion
                    ValueT = 0; //yello trinket
                    break;
                case "Tristana":
                     Value1 = 0; //doran blade
                    Value3 = 1; //health potion
                    ValueT = 0; //yello trinket
                    break;
                case "Twitch":
                    Value1 = 0; //doran blade
                    Value3 = 1; //health potion
                    ValueT = 0; //yello trinket                    
                    break;
                case "Urgot":
                    Value1 = 0; //doran blade
                    Value3 = 1; //health potion
                    ValueT = 0; //yello trinket
                    break;
                case "Vayne":
                     Value1 = 0; //doran blade
                    Value3 = 1; //health potion
                    ValueT = 0; //yello trinket
                    break;
                case "Varus":
                     Value1 = 0; //doran blade
                    Value3 = 1; //health potion
                    ValueT = 0; //yello trinket
                    break;

               // default: break;

            }
            Menuadd2();
            Menu.AddToMainMenu();

            Game.PrintChat("<font color=\"#33CC00\">Auto Buy Starting Items</font> - ["+ Player.ChampionName +"] v" + VersionE + " By <font color=\"#0066FF\">E2Slayer</font>");
            Game.OnGameUpdate += Game_OnGameUpdate;
           
        }

        private static void Game_OnGameUpdate(EventArgs args)
        {
            if (Player.IsDead)
                return;

            
            if (Game.ClockTime > 150) // about 2:05 
            {
              //  Game.PrintChat("Time check");
                 //Game.PrintChat("<font color=\"#C11B17\">Auto Buy Starting Items</font> is Unloaded (Time is over 2:00)"); 
                return;
            }
             
            Settingcheck();
            setSmiteSlot();

            if (GSetting == 0)
            {
                Utility.DelayAction.Add(2000, () =>
                {
                    Autobuy1();
                });
            }
            else if (GSetting == 1)
            {
                Autobuy1();
            }


        }

        public static void Settingcheck()
        {
            var championName2 = ObjectManager.Player.ChampionName.ToLowerInvariant();
            Setting1 = Menu.Item("Item1" + championName2).GetValue<StringList>().SelectedIndex;
            Setting2 = Menu.Item("Item2" + championName2).GetValue<StringList>().SelectedIndex;
            Setting3 = Menu.Item("Item3" + championName2).GetValue<Slider>().Value;
            Setting4 = Menu.Item("Item4" + championName2).GetValue<Slider>().Value;
            Setting5 = Menu.Item("Item5" + championName2).GetValue<Slider>().Value;
            Setting6 = Menu.Item("Item6" + championName2).GetValue<Slider>().Value;

            GSetting = Menu.Item("AutobuyG1").GetValue<StringList>().SelectedIndex;

        }

        public static void Autobuy1()
        {
            var championName2 = ObjectManager.Player.ChampionName.ToLowerInvariant();

            if (GSetting == 0 || (GSetting == 1 && Menu.Item("AutobuyA").GetValue<KeyBind>().Active))
            {
                //  Game.PrintChat("If check1");
                if (Buyc == 0 && HasSmite == 1 && Menu.Item("AutobuySmite").GetValue<bool>())
                {
                  //  Game.PrintChat("You have Smite !");
                    Buyc = 1;
                    Player.BuyItem(ItemId.Hunters_Machete);
                    Player.BuyItem(ItemId.Health_Potion);
                    Player.BuyItem(ItemId.Health_Potion);
                    TrinketC();

                     return;
                }
            }
         //   if (!Menu.Item("AutobuySmite").GetValue<bool>())
          //  {
          //     Game.PrintChat("You don't have Smite !");
               
                if (GSetting == 0 || (GSetting == 1 && Menu.Item("AutobuyA").GetValue<KeyBind>().Active))
                {

                    if (Buyc2 == 1 && Player.Level == 1 && Utility.InShopRange() && Game.ClockTime < 150)
                    {

                        //   { "Doran's Blade", "Doran's Ring", "Doran's Shield", "Crystalline Flask", "Hunter's Machete", "Cloth Armor"/*5*/, "Boots of Speed", "Long Sword", "Ancient Coin", "Relic Shield", "Spellthief's Edge"/*10*/, "Dagger", "Brawler's Gloves", "Amplifying Tome", "Sapphire Crystal", "Faerie Charm"/*15*/, "Rejuvenation Bead", "OFF" }, 17)));
                        TrinketC();
                        if (Buyc2 == 1)
                        {

                            Buyc2 = 2; // make sure don't buy item again
                            switch (Setting1)
                            {
                                case 0:
                                {
                                    Player.BuyItem(ItemId.Dorans_Blade);

                                }
                                    break;
                                case 1:
                                {
                                    Player.BuyItem(ItemId.Dorans_Ring);

                                }
                                    break;
                                case 2:
                                {
                                    Player.BuyItem(ItemId.Dorans_Shield);

                                }
                                    break;
                                case 3:
                                {
                                    Player.BuyItem(ItemId.Crystalline_Flask);

                                }
                                    break;
                                case 4:
                                {
                                    Player.BuyItem(ItemId.Hunters_Machete);

                                }
                                    break;
                                case 5:
                                {
                                    Player.BuyItem(ItemId.Cloth_Armor);

                                }
                                    break;
                                case 6:
                                {
                                    Player.BuyItem(ItemId.Boots_of_Speed);

                                }
                                    break;
                                case 7:
                                {
                                    Player.BuyItem(ItemId.Long_Sword);

                                }
                                    break;
                                case 8:
                                {
                                    Player.BuyItem(ItemId.Ancient_Coin);

                                }
                                    break;
                                case 9:
                                {
                                    Player.BuyItem(ItemId.Relic_Shield);

                                }
                                    break;
                                case 10:
                                {
                                    Player.BuyItem(ItemId.Spellthiefs_Edge);

                                }
                                    break;
                                case 11:
                                {
                                    Player.BuyItem(ItemId.Dagger);

                                }
                                    break;
                                case 12:
                                {
                                    Player.BuyItem(ItemId.Brawlers_Gloves);

                                }
                                    break;
                                case 13:
                                {
                                    Player.BuyItem(ItemId.Amplifying_Tome);

                                }
                                    break;
                                case 14:
                                {
                                    Player.BuyItem(ItemId.Sapphire_Crystal);

                                }
                                    break;
                                case 15:
                                {
                                    Player.BuyItem(ItemId.Faerie_Charm);

                                }
                                    break;
                                case 16:
                                {
                                    Player.BuyItem(ItemId.Rejuvenation_Bead);

                                }
                                    break;
                                case 17:
                                {

                                }
                                    break;


                            }

                            switch (Setting2)
                            {
                                case 0:
                                {
                                    Player.BuyItem(ItemId.Faerie_Charm);

                                }
                                    break;
                                case 1:
                                {
                                    Player.BuyItem(ItemId.Rejuvenation_Bead);

                                }
                                    break;
                                case 2:
                                {

                                }
                                    break;
                            }

                            if (Setting3 != 0)
                            {
                                for (int hp = 0; hp < Setting3; hp++)
                                {
                                    Player.BuyItem(ItemId.Health_Potion);
                                }
                            }
                            if (Setting4 != 0)
                            {
                                for (int mp = 0; mp < Setting4; mp++)
                                {
                                    Player.BuyItem(ItemId.Mana_Potion);
                                }
                            }
                            if (Setting5 != 0)
                            {
                                for (int sw = 0; sw < Setting5; sw++)
                                {
                                    Player.BuyItem(ItemId.Stealth_Ward);
                                }
                            }
                            if (Setting6 != 0)
                            {
                                for (int vw = 0; vw < Setting6; vw++)
                                {
                                    Player.BuyItem(ItemId.Vision_Ward);
                                }
                            }



                            if (championName2 != "rengar")
                            {
                                switch (SettingT)
                                {
                                    case 0:
                                    {
                                        Player.BuyItem(ItemId.Warding_Totem_Trinket);

                                    }
                                        break;
                                    case 1:
                                    {
                                        Player.BuyItem(ItemId.Sweeping_Lens_Trinket);

                                    }
                                        break;
                                    case 2:
                                    {
                                        Player.BuyItem(ItemId.Scrying_Orb_Trinket);

                                    }
                                        break;
                                    case 3:
                                    {

                                    }
                                        break;
                                }
                            }

                            else if (championName2 == "rengar")
                            {
                                switch (SettingTR)
                                {
                                    case 0:
                                    {
                                        Player.BuyItem(ItemId.Bonetooth_Necklace);

                                    }
                                        break;
                                    case 1:
                                    {
                                        Player.BuyItem(ItemId.Bonetooth_Necklace_3405);

                                    }
                                        break;
                                    case 2:
                                    {
                                        Player.BuyItem(ItemId.Bonetooth_Necklace_3411);

                                    }
                                        break;
                                    case 3:
                                    {

                                    }
                                        break;
                                }
                            }



                       // }
                    }
                }
            }
                      
            

        }


        public static void setSmiteSlot()  // made by metaphorce
        {
            foreach (var spell in ObjectManager.Player.Spellbook.Spells.Where(spell => String.Equals(spell.Name, "summonersmite", StringComparison.CurrentCultureIgnoreCase)))
            {
                smiteSlot = spell.Slot;
                smite = new Spell(smiteSlot, 700);
            //    Game.PrintChat("Has Smite !");
                HasSmite = 1;
                return;
            }
        }


        public static void TrinketC()  
        {
            var championName2 = ObjectManager.Player.ChampionName.ToLowerInvariant();
            if (Buyc3 == 0 && Menu.Item("AutobuyTW").GetValue<bool>())
            {
            //    Player.BuyItem(ItemId.Bonetooth_Necklace);
            //        Player.BuyItem(ItemId.Warding_Totem_Trinket);

                    if (championName2 != "rengar")
                    {
                       
                                    Player.BuyItem(ItemId.Warding_Totem_Trinket);

                
                    }

                    else if (championName2 == "rengar")
                    {
                        
                                    Player.BuyItem(ItemId.Bonetooth_Necklace);

                    }



                    Buyc3 = 1; // make sure don't buy trinket again
            }
      
        }
   
   

        
    }
}
