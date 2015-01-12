#region

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;
using Color = System.Drawing.Color;

#endregion

namespace Mata_View
{
    internal class DetectObj
    {
        public static List<ListedText> AllSkills = new List<ListedText>();
      

        public static bool PanthD;
        public static Obj_AI_Hero Heropos = new Obj_AI_Hero();


        public static void DetectObjload()
        {
            Obj_AI_Base.OnProcessSpellCast += OnProcessSpell;
            
            GameObject.OnCreate += OnCreateObject;
            GameObject.OnDelete += OnDeleteObject;
        }

    


 

        public static void OnProcessSpell(Obj_AI_Base obj, GameObjectProcessSpellCastEventArgs arg)
        {
            if (!Menus.Menu.Item("activeskill").GetValue<bool>())
                return;
            if (obj.Name.Contains("Turret") || obj.Name.Contains("Minion"))
                 return;

            if (arg.SData.Name.Contains("ZhonyasHourglass"))
            {
                if (Menus.Menu.SubMenu("misclist").Item("zhonyas_ring_activate.troy").GetValue<bool>() &&
                    Menus.Menu.Item("activeMisc").GetValue<bool>())
                {
                    var ho = SkillsList.IsObj("zhonyas_ring_activate.troy");
                    if (ho == null) return;

                    var misccheck = SkillsList.IsMisc("zhonyas_ring_activate.troy");
                    if (misccheck != null) return;

                    var sender = new GameObject();
                    foreach (
                        var hero in
                            ObjectManager.Get<Obj_AI_Hero>()
                                .Where(d => obj.BaseSkinName == d.BaseSkinName))
                    {
                        AllSkills.Add(new ListedText(999999998, "zhonyas_ring_activate.troy", ho.Duration,
                            hero.Position, Game.Time, sender, ho.Realtime, hero));
                    }
                }
            }

            if (arg.SData.Name.Contains("KarthusFallenOne"))
            {
                if (Menus.Menu.Item("Karthus_Base_R_Cas.troy").GetValue<bool>())
                {
                    var ho = SkillsList.IsObj("Karthus_Base_R_Cas.troy");
                    if (ho == null) return;

                    var misccheck = SkillsList.IsMisc("Karthus_Base_R_Cas.troy");
                    if (misccheck != null) return;

                    var sender = new GameObject();
                    foreach (
                        var hero in
                            ObjectManager.Get<Obj_AI_Hero>()
                                .Where(d => obj.BaseSkinName == d.BaseSkinName))
                    {
                        AllSkills.Add(new ListedText(999999997, "Karthus_Base_R_Cas.troy", ho.Duration,
                            hero.Position, Game.Time, sender, ho.Realtime, hero));
                    }
                }
            }


            /*
            if (arg.SData.Name.Contains("TalonShadowAssault"))
            {
                Talon = true;
                var ho = SkillsList.IsObj("talon_ult_sound.troy");
                var sender = new GameObject();
                var hero = new Obj_AI_Hero();
                AllSkills.Add(new ListedText(999999997, "talon_ult_sound.troy", ho.Duration,
                        obj.Position, Game.Time, sender, ho.Realtime, hero));
            }*/ // talon is kind of buggy

            if (arg.SData.Name.Contains("PantheonRJump"))
                PanthD = true;

     
        }

        private static void OnCreateObject(GameObject sender, EventArgs args)
        {
            if (!Menus.Menu.Item("activeskill").GetValue<bool>())
                return;
            if (sender.Name.Contains("missile") || sender.Name.Contains("Minion"))
                return;

            var ho = SkillsList.IsObj((ObjectManager.GetUnitByNetworkId<Obj_GeneralParticleEmitter>(sender.NetworkId)).Name);
            if (ho == null) return;
           
            var misccheck = SkillsList.IsMisc((ObjectManager.GetUnitByNetworkId<Obj_GeneralParticleEmitter>(sender.NetworkId)).Name);
            if (misccheck != null) return;
            
 

            {
                switch (ho.Realtime)
                {
                        case 0: //Object First Created Posistion
                        var r1 = DivideRealTime.RealTimeDivide(sender, 1);
                        if (r1 == null) return;
                        if (PanthD)
                        {
                            ho.Duration = 2.5f;
                            PanthD = false;
                        }
                        break;
                    case 1: //Realtime on Hero Position / Don't need to check Hero.isenemy
                       var r2 = DivideRealTime.RealTimeDivide(sender, 2);
                        if (r2 == null) return;
                        break;
                    case 2: //Realtime on Sender Position
                        var r3 = DivideRealTime.RealTimeDivide(sender, 1);
                        if (r3 == null) return;
                        break;
                   
                }
                AllSkills.Add(new ListedText(sender.NetworkId, sender.Name, ho.Duration, sender.Position, Game.Time, sender, ho.Realtime, Heropos));
            }
        }
         

        private static
            void OnDeleteObject(GameObject sender, EventArgs args)
        {
            if (!Menus.Menu.Item("activeskill").GetValue<bool>())
                return;
            foreach (var deleteobj in AllSkills.Where(w => w.NetworkId == sender.NetworkId || w.Name == sender.Name ))
            {
                deleteobj.Visible = false;
                AllSkills.Remove(deleteobj);
            }

        }

    }
}
