
using System;
using System.Collections.Generic;
using System.Linq;
using LeagueSharp;

namespace Mata_View___Rework
{
    class SkillsList
    {
        public static List<SkillAdd> SkillList0 = new List<SkillAdd>();
        public static List<SkillAdd> SkillList1 = new List<SkillAdd>();
        public static List<SkillAddP> SkillList2 = new List<SkillAddP>();

        private static readonly List<String> CurrentChampions = new List<String>();

        public SkillsList()
        {
            foreach (var cham in ObjectManager.Get<Obj_AI_Hero>())
                CurrentChampions.Add(cham.ChampionName);
            CurrentChampions.Add("Misc");
            ProsList();
            SkillObj();
            SkillPro();
        }

        public static void ProsList()
        {
            SkillList2.Add(new SkillAddP("ZhonyasHourglass", "zhonyas_ring_activate.troy"));
            SkillList2.Add(new SkillAddP("UndyingRage", "UndyingRage_glow.troy"));
            SkillList2.Add(new SkillAddP("MonkeyKingSpinToWin", "MonkeyKing_Base_R_Cas.troy"));
            SkillList2.Add(new SkillAddP("ChronoShift", "nickoftime_tar.troy"));
            SkillList2.Add(new SkillAddP("VladimirSanguinePool", "Vladimir_Base_W_buf.troy"));
            SkillList2.Add(new SkillAddP("JudicatorIntervention", "eyeforaneye"));
            SkillList2.Add(new SkillAddP("KennenShurikenStorm", "kennen_ss_aoe_"));
            SkillList2.Add(new SkillAddP("Sadism", "dr_mundo_heal.troy"));
            SkillList2.Add(new SkillAddP("ShenStandUnited", "Shen_StandUnited_shield_v2.troy"));
            SkillList2.Add(new SkillAddP("KarthusFallenOne", "Karthus_Base_R_Cas.troy"));
            SkillList2.Add(new SkillAddP("PoppyDiplomaticImmunity", "DiplomaticImmunity_buf.troy"));
            SkillList2.Add(new SkillAddP("SionR", "Sion_Base_R_Cas.troy"));
            SkillList2.Add(new SkillAddP("OlafRagnarok", "olaf_ragnorok_"));
            SkillList2.Add(new SkillAddP("FerociousHowl", "alistar_trample_"));
            SkillList2.Add(new SkillAddP("ZacR", "Zac_R_tar.troy"));
            SkillList2.Add(new SkillAddP("SkarnerImpale", "Skarner_Base_R_beam.troy"));
            SkillList2.Add(new SkillAddP("AlZaharNetherGrasp", "Malzahar_Base_R_tar.troy"));
            SkillList2.Add(new SkillAddP("SoulShackles", "Morgana_base_R_Indicator_Ring.troy"));
            SkillList2.Add(new SkillAddP("goldcardlock", "Card_Yellow.troy"));
            SkillList2.Add(new SkillAddP("bluecardlock", "Card_Blue.troy"));
            SkillList2.Add(new SkillAddP("redcardlock", "Card_Red.troy"));
            SkillList2.Add(new SkillAddP("MaokaiDrain3", "Maokai_Base_R_Aura.troy"));
            SkillList2.Add(new SkillAddP("infiniteduresschannel", "InfiniteDuress_tar.troy"));
            SkillList2.Add(new SkillAddP("JaxCounterStrike", "Counterstrike_cas.troy"));
        }

