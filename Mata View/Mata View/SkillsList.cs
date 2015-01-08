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

namespace Mata_View
{
    class Getobj
    {
        public string Name;
        public float Duration;
        public Getobj(string name, float duration)
        {
            Name = name;
            Duration = duration;
        }
    }

    class SkillsList
    {
            public static List<Getobj> SkillList0 = new List<Getobj>();
            public static float Panthtimer = 1.5f;

            static SkillsList()
            {

               SkillList0.Add(new Getobj("Zed_Base_W_cloneswap_buf.troy", 4.5f));
               SkillList0.Add(new Getobj("Zed_Base_R_cloneswap_buf.troy", 7.5f));
               SkillList0.Add(new Getobj("LeBlanc_Base_W_return_indicator.troy", 4f));
               SkillList0.Add(new Getobj("LeBlanc_Base_RW_return_indicator.troy", 4f));





                SkillList0.Add(new Getobj("Thresh_Base_Lantern_cas_", 6f)); 
                SkillList0.Add(new Getobj("Viktor_Catalyst_", 4f));
                SkillList0.Add(new Getobj("pirate_cannonBarrage_aoe_indicator_", 7f));
                SkillList0.Add(new Getobj("Jinx_E_Mine_Ready_", 4.5f));
                SkillList0.Add(new Getobj("Zyra_R_cast_", 2f));
                SkillList0.Add(new Getobj("Veigar_Base_W_cas_", 1.2f));
                SkillList0.Add(new Getobj("Veigar_Base_E_cage_", 3f));
                SkillList0.Add(new Getobj("akali_smoke_bomb_tar_team_", 8f));
                SkillList0.Add(new Getobj("Pantheon_Base_R_indicator_", 1.5f)); // Visible = 2.5f / Invisible = 1.5f
            }

            public static Getobj IsObj(string skillname)
            {
                var checkenemy = Menus.Menu.Item("enemyskill").GetValue<bool>() && skillname.ToLower().Contains("red");
                var checkally = Menus.Menu.Item("allyskill").GetValue<bool>() && skillname.ToLower().Contains("blue");

                foreach (var proobj in SkillList0.Where(proobj1 => (skillname.ToLower().Contains(proobj1.Name.ToLower()))))
                {
                    if (String.Equals(proobj.Name, skillname, StringComparison.CurrentCultureIgnoreCase))
                        return proobj;
                    if (checkenemy)
                        return proobj;
                    if (checkally)
                        return proobj;
                    else
                    {
                        return null;
                    }
                }
                return null;
            }
    }


}
