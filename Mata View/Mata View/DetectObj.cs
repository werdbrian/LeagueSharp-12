#region

using System;
using System.Collections.Generic;
using System.Linq;

using LeagueSharp;

#endregion

namespace Mata_View
{
    internal class DetectObj
    {
        public static List<ListedText> AllSkills = new List<ListedText>();

        public static bool PanthD;


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
            if (PanthD)
            {
                ho.Duration = 2.5f;
                PanthD = false;
            }
            AllSkills.Add(new ListedText(sender.NetworkId, sender.Name, ho.Duration , sender.Position, Game.Time, sender));
        }
         

        private static
            void OnDeleteObject(GameObject sender, EventArgs args)
        {
            if (!Menus.Menu.Item("activeskill").GetValue<bool>())
                return;
            foreach (var deleteobj in AllSkills.Where(w => w.NetworkId == sender.NetworkId))
            {
                deleteobj.Visible = false;
                AllSkills.Remove(deleteobj);
            }

        }

    }
}