        public static void SkillPro()
        {
            var skilltemp = new List<SkillAdd>
            {
                 new SkillAdd("Counterstrike_cas.troy", 2f, 0, "Jax", "Jax E"),
            //    new SkillAdd("Karthus_Base_R_Target.troy", 3f, 1, "Karthus", "Karthus R(Target)"), work later
              //    new SkillAdd("InfiniteDuress_tar.troy", 1.8f, 1, "Warwick", "Warwick R"), //work later
                new SkillAdd("Maokai_Base_R_Aura.troy", 10f, 0, "Maokai", "Maokai R"),
                new SkillAdd("Card_Yellow.troy", 6f, 0, "TwistedFate", "TwistedFate W"),
                new SkillAdd("Card_Blue.troy", 6f, 0, "TwistedFate", "TwistedFate W"),
                new SkillAdd("Card_Red.troy", 6f, 0, "TwistedFate", "TwistedFate W"),
                new SkillAdd("Malzahar_Base_R_tar.troy", 3f, 0, "Malzahar", "Malzahar R"),
                new SkillAdd("Skarner_Base_R_beam.troy", 2f, 0, "Skarner", "Skarner R"),
                new SkillAdd("UndyingRage_glow.troy", 5f, 0, "Tryndamere", "Tryndamere R"),
                new SkillAdd("MonkeyKing_Base_R_Cas.troy", 4f, 0, "MonkeyKing", "Wukong R"),
                new SkillAdd("eyeforaneye", 2f, 1, "Kayle", "Kayle R"),
                new SkillAdd("nickoftime_tar.troy", 5f, 1, "Zilean", "Zilean R"),
                new SkillAdd("Vladimir_Base_W_buf.troy", 2f, 0, "Vladimir", "Vladimir W"),
                new SkillAdd("Karthus_Base_R_Cas.troy", 3f, 0, "Karthus", "Karthus R"),
                new SkillAdd("alistar_trample_", 7f, 0, "Alistar", "Alistar R"),
                new SkillAdd("Shen_StandUnited_shield_v2.troy", 3f, 1, "Shen", "Shen R (Target)"),
                new SkillAdd("DiplomaticImmunity_buf.troy", 7f, 0, "Poppy", "Poppy R"), 
                new SkillAdd("olaf_ragnorok_", 6f, 0, "Olaf", "Olaf R"), 
                new SkillAdd("Morgana_base_R_Indicator_Ring.troy", 3.5f, 0, "Morgana", "Morgana R"),
                new SkillAdd("Sion_Base_R_Cas.troy", 8f, 0, "Sion", "Sion R"), 
                new SkillAdd("Zac_R_tar.troy", 4f, 0, "Zac", "Zac R"),
                new SkillAdd("dr_mundo_heal.troy", 12f, 0, "DrMundo", "Mundo R"),  
                new SkillAdd("zhonyas_ring_activate.troy", 2.5f, 0, "Misc", "Zhonya Hourglass"), 
                new SkillAdd("kennen_ss_aoe_", 3.5f, 0, "Kennen", "Kennen R"),
            };

            foreach (var temp in from temp in skilltemp from list in CurrentChampions where list == temp.GetChampName() select temp)
            {
                SkillList1.Add(temp);
            }
        }

