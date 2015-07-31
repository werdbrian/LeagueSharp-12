
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
    class DetectMain
    {

        private static readonly List<TimerDraw> SkillDrawing = new List<TimerDraw>();
        private static readonly List<TimeDrawP> SkillDrawingP = new List<TimeDrawP>();
        public DetectMain()
        {
            Obj_AI_Base.OnProcessSpellCast += OnProcessSpell;
            GameObject.OnCreate += OnCreateObject;
            GameObject.OnDelete += OnDeleteObject;
        }

        private static void OnProcessSpell(Obj_AI_Base obj, GameObjectProcessSpellCastEventArgs arg)
        {
            if (obj == null) return;
            try
            {
                if (ActiveChecker() || !SpellVaildChecker(obj, arg))
                    return;
                var ch = SkillsList.IsSpell(arg.SData.Name);
                if (ch == null)
                    return;
                var ch2 = SkillsList.IsSpell2(ch.GetObjName());
                if (ch2 == null)
                    return;
                if (arg.SData.Name.Contains("JudicatorIntervention")) //work later
                    ch2.SetDuration(KayleUlt(obj));
                SkillDrawingP.Add(new TimeDrawP(ch2.GetSkillName(), ch2.GetDuration(), obj.Position, Game.Time, ch2.GetRealtime(), obj, arg.Target));
            }
            catch (Exception e)
            {
                Console.WriteLine("===MataView Error=== / OnProcessSpell");
                Console.WriteLine(e);
                throw;
            }
           
        }

        private static void OnCreateObject(GameObject sender, EventArgs args)
        {
            try
            {
                if (ActiveChecker() || !ObjVaildChecker(sender, args))
                    return;

                var ch = SkillsList.IsObj(sender.Name);
                if (ch == null)
                    return;

                SkillDrawing.Add(new TimerDraw(sender.NetworkId, sender.Name, ch.GetDuration(), sender.Position, Game.Time, sender, ch.GetRealtime()));
            }
            catch (Exception e)
            {
                Console.WriteLine("===MataView Error=== / OnCreateObject");
                Console.WriteLine(e);
                throw;
            }
        }

        private static void OnDeleteObject(GameObject sender, EventArgs args)
        {
            try
            {
                if (ActiveChecker() || !ObjVaildChecker(sender, args))
                   return;

               foreach (var deleteobj in new List<TimerDraw>(SkillDrawing).Where(w => w.NetworkId == sender.NetworkId || w.Name == sender.Name))
               {
                    deleteobj.Visible = false;
                    SkillDrawing.Remove(deleteobj);
               }

               foreach (var deleteobj2 in new List<TimeDrawP>(SkillDrawingP).Where(w => w.Name == sender.Name))
               {
                   deleteobj2.VisibleTimer = false;
                   SkillDrawingP.Remove(deleteobj2);
               }
            }
            catch (Exception e)
            {
                Console.WriteLine("===MataView Error=== / OnDeleteObject");
                Console.WriteLine(e);
                throw;
            }
            
        }

        private static float KayleUlt(Obj_AI_Base obj)
        {
            if (obj.Spellbook.GetSpell(SpellSlot.R).Level == 0)
                return 0;
            var level = obj.Spellbook.GetSpell(SpellSlot.R).Level;
            float kayleDuration = 2f + (level - 1) * 0.5f;
            return kayleDuration;
        }

        private static Boolean ObjVaildChecker(GameObject sender, EventArgs args)
        {
            return sender != null && args != null && sender.Type == GameObjectType.obj_GeneralParticleEmitter && !sender.Name.Contains("missile") && !sender.Name.Contains("Minion") && !sender.Name.Contains("teleport_flash") && !sender.Name.Contains("teleport_arrive");
        }

        private static Boolean SpellVaildChecker(Obj_AI_Base obj, GameObjectProcessSpellCastEventArgs arg)
        {
            return obj != null && arg != null;
        }

        private static Boolean ActiveChecker()
        {
            return !MenuList.Menu.Item("activeskill").GetValue<bool>();
        }
    }
}