        public static void SkillObj()
        {
            var skilltemp = new List<SkillAdd>
            {
                new SkillAdd("Akali_Base_smoke_bomb_tar_team_", 8f, 0, "Akali", "Akali W"),
                new SkillAdd("MasterYi_Base_W_Buf.troy", 4f, 0, "MasterYi", "MasterYi W"),
                new SkillAdd("W_windwall", 4f, 2, "Yasuo", "Yasuo W"),
                new SkillAdd("Velkoz_Base_R_Beam_Eye.troy", 2.5f, 0, "Velkoz", "Velkoz R"),
                new SkillAdd("Lissandra_Base_R_ring_", 1.5f, 1, "Lissandra", "Lissandra R"), 
                new SkillAdd("Lissandra_Base_R_iceblock.troy", 2.5f, 0, "Lissandra", "Lissandra Self-R"),
                new SkillAdd("ShenTeleport_v2.troy", 3f, 0, "Shen" , "Shen R"),
                new SkillAdd("EggTimer.troy", 6f, 1, "Anivia", "Anivia Egg"),
                new SkillAdd("Passive_Death_Activate.troy", 3f, 1, "Aatrox", "Aatrox Passive"),
                new SkillAdd("Azir_Base_R_SoldierCape_", 5f, 2, "Azir","Azir R"), //Azir_Base_R_SoldierCape_Enemy.troy
                new SkillAdd("Zed_Base_W_cloneswap_buf.troy", 4.5f, 0, "Zed", "Zed W"),
                new SkillAdd("Zed_Base_R_cloneswap_buf.troy", 7.5f, 0,"Zed","Zed R"),
                new SkillAdd("LeBlanc_Base_W_return_indicator.troy", 4f, 0, "Leblanc", "LeBlanc W"),
                new SkillAdd("LeBlanc_Base_RW_return_indicator.troy", 4f, 0, "Leblanc", "LeBlanc R"),
                new SkillAdd("zhonyas_ring_activate.troy", 2.5f, 1, "Misc", "Zhonya Hourglass"),
                new SkillAdd("Zilean_Base_R_Buf.troy", 3f, 0, "Zilean", "Zilean R Revive"),
                new SkillAdd("LifeAura.troy", 4f, 0, "Misc", "Guardian Angel / Zilean Revive"),
                new SkillAdd("global_ss_teleport_", 3.5f, 0, "Misc", "Teleport"),
                new SkillAdd("Bard_Base_E_Door.troy", 10f, 0, "Bard", "Bard E"),
                new SkillAdd("Bard_Base_R_stasis_skin_", 2.5f, 0, "Bard", "Bard R"),
                new SkillAdd("galio_beguilingStatue_taunt_indicator_team_", 2f, 0, "Galio", "Galio R"),
                new SkillAdd("AbsoluteZero2_", 3f, 0, "Nunu", "Nunu R"),
                new SkillAdd("Karthus_Base_W_Post", 5f, 0, "Karthus", "Karthus W"),
                new SkillAdd("Karthus_Base_R_Cas.troy", 3f, 0, "Karthus", "Karthus R"),
                new SkillAdd("Thresh_Base_Lantern_cas_", 6f, 0, "Thresh","Thresh W"),
                new SkillAdd("Viktor_Catalyst_", 4f, 0,"Viktor","Viktor W"),
                new SkillAdd("Viktor_ChaosStorm_", 7f, 2,"Viktor","Viktor R"),
                new SkillAdd("pirate_cannonBarrage_aoe_indicator_", 7f, 0, "Gangplank", "Gangplank R"),
                new SkillAdd("Jinx_Base_E_Mine_Ready_", 4.5f, 0, "Jinx", "Jinx E"),
                new SkillAdd("Zyra_R_cast_", 2f, 0, "Zyra", "Zyra R"),
                new SkillAdd("Veigar_Base_W_cas_", 1.2f, 0, "Veigar", "Veigar W"),
                new SkillAdd("Veigar_Base_E_cage_", 3f, 0, "Veigar", "Veigar E"),
                new SkillAdd("Pantheon_Base_R_indicator_red", 1.5f, 0, "Pantheon", "Pantheon R"), // Visible = 2.5f / Invisible = 1.5f
                new SkillAdd("ReapTheWhirlwind_", 3f, 0, "Janna", "Janna R"),
            };

            foreach (var temp in from temp in skilltemp from list in CurrentChampions where list == temp.GetChampName() select temp) 
            {
                SkillList0.Add(temp);
            }
        }

        public static SkillAdd IsObj(string skillname)
        {
            try
            {
                return SkillList0.FirstOrDefault(skillAdd => skillname.ToLower().Contains(skillAdd.GetSkillName().ToLower()));
            }
            catch (Exception e)
            {
                Console.WriteLine("===MataView Error=== / IsObj");
                Console.WriteLine(e);
                throw;
            }
           
        }

        public static SkillAddP IsSpell(string skillname)
        {
            try
            {
                return SkillList2.FirstOrDefault(skillAdd => skillname.ToLower().Contains(skillAdd.GetSkillName().ToLower()));
            }
            catch (Exception e)
            {
                Console.WriteLine("===MataView Error=== / IsSpell");
                Console.WriteLine(e);
                throw;
            }

        }

        public static SkillAdd IsSpell2(string skillname)
        {
            try
            {
                return SkillList1.FirstOrDefault(skillAdd => skillname.ToLower().Contains(skillAdd.GetSkillName().ToLower()));
            }
            catch (Exception e)
            {
                Console.WriteLine("===MataView Error=== / IsSpell2");
                Console.WriteLine(e);
                throw;
            }

        }
    }
}
